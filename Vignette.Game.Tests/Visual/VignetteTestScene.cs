// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using Vignette.Game.Themeing;

namespace Vignette.Game.Tests.Visual
{
    public abstract class VignetteTestScene : TestScene
    {
        [Cached(typeof(IThemeSource))]
        protected readonly TestThemeSource Provider;

        protected override Container<Drawable> Content => Provider;

        public VignetteTestScene()
        {
            base.Content.Add(Provider = new TestThemeSource
            {
                RelativeSizeAxes = Axes.Both
            });
        }

        protected override ITestSceneTestRunner CreateRunner() => new VignetteTestSceneTestRunner();

        public class VignetteTestSceneTestRunner : VignetteGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }

        protected class TestThemeSource : Container, IThemeSource
        {
            public event Action SourceChanged;

            public readonly Bindable<Theme> Current = new Bindable<Theme>(Theme.Light);

            public TestThemeSource()
            {
                Current.BindValueChanged(_ => SourceChanged?.Invoke(), true);
            }

            public Theme GetCurrent() => Current.Value;
        }
    }
}
