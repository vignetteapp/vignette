// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Testing;
using osuTK;
using Vignette.Game.Graphics.Sprites;

namespace Vignette.Game.Tests.Visual.Sprites
{
    public class TestSceneSpriteNoise : TestScene
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
