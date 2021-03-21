// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Testing;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneCheckbox : TestScene
    {
        public TestSceneCheckbox()
        {
            Add(new ThemedCheckbox
            {
                Text = @"Checkbox",
                Margin = new MarginPadding(10),
            });
        }
    }
}
