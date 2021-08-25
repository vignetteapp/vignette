// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Game.Screens;

namespace Vignette.Game.Tests.Visual.Screens
{
    public class TestSceneSplashScreen : ScreenTestScene
    {
        public override void SetupSteps()
        {
            base.SetupSteps();
            AddStep("load screen", () => LoadScreen(new SplashScreen()));
        }
    }
}
