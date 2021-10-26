// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using osu.Framework.Graphics;
using Akihabara;
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
        const string kInputStream = "input_video";
        const string kOutputStream0 = "output_video";
        const string kOutputStream1 = "multi_face_landmarks";

        private CalculatorGraph graph;

        private OutputStreamPoller<ImageFrame> imagePoller;

        private GCHandle packetCallbackHandle;

        public TrackingComponent(string configText)
        {
            graph = new CalculatorGraph(configText);
            imagePoller = graph.AddOutputStreamPoller<ImageFrame>(kOutputStream0).Value();
            graph.ObserveOutputStream<NormalizedLandmarkListVectorPacket, List<NormalizedLandmarkList>>(kOutputStream1, handleLandmarks, out packetCallbackHandle).AssertOk();

            graph.StartRun().AssertOk();
        }

        private Status handleLandmarks(NormalizedLandmarkListVectorPacket packet)
        {
            var timestamp = packet.Timestamp().Value();
            Glog.Log(Glog.Severity.Info, $"Got landmarks at timestamp {timestamp}");

            var landmarks = packet.Get();
            return Status.Ok();
        }

        public void OnFrame(dynamic frame)
        {
            var inputFrame = new ImageFrame(ImageFormat.Format.Srgba, frame.Width, frame.Height, frame.Width * 4, frame.Data);

            // Then, we need a timestamp to package the image frame in an actual `ImageFramePacket`.
            int timestamp = System.Environment.TickCount & int.MaxValue;
            var inputPacket = new ImageFramePacket(inputFrame, new Timestamp(timestamp));

            // Finally send the packet to the graph
            graph.AddPacketToInputStream(kInputStream, inputPacket);
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

            graph.CloseInputStream(kInputStream);
            var doneStatus = graph.WaitUntilDone();
            packetCallbackHandle.Free();
            doneStatus.AssertOk();
        }
    }
}
