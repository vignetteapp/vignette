// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Screens;

namespace Vignette.Game.Screens
{
    public class BackgroundScreenStack : ScreenStack
    {
        public BackgroundScreenStack(IScreen baseScreen, bool suspendImmediately = true)
            : base(baseScreen, suspendImmediately)
        {
        }
    }
}
