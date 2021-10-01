// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Logging;

namespace Vignette.Camera
{
    /// <summary>
    /// A base class for camera components which handles device or file access, disposal, and update logic.
    /// </summary>
    public abstract class Camera : IDisposable, ICamera
    {
        public int Width => Capture.Width;

        public int Height => Capture.Height;

        public Vector2I Size => new Vector2I(Width, Height);

        public double FramesPerSecond => Capture.Get(CapProp.Fps);

        public IReadOnlyList<byte> Data => data;

        /// <summary>
        /// Fired when a new update occurs. The frequency of invocations is tied to the <see cref="FramesPerSecond"/>.
        /// </summary>
        public event Action OnTick;

        public bool Paused => State == DecoderState.Paused;

        public bool Started => State == DecoderState.Started;

        public bool Stopped => State == DecoderState.Stopped;

        public bool Ready => State == DecoderState.Ready;

        /// <summary>
        /// Whether this <see cref="Camera"/> has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        protected DecoderState State { get; private set; } = DecoderState.Ready;

        protected Mat Mat { get; private set; }

        private static Logger logger => Logger.GetLogger("performance-camera");

        private readonly EncodingFormat format;
        private readonly Dictionary<ImwriteFlags, int> encodingParams;
        private byte[] data;

        internal VideoCapture Capture;

        public Camera(EncodingFormat format = EncodingFormat.PNG, Dictionary<ImwriteFlags, int> encodingParams = null)
        {
            this.format = format;
            this.encodingParams = encodingParams;
        }

        public void Start()
        {
            if (!Ready)
                return;

            Capture.ImageGrabbed += handleImageGrabbed;
            Capture.Start();

            State = DecoderState.Started;
        }

        public virtual void Pause()
        {
            if (Ready || Paused)
                return;

            Capture.Pause();
            State = DecoderState.Paused;
        }

        public virtual void Resume()
        {
            if (Ready || !Paused)
                return;

            Capture.Start();
            State = DecoderState.Started;
        }

        public void Stop()
        {
            if (Ready)
                return;

            OnTick = null;

            Capture?.Dispose();

            State = DecoderState.Stopped;
        }

        private void handleImageGrabbed(object sender, EventArgs args)
        {
            PreTick();

            try
            {
                Mat = new Mat();

                if (Capture.Retrieve(Mat))
                {
                    if (Mat.IsEmpty)
                        return;

                    using (var vector = new VectorOfByte())
                    {
                        CvInvoke.Imencode(getStringfromEncodingFormat(format), Mat, vector, encodingParams?.ToArray());
                        data = vector.ToArray();
                    }
                }

                OnTick?.Invoke();
            }
            catch (CvException e)
            {
                logger.Add($@"{e.Status} {e.Message}", osu.Framework.Logging.LogLevel.Verbose, e);
            }
        }

        protected virtual void PreTick()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            Stop();

            IsDisposed = true;
        }

        ~Camera()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static string getStringfromEncodingFormat(EncodingFormat format)
        {
            switch (format)
            {
                case EncodingFormat.PNG:
                    return ".png";

                case EncodingFormat.JPEG:
                    return ".jpg";

                case EncodingFormat.TIFF:
                    return ".tif";

                case EncodingFormat.WebP:
                    return ".webp";

                case EncodingFormat.Bitmap:
                    return ".bmp";

                case EncodingFormat.JPEG2000:
                    return ".jp2";

                case EncodingFormat.PBM:
                    return ".pbm";

                case EncodingFormat.Raster:
                    return ".ras";

                default:
                    throw new ArgumentOutOfRangeException($@"""{nameof(format)}"" is not a valid export format.");
            }
        }

        protected enum DecoderState
        {
            Ready,

            Started,

            Paused,

            Stopped,
        }
    }
}
