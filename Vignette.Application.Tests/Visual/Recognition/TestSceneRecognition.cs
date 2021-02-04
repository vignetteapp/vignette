// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using OpenCvSharp;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Camera;
using Vignette.Application.Camera.Graphics;
using Vignette.Application.Recognition;

namespace Vignette.Application.Tests.Visual.Recognition
{
    public abstract class TestSceneRecognition : TestScene
    {
        protected readonly CameraVirtual Camera;

        protected readonly FaceTracker Tracker;

        public TestSceneRecognition()
        {
            Camera = new CameraVirtual(VignetteTestResources.GetResource(@"bakamitai_deepfake_template.mp4"), EncodingFormat.JPEG, new[]
            {
                new ImageEncodingParam(ImwriteFlags.JpegQuality, 100),
                new ImageEncodingParam(ImwriteFlags.JpegOptimize, 50),
            });

            Add(new DrawableCameraVirtual(Camera)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Height = Camera.Height,
                Width = Camera.Width,
                Scale = new Vector2(2.0f),
            });

            Add(Tracker = new FaceRecognitionDotNetFaceTracker());

            Camera.Loop = true;

            AddStep(@"start camera", () => Camera.Start());
            AddStep(@"start tracking", () => Tracker.StartTracking(Camera));
            AddStep(@"pause/unpause", () =>
            {
                if (Camera.Paused)
                    Camera.Resume();
                else
                    Camera.Pause();
            });
        }
    }
}
