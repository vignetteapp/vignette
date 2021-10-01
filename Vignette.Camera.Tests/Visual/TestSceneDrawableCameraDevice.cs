// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;
using Vignette.Camera.Graphics;

namespace Vignette.Camera.Tests.Visual
{
    [Ignore("This test cannot be run in headless mode (a physical camera device is required).")]
    public class TestSceneDrawableCameraDevice : TestScene
    {
        private CameraDevice camera;

        public TestSceneDrawableCameraDevice()
        {
            camera = new CameraDevice(0);
            camera.Start();

            Add(new DrawableCameraDevice(camera)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(256),
            });
        }
    }
}
