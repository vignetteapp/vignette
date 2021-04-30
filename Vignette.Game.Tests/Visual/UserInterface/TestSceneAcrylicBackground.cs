// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Themeing;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneAcrylicBackground : UserInterfaceTestScene
    {
        private BufferedContainer blurTarget;

        private AcrylicBox background;

        public TestSceneAcrylicBackground()
        {
            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    blurTarget = new BufferedContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = new Box
                        {
                            Anchor = Anchor.Centre,
                            Colour = Colour4.Green,
                            Size = new Vector2(512),
                        },
                    },
                    background = new AcrylicBox
                    {
                        Size = new Vector2(512),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = ThemeSlot.AccentPrimary,
                        Target = blurTarget,
                    },
                },
            });

            AddSliderStep<float>("luminosity", 0.0f, 1.0f, 1.0f, l => background.Luminosity = l);
            AddSliderStep<float>("tint alpha", 0.1f, 0.9f, 0.6f, l => background.TintAlpha = l);
            AddSliderStep<float>("luminosity alpha ", 0.1f, 0.9f, 0.6f, l => background.LuminosityAlpha = l);
        }
    }
}
