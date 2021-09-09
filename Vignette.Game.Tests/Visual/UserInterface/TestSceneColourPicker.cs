// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osuTK;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneColourPicker : UserInterfaceTestScene
    {
        public TestSceneColourPicker()
        {
            Add(new FluentColourPicker());
        }
    }
}
