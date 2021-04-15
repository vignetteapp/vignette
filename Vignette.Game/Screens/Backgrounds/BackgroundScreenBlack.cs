// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace Vignette.Game.Screens.Backgrounds
{
    public class BackgroundScreenBlack : BackgroundScreen
    {
        public BackgroundScreenBlack()
        {
            InternalChild = new Box
            {
                Colour = Colour4.Black,
                RelativeSizeAxes = Axes.Both,
            };
        }
    }
}
