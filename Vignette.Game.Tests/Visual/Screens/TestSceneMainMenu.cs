// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using NUnit.Framework;
using Vignette.Game.Screens.Menu;
using Vignette.Game.Screens.Menu.Help;

namespace Vignette.Game.Tests.Visual.Screens
{
    public class TestSceneMainMenu : ScreenTestScene
    {
        private MainMenu menu;

        public override void SetupSteps()
        {
            base.SetupSteps();
            AddStep("load main menu", () => LoadScreen(menu = new MainMenu()));
        }

        [Test]
        public void TestMenuNavigationControls()
        {
            AddStep("toggle side panel", () => menu.ToggleNavigationView());
            AddStep("select help tab", () => menu.SelectTab<HelpPage>());
        }
    }
}
