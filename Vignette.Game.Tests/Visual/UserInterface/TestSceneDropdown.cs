// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
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
                Width = 200,
                Current = new Bindable<string>
                {
                    Value = "Another Item",
                    Disabled = true,
                },
                Items = new[]
                {
                    "Hello World",
                    "Another Item",
                    "Really Long Option",
                },
            });

            Add(new FluentDropdown<string>
            {
                Width = 200,
                Style = DropdownStyle.Underlined,
                Items = new[]
                {
                    "Hello World",
                    "Another Item",
                    "Really Long Option",
                },
            });

            Add(new FluentDropdown<string>
            {
                Width = 200,
                Style = DropdownStyle.Underlined,
                Current = new Bindable<string>
                {
                    Value = "Another Item",
                    Disabled = true,
                },
                Items = new[]
                {
                    "Hello World",
                    "Another Item",
                    "Really Long Option",
                },
            });
        }
    }
}
