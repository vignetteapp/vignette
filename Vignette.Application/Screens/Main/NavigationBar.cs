// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.Input;

namespace Vignette.Application.Screens.Main
{
    public class NavigationBar : Container
    {
        public Action ToggleRequested;

        public Bindable<ToolbarSection> Current
        {
            get => tabControl.Current;
            set => tabControl.Current = value;
        }

        public IReadOnlyList<ToolbarSection> Items
        {
            get => tabControl.Items;
            set => tabControl.Items = value;
        }

        private readonly ThemedSolidBox mainBackground;

        private readonly ThemedSolidBox extendedBackground;

        private readonly TriggerableComponent hoverTracker;

        private ToolbarTabControl tabControl;

        public NavigationBar()
        {
            Width = Toolbar.TOOLBAR_WIDTH;
            Masking = true;
            RelativeSizeAxes = Axes.Y;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Colour4.Black.Opacity(0),
                Radius = 6.0f,
                Hollow = true,
            };

            Children = new Drawable[]
            {
                hoverTracker = new TriggerableComponent(1000)
                {
                    Condition = () => IsHovered,
                },
                mainBackground = new ThemedSolidBox
                {
                    Width = Toolbar.TOOLBAR_WIDTH,
                    ThemeColour = ThemeColour.NeutralLight,
                    RelativeSizeAxes = Axes.Y,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        extendedBackground = new ThemedSolidBox
                        {
                            Alpha = 0,
                            ThemeColour = ThemeColour.NeutralLighter,
                            RelativeSizeAxes = Axes.Both,
                        },
                        new GridContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            RowDimensions = new[]
                            {
                                new Dimension(GridSizeMode.AutoSize),
                                new Dimension(GridSizeMode.Distributed),
                                new Dimension(GridSizeMode.Absolute, 40),
                            },
                            Content = new[]
                            {
                                new Drawable[]
                                {
                                    new NavigationButtonIcon
                                    {
                                        Icon = FluentSystemIcons.Filled.Navigation24,
                                        Action = () => ToggleRequested?.Invoke(),
                                        Margin = new MarginPadding { Bottom = 20 },
                                    }
                                },
                                new Drawable[]
                                {
                                    tabControl = new ToolbarTabControl
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                    },
                                },
                                new Drawable[]
                                {
                                    new NavigationBrandButton(),
                                }
                            },
                        },
                    }
                },
            };

            hoverTracker.Current.BindValueChanged((e) =>
            {
                if (e.NewValue)
                    Expand();
                else
                    Detract();
            }, true);

            tabControl.Current.ValueChanged += (e) =>
            {
                if (e.NewValue == null || e.OldValue == null)
                    return;

                hoverTracker.Trigger();
            };
        }

        public void Expand()
        {
            mainBackground.Alpha = 0;
            extendedBackground.Alpha = 1;
            this
                .ResizeWidthTo(Toolbar.TOOLBAR_WIDTH_EXTENDED, 500, Easing.OutQuint)
                .FadeEdgeEffectTo(0.15f, 500, Easing.OutQuint);
        }

        public void Detract()
        {
            mainBackground.Alpha = 1;

            // Add fade after a short delay so the effect won't be so jarring
            extendedBackground
                .Delay(100)
                .FadeOut(300, Easing.OutQuint);

            this
                .ResizeWidthTo(Toolbar.TOOLBAR_WIDTH, 500, Easing.OutQuint)
                .FadeEdgeEffectTo(0, 500, Easing.OutQuint);
        }

        protected override bool OnHover(HoverEvent e)
        {
            hoverTracker.Trigger();
            return base.OnHover(e);
        }

        private class ToolbarTabControl : TabControl<ToolbarSection>
        {
            public ToolbarTabControl()
            {
                TabContainer.AllowMultiline = true;
                TabContainer.RelativeSizeAxes = Axes.X;
                TabContainer.AutoSizeAxes = Axes.Y;
            }

            protected override Dropdown<ToolbarSection> CreateDropdown() => null;

            protected override TabItem<ToolbarSection> CreateTabItem(ToolbarSection value) => new ToolbarTabItem(value);

            private class ToolbarTabItem : TabItem<ToolbarSection>
            {
                private readonly Container highlight;

                public ToolbarTabItem(ToolbarSection value)
                    : base(value)
                {
                    Masking = true;
                    AutoSizeAxes = Axes.Both;
                    Children = new Drawable[]
                    {
                highlight = new Container
                {
                    Size = new Vector2(28),
                    Alpha = 0,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Position = new Vector2(-26, 0),
                    Masking = true,
                    CornerRadius = 2.5f,
                    Child = new ThemedSolidBox
                    {
                        RelativeSizeAxes = Axes.Both,
                        ThemeColour = ThemeColour.NeutralPrimary,
                    },
                },
                new NavigationButtonIcon
                {
                    Icon = value.Icon,
                    Text = value.Title,
                },
                    };
                }

                protected override void OnActivated()
                {
                    highlight.Alpha = 1;
                }

                protected override void OnDeactivated()
                {
                    highlight.Alpha = 0;
                }
            }
        }
    }
}
