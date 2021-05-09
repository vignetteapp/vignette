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

        [Resolved]
        private VignetteGameBase game { get; set; }

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
                    Colour = getGradient(),
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
                .FadeColour(getGradient(), 5000, Easing.InSine)
                .Then()
                .FadeColour(getGradient(true), 5000, Easing.OutSine)
                .Loop();
        }

        private static Colour4 grad_pub_a = Colour4.FromHex("BE58CB");
        private static Colour4 grad_pub_b = Colour4.FromHex("F10E5A");
        private static Colour4 grad_ins_a = Colour4.FromHex("58CB86");
        private static Colour4 grad_ins_b = Colour4.FromHex("0EBBF1");

        private ColourInfo getGradient(bool flipped = false)
        {
            if (game.IsInsidersBuild)
            {
                return flipped
                    ? ColourInfo.GradientHorizontal(grad_ins_b, grad_ins_a)
                    : ColourInfo.GradientHorizontal(grad_ins_a, grad_ins_b);
            }
            else
            {
                return flipped
                    ? ColourInfo.GradientHorizontal(grad_pub_b, grad_pub_a)
                    : ColourInfo.GradientHorizontal(grad_pub_a, grad_pub_b);
            }
        }
    }
}
