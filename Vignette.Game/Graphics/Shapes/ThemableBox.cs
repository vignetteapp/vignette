// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Shapes
{
    public class ThemableBox : ThemableDrawable<Box>
    {
        public ThemableBox(bool attached = true)
            : base(attached)
        {
        }

        protected override Box CreateDrawable() => new Box { RelativeSizeAxes = Axes.Both };
    }
}
