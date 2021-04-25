// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Bindables;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public class TestSceneNumberBox : UserInterfaceTestScene
    {
        private readonly FluentNumberBox<float> numberBox;

        public TestSceneNumberBox()
        {
            Add(numberBox = new FluentNumberBox<float>
            {
                Width = 100,
                Current = new BindableFloat
                {
                    Default = 0,
                    Precision = 0.1f,
                }
            });

            Add(new FluentNumberBox<int>
            {
                Width = 100,
                Current = new BindableInt
                {
                    Disabled = true,
                }
            });

            AddStep("increment", () => numberBox.Increment());
            AddAssert("check value", () => numberBox.Current.Value == 0.1f);
            AddStep("set value to default", () => numberBox.Current.SetDefault());
            AddAssert("check value", () => numberBox.Current.Value == 0);
            AddStep("decrement", () => numberBox.Decrement());
            AddAssert("check value", () => numberBox.Current.Value == -0.1f);
        }
    }
}
