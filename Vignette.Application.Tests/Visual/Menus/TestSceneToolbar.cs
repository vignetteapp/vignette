// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Testing;
using Vignette.Application.Screens.Main;

namespace Vignette.Application.Tests.Visual.Menus
{
    public class TestSceneToolbar : TestScene
    {
        public TestSceneToolbar()
        {
            var toolbar = new Toolbar();

            Add(toolbar);
            AddStep("toggle toolbar", () => toolbar.ToggleVisibility());
        }
    }
}
