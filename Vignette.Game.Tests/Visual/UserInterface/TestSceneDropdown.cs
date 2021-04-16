// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneDropdown : ThemeProvidedTestScene
    {
        public TestSceneDropdown()
        {
            Add(new VignetteDropdown<string>
            {
                Width = 200,
                Items = new[] { "One", "Two", "Three" },
            });
        }
    }
}
