// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneDropdown : TestSceneInterface
    {
        public TestSceneDropdown()
        {
            AddComponent(new VignetteDropdown<ThemeColour>
            {
                Width = 200,
                Items = Enum.GetValues<ThemeColour>(),
            });
        }
    }
}
