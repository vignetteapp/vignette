// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Shapes
{
    public class ThemableEffectBox : ThemableContainer<Box>
    {
        public ThemableEffectBox()
        {
            Target.Add(new Box { RelativeSizeAxes = Axes.Both });
        }

        protected override void ThemeChanged(Theme theme)
        {
            base.ThemeChanged(theme);
            Target.Child.Colour = theme.Get(Colour);
        }
    }
}
