// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Presentations;

namespace Vignette.Game.Screens.Menus
{
    public class NavigationBar : Container
    {
        public NavigationTabControl TabControl { get; private set; }

        private readonly Box background;

        private readonly Box line;

        public Action OnBack;

        public NavigationBar(Presentation presentation)
        {
            Width = 200;
            RelativeSizeAxes = Axes.Y;
            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Absolute, 40),
                        new Dimension(GridSizeMode.Distributed),
                        new Dimension(GridSizeMode.Absolute, 40),
                    },
                    Content = new Drawable[][]
                    {
                        new Drawable[]
                        {
                            new ButtonIcon
                            {
                                Size = new Vector2(40),
                                Icon = FluentSystemIcons.ArrowLeft24,
                                Action = () => OnBack?.Invoke(),
                                CornerRadius = 0,
                            },
                        },
                        new Drawable[]
                        {
                            TabControl = new NavigationTabControl(presentation)
                            {
                                RelativeSizeAxes = Axes.Both,
                            }
                        },
                        new Drawable[]
                        {
                            new NavigationBarButton
                            {
                                RelativeSizeAxes = Axes.X,
                                Text = "version placeholder",
                                Action = () => { },
                            },
                        }
                    }
                },
                line = new Box
                {
                    Width = 2,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Y,
                },
            };
        }

        private Bindable<Theme> theme;

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                background.Colour = e.NewValue.White;
                line.Colour = e.NewValue.NeutralQuaternary;
            }, true);
        }
    }
}
