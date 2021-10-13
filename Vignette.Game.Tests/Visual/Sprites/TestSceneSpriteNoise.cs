// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osuTK;
using Vignette.Game.Graphics.Sprites;

namespace Vignette.Game.Tests.Visual.Sprites
{
    public class TestSceneSpriteNoise : VignetteTestScene
    {
        public TestSceneSpriteNoise()
        {
            Add(new SpriteNoise
            {
                Size = new Vector2(512),
                Resolution = new Vector2(50),
            });
        }
    }
}
