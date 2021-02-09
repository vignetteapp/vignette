// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osuTK;
using Vignette.Application.Camera;

namespace Vignette.Application.Recognition
{
    public abstract class FaceTracker : Component, IFaceTracker
    {
        public IReadOnlyList<Face> Faces => faces?.ToArray();

        public int Tracked => faces?.Count() ?? 0;

        public double Delta => Time.Current - lastUpdateTime;

        protected ICamera Camera { get; private set; }

        private readonly CancellationTokenSource cancellationToken = new CancellationTokenSource();

        private bool isDisposed;

        private double lastUpdateTime;

        private IEnumerable<Face> faces;

        private Task tracker;

        public virtual void StartTracking(ICamera camera) => Camera = camera;

        public virtual void StopTracking() => Camera = null;

        protected abstract IEnumerable<Face> Track();

        public abstract Vector3? GetHeadAngles(int index = 0);

        public abstract Vector2? GetHeadPosition(int index = 0);

        public abstract Vector2? GetEyePupilPosition(FaceRegion eye, int headIndex = 0);

        public abstract float? GetEyeLidOpen(FaceRegion eye, int headIndex = 0);

        public abstract float? GetMouthOpen(int index = 0);

        protected override void LoadComplete()
        {
            base.LoadComplete();
            tracker = Task.Factory.StartNew(() => track(cancellationToken.Token), cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void track(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (Camera?.Data == null)
                    continue;

                faces = Track();

                lastUpdateTime = Time.Current;
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposed)
                return;

            if (!isDisposing)
                return;

            cancellationToken.Cancel();
            tracker.Wait();
            tracker.Dispose();
            cancellationToken.Dispose();

            isDisposed = true;
        }
    }
}
