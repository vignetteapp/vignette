// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu
{
    public class HomeScreen : MenuScreen
    {
        public override LocalisableString Title => "Home";

        public override IconUsage Icon => FluentSystemIcons.Home24;

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            RelativeSizeAxes = Axes.Both;
            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    Spacing = new Vector2(0, 20),
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = ColourInfo.GradientHorizontal(Colour4.FromHex("BE58CB"), Colour4.FromHex("F10E5A")),
                                },
                                new Container
                                {
                                    Margin = new MarginPadding { Vertical = 40, Left = 20 },
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Children = new Drawable[]
                                    {
                                        new Sprite
                                        {
                                            Size = new Vector2(50),
                                            Colour = Colour4.White,
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Texture = textures.Get("branding"),
                                        },
                                        new Container
                                        {
                                            Margin = new MarginPadding { Left = 70 },
                                            RelativeSizeAxes = Axes.X,
                                            Height = 60,
                                            Children = new Drawable[]
                                            {
                                                new SpriteText
                                                {
                                                    Font = Spartan.Bold.With(size: 40),
                                                    Text = "Vignette",
                                                    Colour = Colour4.White,
                                                    Spacing = new Vector2(-1.5f, 0),
                                                },
                                                new SpriteText
                                                {
                                                    Text = @"Make your streams more virtual.",
                                                    Font = Spartan.Bold.With(size: 20),
                                                    Anchor = Anchor.BottomLeft,
                                                    Origin = Anchor.BottomLeft,
                                                    Margin = new MarginPadding { Left = 5 },
                                                    Colour = Colour4.White
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Child = new FillFlowContainer
                            {
                                Width = 0.9f,
                                Spacing = new Vector2(10),
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                Children = new Drawable[]
                                {
                                    new HomeButton
                                    {
                                        Url = "https://github.com/vignette-project/vignette",
                                        Text = "Vignette",
                                        Icon = VignetteFont.Logo,
                                        SubText = "Visit our website",
                                    },
                                    new HomeButton
                                    {
                                        Url = "https://github.com/vignette-project/vignette",
                                        Text = "GitHub",
                                        Icon = FontAwesome.Brands.Github,
                                        SubText = "We're open source!",
                                    },
                                    new HomeButton
                                    {
                                        Url = "https://github.com/vignette-project/vignette/issues/new",
                                        Text = "Report Issues",
                                        Icon = FluentSystemIcons.Bug24,
                                        SubText = "Send a bug report ticket",
                                    },
                                    new HomeButton
                                    {
                                        Url = "https://liberapay.com/holotrack/",
                                        Text = "Support Us",
                                        Icon = FluentSystemIcons.Heart24,
                                        SubText = "Support the project",
                                    },
                                    new HomeButton
                                    {
                                        Url = " https://discord.gg/3yMf3Y9",
                                        Text = "Discord",
                                        Icon = FontAwesome.Brands.Discord,
                                        SubText = "Join the community",
                                    },
                                    new HomeButton
                                    {
                                        Url = "https://twitter.com/ProjectVignette",
                                        Text = "Twitter",
                                        Icon = FontAwesome.Brands.Twitter,
                                        SubText = "Check us out on Social Media",
                                    },
                                },
                            }
                        }
                    }
                }
            };
        }

        private class HomeButton : FluentButtonBase
        {
            private ThemableMaskedBox border;

            private ThemableSpriteIcon icon;

            private ThemableSpriteText text;

            private ThemableSpriteText subText;

            public string Url { get; set; }

            public IconUsage Icon
            {
                get => icon.Icon;
                set => icon.Icon = value;
            }

            public LocalisableString Text
            {
                get => text.Text;
                set => text.Text = value;
            }

            public LocalisableString SubText
            {
                get => subText.Text;
                set => subText.Text = value;
            }

            public HomeButton()
            {
                Size = new Vector2(240, 80);
                BackgroundResting = ThemeSlot.Transparent;
                BackgroundHovered = ThemeSlot.Gray20;
                BackgroundPressed = ThemeSlot.Gray30;
                BackgroundDisabled = ThemeSlot.Transparent;
                Children = new Drawable[]
                {
                    border = new ThemableMaskedBox
                    {
                        BorderThickness = 3.0f,
                        RelativeSizeAxes = Axes.Both,
                        Colour = ThemeSlot.Transparent,
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding(15),
                        Children = new Drawable[]
                        {
                            icon = new ThemableSpriteIcon
                            {
                                Colour = ThemeSlot.AccentPrimary,
                                Size = new Vector2(28),
                            },
                            new FillFlowContainer
                            {
                                Margin = new MarginPadding { Left = 40 },
                                Direction = FillDirection.Vertical,
                                Children = new Drawable[]
                                {
                                    text = new ThemableSpriteText
                                    {
                                        Font = SegoeUI.Regular.With(size: 24),
                                        Colour = ThemeSlot.Black,
                                    },
                                    subText = new ThemableSpriteText
                                    {
                                        Font = SegoeUI.Regular.With(size: 16),
                                        Colour = ThemeSlot.Gray190,
                                    }
                                },
                            }
                        }
                    }
                };
            }

            protected override void UpdateBackground(ThemeSlot slot)
                => border.BorderColour = slot;

            [BackgroundDependencyLoader]
            private void load(GameHost host)
            {
                if (!string.IsNullOrEmpty(Url))
                    Action = () => host.OpenUrlExternally(Url);
            }
        }
    }
}
