// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Akihabara.Framework;
using Akihabara.Framework.Packet;
using Akihabara.Framework.Port;
using Akihabara.Framework.Protobuf;
using Emgu.CV;
using Emgu.CV.CvEnum;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.PixelFormats;
using UnmanageUtility;
using Vignette.Camera;
using Vignette.Game.IO;
using Image = SixLabors.ImageSharp.Image;
using ImageFormat = Akihabara.Framework.ImageFormat.ImageFormat;
using ImageFrame = Akihabara.Framework.ImageFormat.ImageFrame;
using Rectangle = System.Drawing.Rectangle;

namespace Vignette.Game.Tracking
{
    public class TrackingComponent : Component
    {
        public IReadOnlyList<NormalizedLandmarkList> Landmarks => landmarks?.ToList();

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

            byte[] image = bitmapToRawBGRA(bitmap, width, height);

            timestampCounter++;

            var inputFrame = new ImageFrame(ImageFormat.Format.Srgba, width, height, width * 4,
                image.ToUnmanagedArray());

            var inputPacket = new ImageFramePacket(inputFrame, new Timestamp(timestampCounter));
            graph.AddPacketToInputStream(input_video, inputPacket).AssertOk();
            RemovePacketFromQueue();
        }

        public bool TryGetFrame(out Bitmap frame)
        {
            frame = null;

            var packet = new ImageFramePacket();
            if (!poller.Next(packet))
                return false;

            var raw = packet.Get();
            var arr = raw.CopyToByteBuffer(raw.Height() * raw.WidthStep());
            frame = rawBGRAToBitmap(arr, raw.Width(), raw.Height());
            return true;
        }

        public bool RemovePacketFromQueue()
        {
            var packet = new ImageFramePacket();
            if (!poller.Next(packet))
                return false;

            return true;
        }

        public bool TryGetRawFrame(out byte[] frame)
        {
            frame = null;

            var packet = new ImageFramePacket();
            if (!poller.Next(packet))
                return false;

            var raw = packet.Get();
            frame = raw.CopyToByteBuffer(raw.Height() * raw.WidthStep());
            return true;
        }

        private byte[] bitmapToRawBGRA(byte[] data, int width, int height)
        {
            //BGR24 --> RGB32

            Image<Bgr24> start = Image.LoadPixelData<Bgr24>(data, width, height);
            //convert to 32 bit

            Span<Bgr24> pixels;
            if (!start.TryGetSinglePixelSpan(out pixels))
            {
                throw new InvalidOperationException("Image is too big");
            }

            Bgra32[] dest = new Bgra32[pixels.Length];
            Span<Bgra32> destination = new Span<Bgra32>(dest);
            PixelOperations<Bgr24>.Instance.ToBgra32(new SixLabors.ImageSharp.Configuration(), pixels, destination);
            start.Dispose();
            GC.Collect();
            return MemoryMarshal.AsBytes(destination).ToArray();
        }

        private Bitmap rawBGRAToBitmap(byte[] data, int width, int height)
        {
            Bitmap b = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle BoundsRect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = b.LockBits(BoundsRect,
                ImageLockMode.WriteOnly,
                b.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            Marshal.Copy(data, 0, ptr, data.Length);
            b.UnlockBits(bmpData);
            return b;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            packetCallbackHandle.Free();
        }
    }
}
