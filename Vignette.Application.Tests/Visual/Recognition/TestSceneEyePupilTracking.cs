// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Application.Recognition;

namespace Vignette.Application.Tests.Visual.Recognition
{
    public class TestSceneEyePupilTracking : TestSceneRecognition
    {
        private FaceTracker tracker;

        private readonly SpriteText positionDisplay;

        public TestSceneEyePupilTracking()
        {
            Add(positionDisplay = new SpriteText { Margin = new MarginPadding(5) });
        }

        protected override void Update()
        {
            base.Update();

            var l = tracker.GetEyePupilPosition(FaceRegion.LeftEye) ?? Vector2.Zero;
            var r = tracker.GetEyePupilPosition(FaceRegion.RightEye) ?? Vector2.Zero;
            positionDisplay.Text = $@"Left Eye: ({l.X},{l.Y}) | Right Eye: ({r.X},{r.Y})";
        }

        protected override void TrackerChanged(FaceTracker tracker) => this.tracker = tracker;
    }
}
