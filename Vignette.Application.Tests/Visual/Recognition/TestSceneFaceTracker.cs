// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Application.Recognition;

namespace Vignette.Application.Tests.Visual.Recognition
{
    public class TestSceneFaceTracker : TestSceneRecognition
    {
        private FaceTracker tracker;

        private TrackerVisualizer visualizer;

        private readonly Sprite regionOfInterest;

        private readonly BasicCheckbox visualizerVisibility;

        private readonly BasicDropdown<VisualizerMode> visualizerMode;

        private readonly BasicDropdown<FaceRegion> regionSelector;

        public TestSceneFaceTracker()
        {
            Add(new FillFlowContainer
            {
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Width = 200.0f,
                Margin = new MarginPadding(5),
                Spacing = new Vector2(0, 5),
                Children = new Drawable[]
                {
                    visualizerVisibility = new BasicCheckbox { LabelText = @"Toggle Visualizer" },
                    visualizerMode = new BasicDropdown<VisualizerMode>
                    {
                        RelativeSizeAxes = Axes.X,
                        Items = Enum.GetValues<VisualizerMode>(),
                    },
                    regionSelector = new BasicDropdown<FaceRegion>
                    {
                        RelativeSizeAxes = Axes.X,
                        Items = Enum.GetValues<FaceRegion>(),
                        Alpha = 0.0f,
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 200.0f,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                Colour = Colour4.Black,
                                RelativeSizeAxes = Axes.Both,
                            },
                            regionOfInterest = new Sprite
                            {
                                RelativeSizeAxes = Axes.Both,
                                FillMode = FillMode.Fit,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            }
                        }
                    }
                }
            });

            visualizerVisibility.Current.ValueChanged += (state) => visualizer.Alpha = state.NewValue ? 1.0f : 0.0f;
            visualizerVisibility.Current.Value = true;

            regionSelector.Current.BindValueChanged((mode) => visualizer.Region = mode.NewValue, true);

            visualizerMode.Current.BindValueChanged((mode) =>
            {
                regionSelector.Alpha = mode.NewValue == VisualizerMode.Regional ? 1.0f : 0.0f;
                visualizer.Mode = mode.NewValue;
            }, true);

            visualizerMode.Current.Value = VisualizerMode.General;
        }

        protected override void Update()
        {
            base.Update();

            if (Camera.Data == null)
                return;

            if (!(tracker?.Faces?.Any() ?? false))
                return;

            // store copies so we're thread-safe
            var cam = Camera.Data.ToArray();
            var faces = tracker.Faces.ToArray();

            var mat = Mat.FromImageData(cam);
            var roi = faces[0].GetRegionBounds(regionSelector.Current.Value);

            if (regionOfInterest.IsLoaded)
                regionOfInterest.Texture = Texture.FromStream(mat[new Rect((int)roi.X, (int)roi.Y, (int)roi.Width, (int)roi.Height)].ToMemoryStream());
        }

        protected override void TrackerChanged(FaceTracker tracker)
        {
            visualizer?.Expire();

            this.tracker = tracker;

            Add(visualizer = new TrackerVisualizer(tracker)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Height = Camera.Height,
                Width = Camera.Width,
                Scale = new Vector2(2.0f),
            });
        }

        private class TrackerVisualizer : Container
        {
            public VisualizerMode Mode { get; set; }

            public FaceRegion Region { get; set; }

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

                box.Scale = Scale * 0.5f;
                box.Colour = tracker.Faces.Any() ? Colour4.Green : Colour4.Red;

                if (!tracker.Faces.Any())
                {
                    foreach (var circle in circles)
                        circle.Alpha = 0.0f;
                }
                else
                {
                    var face = tracker.Faces[0];

                    switch (Mode)
                    {
                        case VisualizerMode.General:
                            visualizeGeneral(face);
                            break;

                        case VisualizerMode.Regional:
                            visualizeRegional(face);
                            break;
                    }

                    for (int i = 0; i < face.Landmarks.Count(); i++)
                    {
                        var circle = circles[i];
                        var landmark = face.Landmarks.ElementAt(i);

                        circle.Alpha = 1.0f;
                        circle.Position = landmark.Coordinates;
                    }
                }
            }

            private void visualizeGeneral(Face face)
            {
                box.X = face.Bounds.X;
                box.Y = face.Bounds.Y;
                box.Width = face.Bounds.Width;
                box.Height = face.Bounds.Height;
            }

            private void visualizeRegional(Face face)
            {
                var bounds = face.GetRegionBounds(Region);

                box.X = bounds.X;
                box.Y = bounds.Y;
                box.Width = bounds.Width;
                box.Height = bounds.Height;
            }
        }

        private enum VisualizerMode
        {
            General,

            Regional,
        }
    }
}
