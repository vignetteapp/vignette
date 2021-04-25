// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.Shapes
{
    public class ThemableMaskedBox : ThemableDrawable<Container>
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

        public ThemableMaskedBox(bool attached = true)
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
        }
    }
}
