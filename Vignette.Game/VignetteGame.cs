// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using Vignette.Game.Screens;
using Vignette.Game.Screens.Menu;

namespace Vignette.Game
{
    public class VignetteGame : VignetteGameBase
    {
        private VignetteScreenStack screenStack;

        public VignetteGame()
        {
            Name = @"Vignette";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(screenStack = new VignetteScreenStack());
            screenStack.Push(new MainMenu());
        }
    }
}
