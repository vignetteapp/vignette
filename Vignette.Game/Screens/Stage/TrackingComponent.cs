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

namespace Vignette.Game.Screens.Stage
{
    public class TrackingComponent : Component
    {
        public IReadOnlyList<NormalizedLandmarkList> Landmarks => landmarks.ToList();
        public FaceControlPoints ControlPoints { get; private set; }

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
            ControlPoints = new FaceControlPoints(landmarks[0]);
            return Status.Ok();
        }

        protected override void Update()
        {
            base.Update();

            if (camera.Value == null || camera.Value.Mat == null || camera.Value.Mat.IsEmpty)
                return;

            var bitmap = camera.Value.Mat.ToBitmap();

            if (bitmap == null)
                return;

            byte[] image = bitmapToRawBGRA(bitmap);
            int timestamp = Environment.TickCount & int.MaxValue;
            var inputFrame = new ImageFrame(ImageFormat.Format.Srgba, bitmap.Width, bitmap.Height, bitmap.Width * 4,
                image.ToUnmanagedArray());

            var inputPacket = new ImageFramePacket(inputFrame, new Timestamp(timestamp));
            graph.AddPacketToInputStream(input_video, inputPacket);
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

        private byte[] bitmapToRawBGRA(Bitmap bitmap)
        {
            var locked = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            byte[] data = new byte[locked.Stride * bitmap.Height];
            Marshal.Copy(locked.Scan0, data, 0, locked.Stride * bitmap.Height);
            bitmap.UnlockBits(locked);
            //BGR24 --> RGB32

            Image<Bgr24> start = Image.LoadPixelData<Bgr24>(data, bitmap.Width, bitmap.Height);
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
    }
}
