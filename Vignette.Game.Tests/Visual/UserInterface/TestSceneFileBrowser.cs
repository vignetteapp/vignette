// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneFileBrowser : UserInterfaceTestScene
    {
        public TestSceneFileBrowser()
        {
            Add(new FluentFileSelector
            {
                Width = 600,
                Height = 150,
            });

        }
    }
}
