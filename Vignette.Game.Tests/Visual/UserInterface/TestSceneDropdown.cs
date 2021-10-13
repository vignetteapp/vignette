// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneDropdown : UserInterfaceTestScene
    {
        public TestSceneDropdown()
        {
            Add(new FluentDropdown<string>
            {
                Width = 200,
                Items = new[]
                {
                    "Hello World",
                    "Another Item",
                    "Really Long Option",
                },
            });
            Add(new FluentDropdown<string>
            {
                Width = 100,
                Items = new[]
                {
                    "Hello World",
                    "Another Item",
                    "Really Long Option",
                    "An even more loooooooooooooooooooonger option",
                },
            });
        }
    }
}
