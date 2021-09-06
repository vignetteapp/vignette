// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Game.Settings;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneMainMenu : ThemeProvidedTestScene
    {
        public TestSceneMainMenu()
        {
            Add(new SettingsOverlay());
        }
    }
}
