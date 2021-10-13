// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using Vignette.Game.Graphics.Shapes;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneAcrylicBox : ThemeProvidedTestScene
    {
        private readonly BufferedContainer blurTarget;
        private readonly AcrylicBox background;
        private readonly Sprite sprite;

        public TestSceneAcrylicBox()
        {
            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    blurTarget = new BufferedContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = sprite = new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Size = new Vector2(512),
                            Position = new Vector2(128),
                        },
                    },
                    background = new AcrylicBox
                    {
                        Size = new Vector2(512),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Target = blurTarget,
                    },
                },
            });

            AddSliderStep("blur", 0.0f, 50.0f, 10.0f, b => background.BlurSigma = new Vector2(b));
            AddSliderStep("luminosity", 0.0f, 1.0f, 1.0f, l => background.Luminosity = l);
            AddSliderStep("tint alpha", 0.1f, 0.9f, 0.6f, l => background.TintAlpha = l);
            AddSliderStep("luminosity alpha ", 0.1f, 0.9f, 0.6f, l => background.LuminosityAlpha = l);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            sprite.Texture = textures.Get("test-wallpaper");
            sprite.Spin(5000, RotationDirection.Clockwise).Loop();
        }
    }
}
