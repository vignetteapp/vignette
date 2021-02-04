// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Camera;
using Vignette.Application.Camera.Graphics;
using Vignette.Application.Recognition;

namespace Vignette.Application.Tests.Visual.Recognition
{
    public class TestSceneFaceTracker : TestScene
    {
        private readonly CameraVirtual camera;

        private readonly FaceTracker tracker;

        public TestSceneFaceTracker()
        {
            camera = new CameraVirtual(VignetteTestResources.GetResource(@"bakamitai_deepfake_template.mp4"), EncodingFormat.JPEG, new[]
            {
                new ImageEncodingParam(ImwriteFlags.JpegQuality, 50),
                new ImageEncodingParam(ImwriteFlags.JpegOptimize, 50),
            });

            Add(new DrawableCameraVirtual(camera)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Height = camera.Height,
                Width = camera.Width,
                Scale = new Vector2(2.0f),
            });

            Add(tracker = new FaceRecognitionDotNetFaceTracker());
            Add(new TrackerVisualizer(tracker)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Height = camera.Height,
                Width = camera.Width,
                Scale = new Vector2(2.0f),
            });

            camera.Loop = true;

            AddStep(@"start camera", () => camera.Start());
            AddStep(@"start tracking", () => tracker.StartTracking(camera));
            AddStep(@"pause/unpause", () =>
            {
                if (camera.Paused)
                    camera.Resume();
                else
                    camera.Pause();
            });
        }

        private class TrackerVisualizer : Container
        {
            private FaceTracker tracker;

            private Box box;

            private readonly List<Circle> circles = new List<Circle>();

            public TrackerVisualizer(FaceTracker tracker)
            {
                this.tracker = tracker;

                Add(box = new Box { Colour = Colour4.Red, Alpha = 0.5f });

                for (int i = 0; i < 73; i++)
                {
                    var circle = new Circle
                    {
                        Size = new Vector2(2.5f),
                        Colour = Colour4.Blue,
                        Alpha = 0.0f
                    };

                    circles.Add(circle);
                    Add(circle);
                }
            }

            protected override void Update()
            {
                base.Update();

                if (tracker.Faces == null)
                    return;

                if (!tracker.Faces.Any())
                {
                    box.Colour = Colour4.Red;

                    foreach (var circle in circles)
                        circle.Alpha = 0.0f;
                }
                else
                {
                    var face = tracker.Faces[0];

                    box.X = face.Bounds.X;
                    box.Y = face.Bounds.Y;
                    box.Width = face.Bounds.Width;
                    box.Height = face.Bounds.Height;
                    box.Scale = Scale * 0.5f;
                    box.Colour = Colour4.Green;

                    for (int i = 0; i < face.Landmarks.Count(); i++)
                    {
                        var circle = circles[i];
                        var landmark = face.Landmarks.ElementAt(i);

                        circle.Alpha = 1.0f;
                        circle.Position = landmark.Coordinates;
                    }
                }
            }
        }
    }
}
