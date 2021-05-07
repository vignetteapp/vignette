// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Vignette.Game.Themeing;

namespace Vignette.Game.Tests.Visual
{
    public abstract class ThemeProvidedTestScene : VignetteTestScene
    {
        [Cached(typeof(IThemeSource))]
        protected readonly TestThemeSource Provider;

        protected override Container<Drawable> Content => Provider;

        public ThemeProvidedTestScene()
        {
            base.Content.Add(Provider = new TestThemeSource
            {
                RelativeSizeAxes = Axes.Both
            });
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
