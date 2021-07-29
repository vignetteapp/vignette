// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Screens.Menu;
using Vignette.Game.Themeing;

namespace Vignette.Game
{
    public class VignetteGame : VignetteGameBase
    {
        private ScreenStack screenStack;

        public VignetteGame()
        {
            Name = @"Vignette";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRange(new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.White,
                },
                screenStack = new ScreenStack(),
            });

            screenStack.Push(new MainMenu());
        }
    }
}
