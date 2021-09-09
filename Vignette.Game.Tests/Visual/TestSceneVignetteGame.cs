// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Platform;
using System.ComponentModel;

namespace Vignette.Game.Tests.Visual
{
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
