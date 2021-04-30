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
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Screens.Menu.Home
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
    }
}
