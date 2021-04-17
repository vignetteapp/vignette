// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Game.Graphics.Themes;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Presentations;
using Vignette.Game.Presentations.Menus;

namespace Vignette.Game.Screens.Menus
{
    public class MenuView : GridContainer
    {
        public Presentation Presentation { get; private set; }

        public FillFlowContainer Header { get; private set; }

        public FillFlowContainer Footer { get; private set; }

        private readonly Box stackBackground;

        private readonly Box headerBackground;

        private readonly Box headerLine;

        private readonly Box footerBackground;

        private readonly Box footerLine;

        private readonly SpriteText branding;

        private Drawable footerContent;

        private Drawable headerContent;

        public MenuView()
        {
            RelativeSizeAxes = Axes.Both;
            RowDimensions = new[]
            {
                new Dimension(GridSizeMode.AutoSize),
                new Dimension(GridSizeMode.Distributed),
                new Dimension(GridSizeMode.AutoSize),
            };

            Content = new Drawable[][]
            {
                new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Children = new Drawable[]
                        {
                            headerBackground = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Alpha = 0.95f,
                            },
                            headerLine = new Box
                            {
                                Height = 2,
                                RelativeSizeAxes = Axes.X,
                                Anchor = Anchor.BottomLeft,
                                Origin = Anchor.BottomLeft,
                            },
                            new FillFlowContainer
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Direction = FillDirection.Vertical,
                                Padding = new MarginPadding { Horizontal = 30 },
                                Children = new Drawable[]
                                {
                                    branding = new SpriteText
                                    {
                                        Text = @"Vignette",
                                        Font = Spartan.Bold.With(size: 36),
                                        Margin = new MarginPadding { Vertical = 10 },
                                        Spacing = new Vector2(-2f, 0),
                                    },
                                    Header = new FillFlowContainer
                                    {
                                        RelativeSizeAxes = Axes.X,
                                        AutoSizeAxes = Axes.Y,
                                        AutoSizeEasing = Easing.OutQuint,
                                        AutoSizeDuration = 200,
                                        Direction = FillDirection.Vertical,
                                    }
                                }
                            }
                        }
                    }
                },
                new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            stackBackground = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Alpha = 0.95f,
                            },
                            Presentation = new Presentation(),
                        }
                    }
                },
                new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Children = new Drawable[]
                        {
                            footerBackground = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Alpha = 0.95f,
                            },
                            footerLine = new Box
                            {
                                Height = 2,
                                RelativeSizeAxes = Axes.X,
                            },
                            Footer = new FillFlowContainer
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Direction = FillDirection.Vertical,
                                AutoSizeEasing = Easing.OutQuint,
                                AutoSizeDuration = 200,
                                Padding = new MarginPadding { Horizontal = 30 },
                            },
                        }
                    }
                }
            };

            Presentation.SlidePushed += onSlidePushed;
        }

        private Bindable<Theme> theme;

        [BackgroundDependencyLoader]
        private void load(Bindable<Theme> theme)
        {
            this.theme = theme.GetBoundCopy();
            this.theme.BindValueChanged(e =>
            {
                branding.Colour = e.NewValue.Black;
                stackBackground.Colour = e.NewValue.White;
                headerBackground.Colour = e.NewValue.White;
                footerBackground.Colour = e.NewValue.White;
                headerLine.Colour = e.NewValue.NeutralQuaternary;
                footerLine.Colour = e.NewValue.NeutralQuaternary;
            }, true);
        }

        private void onSlidePushed(ISlide prev, ISlide next)
        {
            headerContent?.Expire();
            footerContent?.Expire();

            if (next is MenuSlide screen)
            {
                var header = screen.CreateHeader();
                var footer = screen.CreateFooter();

                if (header != null)
                    Header.Add(headerContent = header);

                if (footer != null)
                    Footer.Add(footerContent = footer);
            }
            else
            {
                headerContent = null;
                footerContent = null;
            }
        }
    }
}
