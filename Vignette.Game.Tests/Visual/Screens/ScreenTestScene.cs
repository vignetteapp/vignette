// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osu.Framework.Testing;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Screens;

namespace Vignette.Game.Tests.Visual.Screens
{
    public abstract class ScreenTestScene : VignetteManualInputManagerTestScene
    {
        protected readonly VignetteScreenStack Stack;

        private readonly Container content;

        protected override Container<Drawable> Content => content;

        public ScreenTestScene()
        {
            base.Content.AddRange(new Drawable[]
            {
                new FluentContextMenuContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new FluentTooltipContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = Stack = new VignetteScreenStack { RelativeSizeAxes = Axes.Both },
                    },
                },
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
