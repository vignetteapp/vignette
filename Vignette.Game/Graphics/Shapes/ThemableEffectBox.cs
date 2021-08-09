// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.Shapes
{
    public class ThemableEffectBox : ThemableDrawable<Container>
    {
        private ThemeSlot borderColour;

        public new ThemeSlot BorderColour
        {
            get => borderColour;
            set
            {
                if (borderColour == value)
                    return;

                borderColour = value;
                ScheduleThemeChange();
            }
        }

        public new float BorderThickness
        {
            get => Target.BorderThickness;
            set => Target.BorderThickness = value;
        }

        public new float CornerRadius
        {
            get => Target.CornerRadius;
            set => Target.CornerRadius = value;
        }

        private bool shadow;

        public bool Shadow
        {
            get => shadow;
            set
            {
                if (shadow == value)
                    return;

                shadow = value;
                ScheduleThemeChange();
            }
        }

        private float shadowRadius = 5;

        public float ShadowRadius
        {
            get => shadowRadius;
            set
            {
                if (shadowRadius == value)
                    return;

                shadowRadius = value;
                ScheduleThemeChange();
            }
        }

        private float shadowAlpha = 0.1f;

        public float ShadowAlpha
        {
            get => shadowAlpha;
            set
            {
                if (shadowAlpha == value)
                    return;

                shadowAlpha = value;
                ScheduleThemeChange();
            }
        }

        public ThemableEffectBox(bool attached = true)
            : base(attached)
        {
        }

        protected override Container CreateDrawable() => new Container
        {
            RelativeSizeAxes = Axes.Both,
            Masking = true,
            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
            },
        };

        protected override void ThemeChanged(Theme theme)
        {
            Target.Child.Colour = theme.GetColour(Colour);
            Target.BorderColour = theme.GetColour(BorderColour);

            if (!shadow)
                return;

            Target.EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Offset = new Vector2(0, 2),
                Colour = theme.GetColour(ThemeSlot.Black).Opacity(shadowAlpha),
                Hollow = true,
                Radius = shadowRadius,
                Roundness = 5,
            };
        }
    }
}
