// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;
using Vignette.Camera.Graphics;

namespace Vignette.Camera.Tests.Visual
{
    public class TestSceneSharedCamera : TestScene
    {
        private CameraVirtual camera;

        private DrawableCameraVirtual left, right;

        public TestSceneSharedCamera()
        {
            camera = new TestCameraVirtual();
        }

        [SetUpSteps]
        public void SetUp()
        {
            Add(left = new DrawableCameraVirtual(camera, false)
            {
                X = -0.25f,
                RelativePositionAxes = Axes.X,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(256),
            });

            Add(right = new DrawableCameraVirtual(camera, false)
            {
                X = 0.25f,
                RelativePositionAxes = Axes.X,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(256),
            });
        }

        [Test]
        public void TestSynchronosity()
        {
            AddStep("play", () => camera.Start());
            AddAssert("check state", () => left.Started == right.Started);

            AddStep("pause", () => camera.Pause());
            AddAssert("check state", () => left.Paused == right.Paused);

            AddStep("seek", () => camera.Seek(100));
            AddAssert("check position", () => ((ICameraVirtual)left).Position == ((ICameraVirtual)right).Position);
        }
    }
}
