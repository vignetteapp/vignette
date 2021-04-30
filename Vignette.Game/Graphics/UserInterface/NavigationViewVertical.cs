// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class NavigationViewVertical<T> : NavigationView<T>
    {
        public Action OnBack;

        public NavigationViewVertical()
        {
            Width = 200;
            RelativeSizeAxes = Axes.Y;
            Add(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(),
                    new Dimension(GridSizeMode.AutoSize),
                },
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        new FluentButton
                        {
                            Size = new Vector2(44),
                            Icon = FluentSystemIcons.ArrowLeft24,
                            Style = ButtonStyle.Text,
                            Action = () => OnBack?.Invoke(),
                        },
                    },
                    new Drawable[]
                    {
                        new FluentButton
                        {
                            Size = new Vector2(44),
                            Icon = FluentSystemIcons.Navigation20,
                            Style = ButtonStyle.Text,
                            Action = toggle,
                        },
                    },
                    new Drawable[]
                    {
                        Control
                    },
                    new Drawable[]
                    {
                        CreateEndControl(),
                    },
                }
            });
        }

        private bool isExpanded = true;

        private void toggle()
        {
            this.ResizeWidthTo(isExpanded ? 44 : 200, 200, Easing.OutQuint);
            isExpanded = !isExpanded;
        }

        protected virtual Drawable CreateEndControl() => null;

        protected abstract class NavigationViewVerticalTabControl : NavigationViewTabControl
        {
            public NavigationViewVerticalTabControl()
            {
                TabContainer.Direction = FillDirection.Vertical;
                TabContainer.AllowMultiline = true;
            }
        }

        protected abstract class NavigationViewVerticalTabItem : NavigationViewTabItem
        {
            public NavigationViewVerticalTabItem(T value)
                : base(value)
            {
                Height = 44;
                RelativeSizeAxes = Axes.X;

                Add(Highlight.With(d =>
                {
                    d.Height = 0.75f;
                    d.Anchor = Anchor.CentreLeft;
                    d.Origin = Anchor.CentreLeft;
                    d.RelativeSizeAxes = Axes.Y;
                }));
            }

            protected override void OnActivated()
                => Highlight.ResizeWidthTo(2, 200, Easing.OutQuint);

            protected override void OnDeactivated()
                => Highlight.ResizeWidthTo(0, 200, Easing.OutQuint);
        }
    }
}
