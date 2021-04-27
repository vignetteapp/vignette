// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class NavigationViewVertical<T> : NavigationView<T>
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
                            Action = OnBack,
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
                    }
                }
            });
        }

        private bool isExpanded = true;

        private void toggle()
        {
            this.ResizeWidthTo(isExpanded ? 44 : 200, 200, Easing.OutQuint);
            isExpanded = !isExpanded;
        }

        protected override NavigationViewTabControl CreateTabControl()
            => new NavigationViewVerticalTabControl();

        protected class NavigationViewVerticalTabControl : NavigationViewTabControl
        {
            public NavigationViewVerticalTabControl()
            {
                TabContainer.Direction = FillDirection.Vertical;
                TabContainer.AllowMultiline = true;
            }

            protected override TabItem<T> CreateTabItem(T value)
                => new NavigationViewVerticalTabItem(value);
        }

        protected class NavigationViewVerticalTabItem : NavigationViewTabItem
        {
            public NavigationViewVerticalTabItem(T value)
                : base(value)
            {
                if (value is not IHasIcon)
                    throw new InvalidOperationException($"{typeof(T)} must implement IHasIcon to be a valid item.");

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
