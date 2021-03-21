// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneDropdown : TestScene
    {
        public TestSceneDropdown()
        {
            Add(new ThemedDropdown<ThemeColour>
            {
                Width = 200,
                Items = Enum.GetValues<ThemeColour>(),
                Margin = new MarginPadding(10),
            });
        }
    }
}
