// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Framework.Testing;

namespace Vignette.Application.Tests.Visual
{
    public abstract class ScreenTestScene : TestScene
    {
        protected readonly ScreenStack Stack;

        public ScreenTestScene()
        {
            Add(Stack = new ScreenStack { RelativeSizeAxes = Axes.Both });
        }

        protected void LoadScreen(Screen screen) => Stack.Push(screen);
    }
}
