// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Tests.Visual.Interface
{
    public class TestSceneSliderBar : TestScene
    {
        public TestSceneSliderBar()
        {
            Add(new ThemedSliderBar<float>
            {
                Width = 200,
                Margin = new MarginPadding(10),
                Current = new BindableFloat
                {
                    MinValue = 0,
                    MaxValue = 1,
                }
            });
        }
    }
}
