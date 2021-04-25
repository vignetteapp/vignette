// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneSlider : UserInterfaceTestScene
    {
        public TestSceneSlider()
        {
            FluentSlider<int> disabled;

            AddRange(new Drawable[]
            {
                new FluentSlider<int>
                {
                    Width = 200,
                    Current = new BindableInt
                    {
                        Value = 3,
                        MinValue = 0,
                        MaxValue = 10,
                    }
                },
                disabled = new FluentSlider<int>
                {
                    Width = 200,
                    Current = new BindableInt
                    {
                        Value = 3,
                        MinValue = 0,
                        MaxValue = 10,
                    }
                },
            });

            disabled.Current.Disabled = true;
        }
    }
}
