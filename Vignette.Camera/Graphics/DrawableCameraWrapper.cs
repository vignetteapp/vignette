// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Threading;

namespace Vignette.Camera.Graphics
{
    public abstract class DrawableCameraWrapper<T> : CompositeDrawable, ICamera
        where T : Camera, ICamera
    {
        public double FramesPerSecond => Camera.FramesPerSecond;

        public IReadOnlyList<byte> Data => Camera.Data;

        public bool Paused => Camera.Paused;

        public bool Started => Camera.Started;

        public bool Stopped => Camera.Stopped;

        public bool Ready => Camera.Ready;

        int ICamera.Width => Camera.Width;

        int ICamera.Height => Camera.Height;

        Vector2I ICamera.Size => Camera.Size;

        protected readonly T Camera;

        private ScheduledDelegate scheduled;

        private readonly Sprite sprite;
        
        private readonly bool disposeUnderlyingCameraOnDispose;

        protected DrawableCameraWrapper(Drawable content)
        {
            AddInternal(content);
        }

        protected DrawableCameraWrapper(T camera, bool disposeUnderlyingCameraOnDispose = true)
        {
            Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            this.disposeUnderlyingCameraOnDispose = disposeUnderlyingCameraOnDispose;

            AddInternal(sprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                FillMode = FillMode.Fit,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });

            camera.OnTick += () =>
            {
                scheduled?.Cancel();
                scheduled = Schedule(() =>
                {
                    if (camera.Data == null)
                        return;

                    using var memory = new MemoryStream(camera.Data.ToArray());
                    sprite.Texture = Texture.FromStream(memory);
                });
            };
        }

        public void Pause() => Camera.Pause();

        public void Resume() => Camera.Resume();

        public void Start() => Camera.Start();

        public void Stop() => Camera.Stop();

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (disposeUnderlyingCameraOnDispose)
                Camera?.Dispose();
        }
    }
}
