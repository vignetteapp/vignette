// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osu.Framework.Testing;

namespace Vignette.Game.Tests.Visual.Screens
{
    public abstract class ScreenTestScene : VignetteManualInputManagerTestScene
    {
        protected readonly ScreenStack Stack;

        private readonly Container content;

        protected override Container<Drawable> Content => content;

        public ScreenTestScene()
        {
            base.Content.AddRange(new Drawable[]
            {
                Stack = new ScreenStack { RelativeSizeAxes = Axes.Both },
                content = new Container { RelativeSizeAxes = Axes.Both },
            });
        }

        protected void LoadScreen(Screen screen) => Stack.Push(screen);

        [SetUpSteps]
        public virtual void SetupSteps() => addExitAllScreensStep();

        [TearDownSteps]
        public virtual void TeardownSteps() => addExitAllScreensStep();

        private void addExitAllScreensStep() => AddUntilStep("exit all screens", () =>
        {
            if (Stack.CurrentScreen == null)
                return true;

            Stack.Exit();
            return false;
        });
    }
}
