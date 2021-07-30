// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Game.Screens.Main;

namespace Vignette.Game.Tests.Visual.Screens
{
    public class TestSceneMainScreen : ScreenTestScene
    {
        public override void SetupSteps()
        {
            base.SetupSteps();
            AddStep("load main menu", () => LoadScreen(new MainScreen()));
        }
    }
}
