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
        private MainMenuOverlay mainMenu;
        private Box background;
        private Bindable<Colour4> colour;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            colour = config.GetBindable<Colour4>(VignetteSetting.BackgroundColour);

            AddInternal(background = new Box { RelativeSizeAxes = Axes.Both });

            colour.BindValueChanged(e => background.Colour = e.NewValue, true);

            LoadComponentAsync(mainMenu = new MainMenuOverlay(), _ => AddInternal(mainMenu));
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

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            this.FadeOut().Delay(500).FadeInFromZero(500, Easing.OutQuint);
        }

        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new FluentMenuItem("Home", () => mainMenu.SelectTab<HomeView>()),
        };
    }
}
