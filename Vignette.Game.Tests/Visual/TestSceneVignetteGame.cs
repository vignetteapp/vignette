// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Platform;
using System.ComponentModel;

namespace Vignette.Game.Tests.Visual
{
    [RequiresOpenCVRuntime]
    [Description("The full Vignette experience.")]
    public class TestSceneVignetteGame : VignetteTestScene
    {
        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            var game = new VignetteGame();
            game.SetHost(host);
            Add(game);
        }
    }
}
