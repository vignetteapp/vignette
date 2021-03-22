// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Testing;
using osuTK;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneColourPicker : TestScene
    {
        public TestSceneColourPicker()
        {
            Add(new ColourPicker
            {
                Margin = new MarginPadding(20),
            });
        }
    }
}
