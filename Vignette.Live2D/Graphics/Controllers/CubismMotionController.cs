// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Allocation;
using osu.Framework.Utils;
using System;
using System.Collections.Generic;
using Vignette.Live2D.IO.Serialization;
using Vignette.Live2D.Motion;
using Vignette.Live2D.Utils;

namespace Vignette.Live2D.Graphics.Controllers
{
    /// <summary>
    /// A controller that manages motions of a Live2D model.
    /// </summary>
    public class CubismMotionController : CubismController
    {
        protected readonly Queue<CubismMotion> Queue = new Queue<CubismMotion>();

        protected readonly Dictionary<string, List<CubismMotion>> Entries = new Dictionary<string, List<CubismMotion>>();

        private CubismMotion current;
        private double nextMotionEndTime;

        /// <summary>
        /// The currently playing motiom.
        /// </summary>
        public CubismMotion Current => current;

        /// <summary>
        /// Whether the currently playing motion should loop playback.
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Action invoked when the currently playing motion finishes playback.
        /// </summary>
        public event Action OnMotionFinish;

        [BackgroundDependencyLoader]
        private void load()
        {
            var definitions = Model.Settings.FileReferences.Motions;

            if (definitions == null)
                return;

            foreach ((string key, IEnumerable<CubismModelSetting.FileReference.Motion> motionEntries) in definitions)
            {
                var motions = new List<CubismMotion>();

                foreach (var motionEntry in motionEntries)
                {
                    using var stream = Model.Resources.GetStream(motionEntry.File);
                    motions.Add(new CubismMotion(Model, CubismUtils.ReadJsonSetting<CubismMotionSetting>(stream), motionEntry));
                }

                Entries.Add(key, motions);
            }
        }

        /// <summary>
        /// Enqueues a random motion in the group to be played.
        /// </summary>
        /// <param name="name">The motion group name as defined in the model settings to play.</param>
        /// <returns>Whether we successfully queued a motion.</returns>
        public bool Enqueue(string name)
        {
            if (!Entries.TryGetValue(name, out List<CubismMotion> motions))
                return false;

            return Enqueue(name, RNG.Next(0, motions.Count));
        }

        /// <summary>
        /// Enqueues a motion in the group to be played at a specified index.
        /// </summary>
        /// <param name="name">The motion group name as defined in the model settings to play.</param>
        /// <param name="index">The index of the motion in the group defined in the model settings.</param>
        /// <returns>Whether we successfully queued a motion.</returns>
        public bool Enqueue(string name, int index)
        {
            if (!Entries.TryGetValue(name, out List<CubismMotion> motions))
                return false;

            var next = motions[index];
            Queue.Enqueue(next);

            return true;
        }

        /// <summary>
        /// Plays the next motion in queue.
        /// </summary>
        public void Next() => current = null;

        /// <summary>
        /// Clears the queue and stops the current motion from playing.
        /// </summary>
        public void Stop()
        {
            Queue.Clear();
            Next();
        }

        protected override void Update()
        {
            base.Update();

            if (Current != null && Clock.CurrentTime > nextMotionEndTime && !Loop)
            {
                Next();
                OnMotionFinish?.Invoke();
            }

            if (Current == null && Queue.TryDequeue(out current))
                nextMotionEndTime = Clock.CurrentTime + (current.Duration * 1000);

            current?.Update(Clock.CurrentTime / 1000, Loop);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            OnMotionFinish = null;
        }
    }
}
