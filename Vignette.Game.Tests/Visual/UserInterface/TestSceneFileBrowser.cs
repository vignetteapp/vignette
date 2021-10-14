// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
