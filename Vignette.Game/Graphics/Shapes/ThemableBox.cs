// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
