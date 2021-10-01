// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.IO.Stores;
using osu.Framework.Testing;
using osuTK;
using System;
using System.IO;
using System.Linq;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Tests.Resources;

namespace Vignette.Live2D.Tests.Visual
{
    public abstract class CubismModelTestScene : TestScene
    {
        private readonly BasicTabControl<Tabs> tabControl;
        private readonly BasicDropdown<string> models;
        private readonly Container modelContainer;
        private readonly FillFlowContainer flow;

        protected CubismModel Model { get; private set; }

        protected Container Extras { get; private set; }

        public CubismModelTestScene()
        {
            Add(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.Distributed),
                    new Dimension(GridSizeMode.Absolute, 300),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        modelContainer = new Container { RelativeSizeAxes = Axes.Both },
                        new GridContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            RowDimensions = new[]
                            {
                                new Dimension(GridSizeMode.AutoSize),
                                new Dimension(GridSizeMode.Distributed),
                                new Dimension(GridSizeMode.AutoSize),
                            },
                            Content = new Drawable[][]
                            {
                                new Drawable[]
                                {
                                    models = new BasicDropdown<string> { RelativeSizeAxes = Axes.X }
                                },
                                new Drawable[]
                                {
                                    new Container
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Children = new Drawable[]
                                        {
                                            new Box
                                            {
                                                Colour = FrameworkColour.GreenDark,
                                                RelativeSizeAxes = Axes.Both,
                                            },
                                            new Box
                                            {
                                                Colour = FrameworkColour.BlueGreenDark,
                                                Height = 25,
                                                RelativeSizeAxes = Axes.X,
                                            },
                                            new BasicScrollContainer
                                            {
                                                RelativeSizeAxes = Axes.Both,
                                                Margin = new MarginPadding { Top = 25 },
                                                Child = flow = new FillFlowContainer
                                                {
                                                    RelativeSizeAxes = Axes.X,
                                                    AutoSizeAxes = Axes.Y,
                                                }
                                            },
                                            tabControl = new BasicTabControl<Tabs>
                                            {
                                                RelativeSizeAxes = Axes.X,
                                                Height = 25,
                                                Items = Enum.GetValues<Tabs>(),
                                            },
                                        }
                                    },
                                },
                                new Drawable[]
                                {
                                    Extras = new Container
                                    {
                                        RelativeSizeAxes = Axes.X,
                                        AutoSizeAxes = Axes.Y,
                                    }
                                },
                            }
                        }
                    },
                },
            });

            models.Current.ValueChanged += _ => ModelChanged();
            getAvailableModels();
        }

        private void getAvailableModels()
        {
            var resources = TestResources.GetModelResourceStore();
            var validPaths = resources.GetAvailableResources().Where(path => path.EndsWith("model3.json"));

            foreach (var path in validPaths)
            {
                var di = new DirectoryInfo(path);
                models.AddDropdownItem(di.Parent.Name);
            }

            models.Current.Value = models.Items.FirstOrDefault();
        }

        protected virtual void ModelChanged()
        {
            var store = new NamespacedResourceStore<byte[]>(TestResources.GetModelResourceStore(), models.Current.Value);
            modelContainer.Child = Model = new CubismModel(store)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(1024),
            };
        }

        private enum Tabs
        {
            Drawables,
            Parameters,
            Parts,
        }
    }
}
