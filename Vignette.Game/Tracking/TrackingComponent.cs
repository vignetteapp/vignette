// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.IO;
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

        [Resolved]
        private IBindable<CameraDevice> camera { get; set; }

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

            lock (faces)
            {
                faces.Clear();
                foreach (var landmark in landmarks)
                    faces.Add(new FaceData(landmark));
            }

            return Status.Ok();
        }

        protected override void Update()
        {
            base.Update();

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

            FlushOutputPoller(); // VERY IMPORTANT!! Remove that and Vignette will leak a lot of memory.
        }

        public ImageFramePacket FetchPacketFromQueue()
        {
            var packet = new ImageFramePacket();
            if (!poller.Next(packet))
                throw new NoPacketException();

            return packet;
        }

        public void FlushOutputPoller()
        {
            try
            {
                OutputFrame = TryGetRawFrame(out var width, out var height, out var widthStep);
                OutputFrameWidth = width;
                OutputFrameHeight = height;
                OutputFrameWidthStep = widthStep;
            }
            catch (NoPacketException)
            {
            }
        }

        public byte[] TryGetRawFrame(out int width, out int height, out int widthStep)
        {
            var packet = FetchPacketFromQueue();
            var raw = packet.Get();
            width = raw.Width();
            height = raw.Height();
            widthStep = raw.WidthStep();
            return raw.CopyToByteBuffer(height * widthStep);
        }




        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            packetCallbackHandle.Free();
        }
    }

    internal class NoPacketException : IOException
    {
        public NoPacketException() : base("No packet in the poller")
        {
        }
    }
}
