// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Interface
{
    public class ThemedDropdown<T> : Dropdown<T>
    {
        protected override DropdownHeader CreateHeader() => new ThemedDropdownHeader();

        protected override DropdownMenu CreateMenu() => new ThemedDropdownMenu();

        private class ThemedDropdownHeader : DropdownHeader
        {
            private readonly ThemedSpriteText label;

            protected override LocalisableString Label
            {
                get => label.Text;
                set => label.Text = value;
            }

            public ThemedDropdownHeader()
            {
                AutoSizeAxes = Axes.None;
                Foreground.AutoSizeAxes = Axes.None;
                Foreground.RelativeSizeAxes = Axes.Both;

                Height = 25;
                BackgroundColour = Colour4.Transparent;
                BackgroundColourHover = Colour4.Transparent;

                Children = new Drawable[]
                {
                    new ThemedOutlinedBox
                    {
                        Depth = 1,
                        ThemeColour = ThemeColour.NeutralPrimary,
                        CornerRadius = 2.5f,
                        RelativeSizeAxes = Axes.Both,
                    },
                    label = new ThemedSpriteText
                    {
                        Font = SegoeUI.Regular.With(size: 16),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Left = 10 },
                        ThemeColour = ThemeColour.NeutralPrimary,
                    },
                    new ThemedSpriteIcon
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

        private class ThemedDropdownMenu : DropdownMenu
        {
            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawableThemedDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new ThemedScrollContainer(direction);

            protected override Menu CreateSubMenu() => new ThemedMenu(Direction.Vertical);

            public ThemedDropdownMenu()
            {
                BackgroundColour = Colour4.Transparent;

                MaskingContainer.CornerRadius = 5.0f;
                MaskingContainer.EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Shadow,
                    Colour = Colour4.Black.Opacity(0.25f),
                    Radius = 4.0f,
                    Hollow = true,
                };

                MaskingContainer.Add(new ThemedSolidBox
                {
                    Depth = 1,
                    ThemeColour = ThemeColour.NeutralLighter,
                    RelativeSizeAxes = Axes.Both,
                });

                ItemsContainer.Padding = new MarginPadding { Left = 0.5f, Right = 1, Vertical = 5 };
            }

            protected override void AnimateOpen()
            {
                this.TransformTo("Margin", new MarginPadding { Top = 10 }, 200, Easing.OutQuint);
                this.FadeIn(200, Easing.OutQuint);
            }

            protected override void AnimateClose()
            {
                this.TransformTo("Margin", new MarginPadding { Top = 0 }, 200, Easing.OutQuint);
                this.FadeOut(200, Easing.OutQuint);
            }

            protected override void UpdateSize(Vector2 newSize)
            {
                if (Direction == Direction.Vertical)
                {
                    Width = newSize.X;
                    this.ResizeHeightTo(newSize.Y, 200, Easing.OutQuint);
                }
                else
                {
                    Height = newSize.Y;
                    this.ResizeWidthTo(newSize.X, 200, Easing.OutQuint);
                }
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                ContentContainer.Masking = true;
            }

            private class DrawableThemedDropdownMenuItem : DrawableDropdownMenuItem
            {
                public DrawableThemedDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    Foreground.Padding = new MarginPadding(5);
                    ForegroundColour = Colour4.White;
                    BackgroundColour = Colour4.White;
                    BackgroundColourHover = Colour4.White;
                    BackgroundColourSelected = Colour4.White;
                }

                protected override Drawable CreateContent() => new ThemedSpriteText
                {
                    Font = SegoeUI.Regular.With(size: 16),
                    ThemeColour = ThemeColour.NeutralPrimary,
                };

                protected override Drawable CreateBackground() => new Container
                {
                    Alpha = 0.0f,
                    RelativeSizeAxes = Axes.Both,
                    Child = new ThemedSolidBox
                    {
                        ThemeColour = ThemeColour.NeutralLight,
                        RelativeSizeAxes = Axes.Both,
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
