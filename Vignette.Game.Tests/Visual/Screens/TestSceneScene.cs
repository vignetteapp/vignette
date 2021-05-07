// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Game.Screens.Scene;

namespace Vignette.Game.Tests.Visual.Screens
{
    public class TestSceneScene : ScreenTestScene
    {
        public override void SetupSteps()
        {
            base.SetupSteps();
            AddStep("load scene", () => LoadScreen(new SceneScreen()));
        }
    }
}
