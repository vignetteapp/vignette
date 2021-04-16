// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneSlider : ThemeProvidedTestScene
    {
        public TestSceneSlider()
        {
            Add(new VignetteSliderBar<float>
            {
                Width = 200,
                Margin = new MarginPadding(10),
                Current = new BindableFloat
                {
                    MinValue = 0,
                    MaxValue = 1,
                    Value = 0,
                },
            });
        }
    }
}
