// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Vignette.Game.Screens
{
    public class VignetteScreenStack : ScreenStack
    {
        // [Cached]
        // private BackgroundScreenStack backgroundStack;

        public VignetteScreenStack()
        {
            // InternalChild = backgroundStack = new BackgroundScreenStack { RelativeSizeAxes = Axes.Both };
        }
    }
}
