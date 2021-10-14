// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Shapes
{
    public class ThemableCircle : ThemableDrawable<Circle>
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

        public ThemableCircle(bool attached = true)
            : base(attached)
        {
        }

        protected override Circle CreateDrawable() => new Circle
        {
            RelativeSizeAxes = Axes.Both,
        };

        protected override void ThemeChanged(Theme theme)
        {
            Target.Child.Colour = theme.Get(Colour);
            Target.BorderColour = theme.Get(BorderColour);
        }
    }
}
