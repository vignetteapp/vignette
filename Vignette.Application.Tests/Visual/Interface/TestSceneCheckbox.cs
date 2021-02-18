// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneCheckbox : TestSceneInterface
    {
        public TestSceneCheckbox()
        {
            AddComponent(new VignetteCheckbox
            {
                Text = @"Checkbox",
            });
        }
    }
}
