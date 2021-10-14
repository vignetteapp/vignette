// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using Vignette.Game.Screens;

namespace Vignette.Game.Tests.Visual.Screens
{
    public class TestSceneStageScreen : ScreenTestScene
    {
        public override void SetupSteps()
        {
            base.SetupSteps();
            AddStep("load screen", () => LoadScreen(new StageScreen()));
        }
    }
}
