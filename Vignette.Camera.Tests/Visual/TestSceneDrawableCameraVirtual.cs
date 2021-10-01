// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osuTK;
using Vignette.Camera.Graphics;

namespace Vignette.Camera.Tests.Visual
{
    [Description("Video playback controls")]
    public class TestSceneDrawableCameraVirtual : TestScene
    {
        private SpriteText info;

        private TestCameraVirtual camera;

        private DrawableCameraVirtual drawableCamera;

        public TestSceneDrawableCameraVirtual()
        {
            Add(info = new SpriteText());
        }

        protected override void Update()
        {
            base.Update();

            info.Text = (camera != null && !camera.IsDisposed)
                ? $"Frame: {camera.Position} / {camera.FrameCount} | FPS: {camera.FramesPerSecond} | State: {camera.State}"
                : string.Empty;
        }

        [SetUpSteps]
        public void SetUp()
        {
            AddStep("create camera", () =>
            {
                camera = new TestCameraVirtual();
                drawableCamera = new DrawableCameraVirtual(camera)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(256),
                };

                Add(drawableCamera);
            });
        }

        [Test]
        public void TestPlaybackStart()
        {
            AddAssert("check ready state", () => camera.Ready);
            AddStep("start playback", () => camera.Start());
            AddAssert("check start state", () => camera.Started);
        }

        [Test]
        public void TestPlaybackPausing()
        {
            int frame = 0;

            AddStep("start playback", () => camera.Start());
            AddWaitStep("wait", 20);

            AddStep("pause playback", () =>
            {
                camera.Pause();
                frame = camera.Position;
            });

            AddWaitStep("wait", 20);
            AddAssert("check state", () => camera.Paused);
            AddAssert("check position", () => camera.Position == frame);
        }

        [Test]
        public void TestPlaybackSeeking()
        {
            AddStep("seek playback", () => camera.Seek(100));
            AddAssert("check position", () => camera.Position == 100);
        }

        [Test]
        public void TestPlaybackEnd()
        {
            AddStep("seek near end", () => camera.Seek(890));
            AddStep("start playback", () => camera.Start());
            AddWaitStep("wait", 20);
            AddAssert("check if decoding ended", () => camera.Paused);
        }

        [Test]
        public void TestPlaybackLooping()
        {
            AddStep("set looping", () => camera.Loop = true);
            AddStep("seek near end", () => camera.Seek(890));
            AddStep("start playback", () => camera.Start());
            AddWaitStep("wait", 20);
            AddAssert("check if decoding did not end", () => !camera.Stopped);
            AddAssert("check if playback rewound", () => camera.Position < 500);
        }

        [TearDownSteps]
        public void TearDownSteps()
        {
            AddStep("cleanup", () => drawableCamera.Expire());
        }
    }
}
