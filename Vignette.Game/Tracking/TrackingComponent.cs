// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
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
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using UnmanageUtility;
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

        private MotionController cubismController { get; set; }

        [BackgroundDependencyLoader]
        private void load(MotionController controller, ResourceStore<byte[]> store)
        {
            cubismController = controller;
            string graphConfig = Encoding.UTF8.GetString(store.Get("Graphs/face_mesh_desktop_live.pbtxt"));
            Initialize(graphConfig);
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

            cubismController.ApplyLandmarks(landmarks);

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

        // TODO: figure out encoding params
        public void SendFrame(Mat frame, string format, Dictionary<ImwriteFlags, int> encodingParams = null)
        {
            byte[] frameBytes = getBytesFromFrame(frame, format, encodingParams);
            var pixelData = new UnmanagedArray<byte>(frameBytes.Length);
            pixelData.CopyFrom(frameBytes);

            var inputFrame = new ImageFrame(
                ImageFormat.Format.Unknown, // depends on encoding params
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
