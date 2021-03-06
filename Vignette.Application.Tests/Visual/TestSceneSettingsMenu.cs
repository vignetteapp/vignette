// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Testing;
using Vignette.Application.Configuration.Settings;

namespace Vignette.Application.Tests.Visual
{
    public class TestSceneSettingsMenu : TestScene
    {
        public TestSceneSettingsMenu()
        {
            Add(new SettingsMenu { RelativeSizeAxes = Axes.Both });
        }
    }
}
