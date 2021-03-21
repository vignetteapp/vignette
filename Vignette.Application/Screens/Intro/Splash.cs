// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Screens;
using osuTK;
using Vignette.Application.Graphics;

namespace Vignette.Application.Screens.Intro
{
    public class Splash : Screen
    {
        private TextFlowContainer disclaimer;

        private FillFlowContainer branding;

        private readonly Screen nextScreen;

        public Splash(Screen screen = null)
        {
            nextScreen = screen;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChildren = new Drawable[]
            {
                new FillFlowContainer
                {
                    Width = 400,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Spacing = new Vector2(0, 20),
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        branding = new FillFlowContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(20, 0),
                            Children = new Drawable[]
                            {
                                new Sprite
                                {
                                    Texture = textures.Get("branding"),
                                    Size = new Vector2(64),
                                },
                                new Container
                                {
                                    AutoSizeAxes = Axes.X,
                                    RelativeSizeAxes = Axes.Y,
                                    Child = new SpriteText
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Font = Spartan.Bold.With(size: 56),
                                        Text = "Vignette",
                                    },
                                }
                            }
                        },
                        disclaimer = new TextFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            TextAnchor = Anchor.TopCentre,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        }
                    }
                }
            };

            disclaimer.AddText("This project is ", t => t.Font = SegoeUI.Regular.With(size: 16));
            disclaimer.AddText("under heavy development", t => t.Font = SegoeUI.Bold.With(size: 16));
            disclaimer.AddText(".", t => t.Font = SegoeUI.Regular.With(size: 16));
            disclaimer.NewParagraph();
            disclaimer.AddText("Please direct all bug and crash reports to our GitHub repository.", t => t.Font = SegoeUI.Regular.With(size: 16));
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            branding.Scale = new Vector2(0.98f);
            disclaimer.Scale = new Vector2(0.98f);
            
            branding
                .FadeInFromZero(1000)
                .ScaleTo(1, 1000, Easing.OutCubic);

            
            disclaimer
                .FadeInFromZero(2000)
                .ScaleTo(1, 2000, Easing.OutCubic);

            Scheduler.AddDelayed(() =>
            {
                if (nextScreen != null)
                    this.Push(nextScreen);
            }, 5000);
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);
            this.FadeOutFromOne(500, Easing.OutQuint);
        }
    }
}
