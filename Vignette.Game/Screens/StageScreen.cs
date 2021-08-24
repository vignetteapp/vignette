// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Input;
using Vignette.Game.Overlays;
using Vignette.Game.Overlays.MainMenu;
using Vignette.Game.Overlays.MainMenu.Settings;

namespace Vignette.Game.Screens
{
    public class StageScreen : Screen, IHasContextMenu, IKeyBindingHandler<GlobalAction>
    {
        private readonly MainMenuOverlay mainMenu;
        private Box background;
        private Bindable<Colour4> colour;

        public StageScreen()
        {
            InternalChild = mainMenu = new MainMenuOverlay();
            mainMenu.Show();
        }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);

            AddInternal(background = new Box
            {
                Depth = 1,
                RelativeSizeAxes = Axes.Both,
            });

            colour.BindValueChanged(e => background.Colour = e.NewValue, true);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleMainMenu:
                    mainMenu?.ToggleVisibility();
                    return true;

                case GlobalAction.OpenSettings:
                    mainMenu?.SelectTab<GameSettings>();
                    return true;
            }

            return false;
        }

        public void OnReleased(GlobalAction action)
        {
        }

        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new FluentMenuItem("Home", () => mainMenu.SelectTab<HomeView>()),
        };
    }
}
