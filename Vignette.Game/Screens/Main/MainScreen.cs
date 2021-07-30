// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using Vignette.Game.Input;
using Vignette.Game.Screens.Main.Menu;
using Vignette.Game.Screens.Main.Scene;

namespace Vignette.Game.Screens.Main
{
    public class MainScreen : Screen, IKeyBindingHandler<GlobalAction>
    {
        [Cached]
        private readonly MainMenu mainMenu;

        public MainScreen()
        {
            InternalChildren = new Drawable[]
            {
                new SceneView { RelativeSizeAxes = Axes.Both },
                mainMenu = new MainMenu { RelativeSizeAxes = Axes.Both },
            };

            mainMenu.Show();
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleMainMenu:
                    mainMenu?.ToggleVisibility();
                    return true;
            }

            return false;
        }

        public void OnReleased(GlobalAction action)
        {
        }
    }
}
