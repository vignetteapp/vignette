// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Backgrounds
{
    public class BackgroundScreenTheme : BackgroundScreen
    {
        public BackgroundScreenTheme()
        {
            AddInternal(new ThemableBox
            {
                RelativeSizeAxes = Axes.Both,
                Colour = ThemeSlot.White,
            });
        }
    }
}
