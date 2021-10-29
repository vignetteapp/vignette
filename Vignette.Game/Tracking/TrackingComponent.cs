// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using osu.Framework.Graphics;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Akihabara.External;
using Akihabara.Framework;
using Akihabara.Framework.ImageFormat;
using Akihabara.Framework.Packet;
using Akihabara.Framework.Port;
using Akihabara.Framework.Protobuf;
using Emgu.CV.Dnn;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using UnmanageUtility;
using Vignette.Camera;
using Vignette.Game.Screens.Stage;
using Vignette.Live2D.Graphics;
using Model = Vignette.Live2D.Graphics.CubismModel;

namespace Vignette.Game.Tracking
{
    public class TrackingComponent : Component, IDisposable
    {
        private const string input_stream = "input_video";
        private const string output_stream0 = "output_video";
        private const string output_stream1 = "multi_face_landmarks";

        private CalculatorGraph graph;

        private OutputStreamPoller<ImageFrame> imagePoller;

        private GCHandle packetCallbackHandle;

        private MotionController motionController { get; set; }
        private IBindable<CameraDevice> camera { get; set; }

        [BackgroundDependencyLoader]
        private void load(MotionController controller, ResourceStore<byte[]> store, IBindable<CameraDevice> cam)
        {
            motionController = controller;
            string graphConfig = Encoding.UTF8.GetString(store.Get("Graphs/face_mesh_desktop_live.pbtxt"));
            Initialize(graphConfig);
            camera = cam;
            camera.BindValueChanged(onCameraChanged, true);
        }

        private void onCameraChanged(ValueChangedEvent<CameraDevice> obj)
        {
            if (obj.OldValue != null)
            {
                obj.OldValue.Stop();
            }

            if (obj.NewValue != null)
            {
                obj.NewValue.Start();
                obj.NewValue.OnTick += () =>
                {
                    //handle frame and call SendFrame
                    SendEncodedFrame(camera.Value.Data.Reverse().ToArray(), camera.Value.Width, camera.Value.Height);
                };
            }
        }

        public void Initialize(string configText)
        {
            graph = new CalculatorGraph(configText);
            imagePoller = graph.AddOutputStreamPoller<ImageFrame>(output_stream0).Value();
            graph.ObserveOutputStream<NormalizedLandmarkListVectorPacket, List<NormalizedLandmarkList>>(output_stream1, handleLandmarks, out packetCallbackHandle).AssertOk();

            graph.StartRun().AssertOk();
        }

        private Status handleLandmarks(NormalizedLandmarkListVectorPacket packet)
        {
            var timestamp = packet.Timestamp().Value();
            Glog.Log(Glog.Severity.Info, $"Got landmarks at timestamp {timestamp}");

            var landmarks = packet.Get();

            if (motionController != null)
            {
                motionController.ApplyLandmarks(landmarks);
            }
            return Status.Ok();
        }

        // Taken from Vignette.Camera/Camera.cs -> private void handleImageGrabbed()
        private byte[] getBytesFromFrame(Mat frame, string format, Dictionary<ImwriteFlags, int> encodingParams = null)
        {
            if (frame.IsEmpty)
                throw new ArgumentException("The frame is empty.");

            using (var vector = new VectorOfByte())
            {
                CvInvoke.Imencode(format, frame, vector, encodingParams?.ToArray());
                return vector.ToArray();
            }
        }
            
        public void SendEncodedFrame(byte[] encodedData, int width, int height)
        {
            var pixelMat = new Mat();
            CvInvoke.Imdecode(encodedData, ImreadModes.AnyColor, pixelMat);

            if (pixelMat.IsEmpty)
            {
                throw new ArgumentException("Invalid frame");
            }

            var pixelData = pixelMat.GetRawData().ToUnmanagedArray();

            var inputFrame = new ImageFrame(
                ImageFormat.Format.Srgb, // depends on encoding params
                width,
                height,
                width * 4, // depends on encoding params, for example RGB vs. RGBA
                pixelData
            );

            // Then, we need a timestamp to package the image frame in an actual `ImageFramePacket`.
            int timestamp = System.Environment.TickCount & int.MaxValue;
            var inputPacket = new ImageFramePacket(inputFrame, new Timestamp(timestamp));

            // Finally send the packet to the graph
            graph.AddPacketToInputStream(input_stream, inputPacket);
        }

        // TODO: figure out encoding params
        public void SendFrame(Mat frame, string format, Dictionary<ImwriteFlags, int> encodingParams = null)
        {
            byte[] frameBytes = getBytesFromFrame(frame, format, encodingParams);
            var pixelData = new UnmanagedArray<byte>(frameBytes.Length);
            pixelData.CopyFrom(frameBytes);

            var inputFrame = new ImageFrame(
                ImageFormat.Format.Srgb, // depends on encoding params
                frame.Width,
                frame.Height,
                frame.Width * 4, // depends on encoding params, for example RGB vs. RGBA
                pixelData
            );

            // Then, we need a timestamp to package the image frame in an actual `ImageFramePacket`.
            int timestamp = System.Environment.TickCount & int.MaxValue;
            var inputPacket = new ImageFramePacket(inputFrame, new Timestamp(timestamp));

            // Finally send the packet to the graph
            graph.AddPacketToInputStream(input_stream, inputPacket);
        }

        public ImageFrame GetProcessedFrame()
        {
            var packet = new ImageFramePacket();
            if (!imagePoller.Next(packet))
                throw new InvalidOperationException("No frame in the queue");

            var imageFrame = packet.Get();
            return imageFrame;
        }

        void IDisposable.Dispose()
        {
            graph.CloseInputStream(input_stream);
            var doneStatus = graph.WaitUntilDone();
            packetCallbackHandle.Free();
            doneStatus.AssertOk();
        }
    }
}
