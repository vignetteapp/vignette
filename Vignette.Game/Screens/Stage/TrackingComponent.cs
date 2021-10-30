// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Akihabara.Framework;
using Akihabara.Framework.ImageFormat;
using Akihabara.Framework.Packet;
using Akihabara.Framework.Port;
using Akihabara.Framework.Protobuf;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using UnmanageUtility;
using Vignette.Camera;
using Vignette.Game.IO;

namespace Vignette.Game.Screens.Stage
{
    public class TrackingComponent : Component
    {
        public IReadOnlyList<NormalizedLandmarkList> Landmarks => landmarks.ToList();

        [Resolved]
        private MediapipeGraphStore graphStore { get; set; }

        [Resolved]
        private IBindable<CameraDevice> camera { get; set; }

        private List<NormalizedLandmarkList> landmarks;
        private GCHandle packetCallbackHandle;
        private CalculatorGraph graph;
        private OutputStreamPoller<ImageFrame> poller;

        private const string graph_name = "face_mesh_desktop_live.pbtxt";
        private const string input_video = "input_video";
        private const string output_video = "output_video";
        private const string output_landmarks = "multi_face_landmarks";

        [BackgroundDependencyLoader]
        private void load()
        {
            graph = new CalculatorGraph(graphStore.Get(graph_name));
            poller = graph.AddOutputStreamPoller<ImageFrame>(output_video).Value();

            graph.ObserveOutputStream<NormalizedLandmarkListVectorPacket, List<NormalizedLandmarkList>>(
                output_landmarks, handleLandmarks, out packetCallbackHandle).AssertOk();

            graph.StartRun().AssertOk();
        }

        private Status handleLandmarks(NormalizedLandmarkListVectorPacket packet)
        {
            landmarks = packet.Get();
            return Status.Ok();
        }

        protected override void Update()
        {
            base.Update();

            if (camera.Value == null)
                return;

            int timestamp = Environment.TickCount & int.MaxValue;
            var inputFrame = new ImageFrame(ImageFormat.Format.Srgb, camera.Value.Width, camera.Value.Height, camera.Value.Width * 4,
                camera.Value.Data.ToUnmanagedArray());

            var inputPacket = new ImageFramePacket(inputFrame, new Timestamp(timestamp));
            graph.AddPacketToInputStream(input_video, inputPacket);

        }

        public bool TryGetFrame(out ImageFrame frame)
        {
            frame = null;

            var packet = new ImageFramePacket();
            if (!poller.Next(packet))
                return false;

            frame = packet.Get();
            return true;
        }
    }
}
