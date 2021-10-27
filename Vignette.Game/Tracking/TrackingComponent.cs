// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using UnmanageUtility;

namespace Vignette.Game.Tracking
{
    public class TrackingComponent : Component, IDisposable
    {
        const string k_input_stream = "input_video";
        const string k_output_stream_0 = "output_video";
        const string k_output_stream_1 = "multi_face_landmarks";

        private CalculatorGraph graph;

        private OutputStreamPoller<ImageFrame> imagePoller;

        private GCHandle packetCallbackHandle;

        public TrackingComponent(string configText)
        {
            graph = new CalculatorGraph(configText);
            imagePoller = graph.AddOutputStreamPoller<ImageFrame>(k_output_stream_0).Value();
            graph.ObserveOutputStream<NormalizedLandmarkListVectorPacket, List<NormalizedLandmarkList>>(k_output_stream_1, handleLandmarks, out packetCallbackHandle).AssertOk();

            graph.StartRun().AssertOk();
        }

        private Status handleLandmarks(NormalizedLandmarkListVectorPacket packet)
        {
            var timestamp = packet.Timestamp().Value();
            Glog.Log(Glog.Severity.Info, $"Got landmarks at timestamp {timestamp}");

            var landmarks = packet.Get();
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
        public void OnFrame(Mat frame, string format, Dictionary<ImwriteFlags, int> encodingParams = null)
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
            graph.AddPacketToInputStream(k_input_stream, inputPacket);
        }

        public ImageFrame GetprocessedFrame()
        {
            var packet = new ImageFramePacket();
            if (!imagePoller.Next(packet))
                throw new InvalidOperationException("No frame in the queue");

            var imageFrame = packet.Get();
            return imageFrame;
        }

        void IDisposable.Dispose()
        {
            graph.CloseInputStream(k_input_stream);
            var doneStatus = graph.WaitUntilDone();
            packetCallbackHandle.Free();
            doneStatus.AssertOk();
        }
    }
}
