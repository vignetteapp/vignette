// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneSliderBar : TestSceneInterface
    {
        public TestSceneSliderBar()
        {
            AddComponent(new VignetteSliderBar<float>
            {
                Width = 200,
                Current = new BindableFloat
                {
                    MinValue = 0,
                    MaxValue = 1,
                }
            });
        }
    }
}
