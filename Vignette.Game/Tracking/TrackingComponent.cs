// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Akihabara.Framework;
using Akihabara.Framework.Packet;
using Akihabara.Framework.Port;
using Akihabara.Framework.Protobuf;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using SixLabors.ImageSharp.PixelFormats;
using UnmanageUtility;
using Vignette.Camera;
using Vignette.Game.IO;
using static Vignette.Game.Graphics.Utils;
using ImageFormat = Akihabara.Framework.ImageFormat.ImageFormat;
using ImageFrame = Akihabara.Framework.ImageFormat.ImageFrame;

namespace Vignette.Game.Tracking
{
    public class TrackingComponent : Component
    {
        public IReadOnlyList<NormalizedLandmarkList> Landmarks => landmarks?.ToList();

        public byte[] OutputFrame { get; private set; }

        public int OutputFrameWidth { get; private set; }

        public int OutputFrameHeight { get; private set; }

        public int OutputFrameWidthStep { get; private set; }

        public IReadOnlyList<FaceData> Faces
        {
            get
            {
                lock (faces)
                    return faces.ToList();
            }
        }

        [Resolved]
        private MediapipeGraphStore graphStore { get; set; }

        private readonly IBindable<CameraDevice> camera = new Bindable<CameraDevice>();
        private readonly List<FaceData> faces = new List<FaceData>();
        private List<NormalizedLandmarkList> landmarks;
        private GCHandle packetCallbackHandle;
        private CalculatorGraph graph;
        private OutputStreamPoller<ImageFrame> poller;

        private const string graph_name = "face_mesh_desktop_live.pbtxt";
        private const string input_video = "input_video";
        private const string output_video = "output_video";
        private const string output_landmarks = "multi_face_landmarks";

        private long timestampCounter = 0;

        [BackgroundDependencyLoader]
        private void load(IBindable<CameraDevice> camera)
        {
            graph = new CalculatorGraph(graphStore.Get(graph_name));
            poller = graph.AddOutputStreamPoller<ImageFrame>(output_video).Value();

            graph.ObserveOutputStream<NormalizedLandmarkListVectorPacket, List<NormalizedLandmarkList>>(
                output_landmarks, handleLandmarks, out packetCallbackHandle).AssertOk();

            graph.StartRun().AssertOk();

            this.camera.BindTo(camera);
            this.camera.BindValueChanged(handleCameraChange, true);
        }

        private Status handleLandmarks(NormalizedLandmarkListVectorPacket packet)
        {
            landmarks = packet.Get();

            lock (faces)
            {
                faces.Clear();
                foreach (var landmark in landmarks)
                    faces.Add(new FaceData(landmark));
            }

            return Status.Ok();
        }

        private void handleCameraChange(ValueChangedEvent<CameraDevice> e)
        {
            if (e.OldValue != null)
                e.OldValue.OnTick -= handleCameraTick;

            if (e.NewValue != null)
                e.NewValue.OnTick += handleCameraTick;
        }

        private void handleCameraTick()
        {
            if (camera.Value?.Mat == null || camera.Value.Mat.IsEmpty)
                return;

            int width = camera.Value.Width;
            int height = camera.Value.Height;

            var data = camera.Value.Mat.GetData(false);
            if (data == null)
                return;

            var bitmap = data.Cast<byte>().ToArray();
            if (bitmap == null)
                return;

            byte[] image = ConvertRaw<Bgr24, Rgba32>(bitmap, width, height);

            timestampCounter++;

            var inputFrame = new ImageFrame(ImageFormat.Format.Srgba, width, height, width * 4,
                image.ToUnmanagedArray());

            var inputPacket = new ImageFramePacket(inputFrame, new Timestamp(timestampCounter));
            graph.AddPacketToInputStream(input_video, inputPacket).AssertOk();

            flush(); // VERY IMPORTANT!! Remove that and Vignette will leak a lot of memory.
        }

        private void flush()
        {
            var packet = new ImageFramePacket();

            if (!poller.Next(packet))
                return;

            var frame = packet.Get();
            OutputFrameWidth = frame.Width();
            OutputFrameHeight = frame.Height();
            OutputFrameWidthStep = frame.WidthStep();
            OutputFrame = frame.CopyToByteBuffer(OutputFrameHeight * OutputFrameWidthStep);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            packetCallbackHandle.Free();

            if (camera.Value != null)
                camera.Value.OnTick -= handleCameraTick;
        }
    }
}
