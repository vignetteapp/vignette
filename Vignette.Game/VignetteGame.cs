// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Screens;
using Vignette.Game.Screens.Menu;

namespace Vignette.Game
{
    public class VignetteGame : VignetteGameBase
    {
        public VignetteGame()
        {
            Name = @"Vignette";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new ScreenStack(new MainMenu()));
        }
    }
}
