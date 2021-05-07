// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Screens.Menu.Home
{
    public class HomeBanner : Container
    {
        private Box background;

        private static Colour4 gradient_part_a = Colour4.FromHex("BE58CB");

        private static Colour4 gradient_part_b = Colour4.FromHex("F10E5A");

        public HomeBanner()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ColourInfo.GradientHorizontal(gradient_part_a, gradient_part_b),
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
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            background
                .FadeColour(ColourInfo.GradientHorizontal(gradient_part_b, gradient_part_a), 5000, Easing.InSine)
                .Then()
                .FadeColour(ColourInfo.GradientHorizontal(gradient_part_a, gradient_part_b), 5000, Easing.OutSine)
                .Loop();
        }
    }
}
