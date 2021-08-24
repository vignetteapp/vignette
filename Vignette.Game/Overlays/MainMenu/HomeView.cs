// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Overlays.MainMenu.Home;

namespace Vignette.Game.Overlays.MainMenu
{
    public class HomeView : MenuView
    {
        public override LocalisableString Title => "Home";

        public override IconUsage Icon => FluentSystemIcons.Home24;

        public HomeView()
        {
            Children = new Drawable[]
            {
                new HomeBanner
                {
                    Height = 200,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Margin = new MarginPadding { Top = 150 },
                    Child = new FillFlowContainer
                    {
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
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            },
                            new HomeButton
                            {
                                Url = "https://github.com/vignette-project/vignette",
                                Text = "GitHub",
                                Icon = FontAwesome.Brands.Github,
                                SubText = "We're open source!",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            },
                            new HomeButton
                            {
                                Url = "https://github.com/vignette-project/vignette/issues/new",
                                Text = "Report Issues",
                                Icon = FluentSystemIcons.Bug24,
                                SubText = "Send a bug report ticket",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            },
                            new HomeButton
                            {
                                Url = "https://opencollective.com/vignette",
                                Text = "Support Us",
                                Icon = FluentSystemIcons.Heart24,
                                SubText = "Support the project",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            },
                            new HomeButton
                            {
                                Url = "https://discord.gg/3yMf3Y9",
                                Text = "Discord",
                                Icon = FontAwesome.Brands.Discord,
                                SubText = "Join the community",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            },
                            new HomeButton
                            {
                                Url = "https://twitter.com/ProjectVignette",
                                Text = "Twitter",
                                Icon = FontAwesome.Brands.Twitter,
                                SubText = "Check us out on Social Media",
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            },
                        },
                    }
                }
            };
        }
    }
}
