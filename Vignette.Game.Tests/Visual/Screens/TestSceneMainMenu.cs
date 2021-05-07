// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Game.Screens.Menu;

namespace Vignette.Game.Tests.Visual.Screens
{
    public class TestSceneMainMenu : ScreenTestScene
    {
        private MainMenu menu;

        public override void SetupSteps()
        {
            base.SetupSteps();
            AddStep("load main menu", () => LoadScreen(menu = new MainMenu()));
            AddStep("toggle side panel", () => menu.ToggleNavigationView());
            AddStep("select settings tab", () => menu.SelectTab(typeof(GameSettingScreen)));
        }
    }
}
