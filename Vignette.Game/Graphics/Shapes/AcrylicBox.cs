// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.Shapes
{
    public class AcrylicBox : CompositeDrawable
    {
        private BufferedContainer blur;

        private Box luminosityBox;

        private readonly ThemableBox tint;

        private readonly SpriteNoise noise;

        public new ThemeSlot Colour
        {
            get => tint.Colour;
            set => tint.Colour = value;
        }

        public float TintAlpha
        {
            get => tint.Alpha;
            set => tint.Alpha = value;
        }

        private float luminosity;

        public float Luminosity
        {
            get => luminosity;
            set
            {
                luminosity = value;
                luminosityBox.Colour = Colour4.White.Darken(1.0f - value);
            }
        }

        public float LuminosityAlpha
        {
            get => luminosityBox.Alpha;
            set => luminosityBox.Alpha = value;
        }

        private BufferedContainer target;

        public BufferedContainer Target
        {
            get => target;
            set
            {
                if (target == value)
                    return;

                target = value;

                blur.Child = target.CreateView().With(d =>
                {
                    d.RelativeSizeAxes = Axes.Both;
                    d.SynchronisedDrawQuad = true;
                });
            }
        }

        public AcrylicBox()
        {
            InternalChildren = new Drawable[]
            {
                blur = new BufferedContainer
                {
                    Name = "Guassian Blur",
                    RelativeSizeAxes = Axes.Both,
                    BlurSigma = new Vector2(100),
                },
                tint = new ThemableBox
                {
                    Name = "Tint",
                    RelativeSizeAxes = Axes.Both,
                },
                luminosityBox = new Box
                {
                    Name = "Luminance",
                    RelativeSizeAxes = Axes.Both,
                },
                noise = new SpriteNoise
                {
                    Name = "Noise",
                    RelativeSizeAxes = Axes.Both,
                    Resolution = new Vector2(30),
                    Alpha = 0.02f,
                },
            };
        }
    }
}
