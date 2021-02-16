// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public class VignetteDropdown<T> : Dropdown<T>
    {
        protected override DropdownHeader CreateHeader() => new VignetteDropdownHeader();

        protected override DropdownMenu CreateMenu() => new VignetteDropdownMenu();

        private class VignetteDropdownHeader : DropdownHeader
        {
            private readonly VignetteSpriteText label;

            protected override string Label
            {
                get => label.Text;
                set => label.Text = value;
            }

            public VignetteDropdownHeader()
            {
                AutoSizeAxes = Axes.None;
                Foreground.AutoSizeAxes = Axes.None;
                Foreground.RelativeSizeAxes = Axes.Both;

                Height = 35;
                BackgroundColour = Colour4.Transparent;
                BackgroundColourHover = Colour4.Transparent;

                Children = new Drawable[]
                {
                    new OutlinedBox
                    {
                        Depth = 1,
                        ThemeColour = ThemeColour.NeutralPrimary,
                        CornerRadius = VignetteStyle.CornerRadius,
                        RelativeSizeAxes = Axes.Both,
                    },
                    label = new VignetteSpriteText
                    {
                        Font = VignetteFont.Medium.With(size: 16),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Left = 10 },
                        ThemeColour = ThemeColour.NeutralPrimary,
                    },
                    new VignetteSpriteIcon
                    {
                        Size = new Vector2(10),
                        Icon = FontAwesome.Solid.CaretDown,
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Margin = new MarginPadding { Right = 10 },
                        ThemeColour = ThemeColour.NeutralPrimary,
                    },
                };
            }
        }

        private class VignetteDropdownMenu : DropdownMenu
        {
            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawableVignetteDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new VignetteScrollContainer(direction);

            protected override Menu CreateSubMenu() => new VignetteMenu(Direction.Vertical);

            public VignetteDropdownMenu()
            {
                Margin = new MarginPadding { Top = 10 };
                BackgroundColour = Colour4.Transparent;

                Masking = true;
                EdgeEffect = VignetteStyle.ElevationOne;

                MaskingContainer.CornerRadius = CornerRadius = VignetteStyle.CornerRadius * 1.5f;
                MaskingContainer.Add(new VignetteBox
                {
                    Depth = 1,
                    RelativeSizeAxes = Axes.Both,
                    ThemeColour = ThemeColour.NeutralLighter,
                    TransitionDuration = 0,
                });

                ItemsContainer.Padding = new MarginPadding { Left = 0.5f, Right = 1, Vertical = 5 };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                ContentContainer.Masking = true;
            }

            private class DrawableVignetteDropdownMenuItem : DrawableDropdownMenuItem
            {
                public DrawableVignetteDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    Foreground.Padding = new MarginPadding(5);
                    ForegroundColour = Colour4.White;
                    BackgroundColour = Colour4.White;
                    BackgroundColourHover = Colour4.White;
                    BackgroundColourSelected = Colour4.White;
                }

                protected override Drawable CreateContent() => new VignetteSpriteText
                {
                    Font = VignetteFont.Regular.With(size: 16),
                    ThemeColour = ThemeColour.NeutralPrimary,
                    TransitionDuration = 0,
                };

                protected override Drawable CreateBackground() => new Container
                {
                    Alpha = 0.0f,
                    RelativeSizeAxes = Axes.Both,
                    Child = new VignetteBox
                    {
                        ThemeColour = ThemeColour.NeutralLight,
                        RelativeSizeAxes = Axes.Both,
                        TransitionDuration = 0,
                    },
                };

                protected override void UpdateBackgroundColour()
                {
                    Background.FadeTo(IsPreSelected ? 1.0f : IsSelected ? 1.0f : 0.0f, 200, Easing.OutQuint);
                }

                protected override void UpdateForegroundColour()
                {
                }
            }
        }
    }
}
