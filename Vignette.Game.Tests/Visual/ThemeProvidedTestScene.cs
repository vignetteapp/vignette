// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;
using Vignette.Game.Themeing;

namespace Vignette.Game.Tests.Visual
{
    public abstract class ThemeProvidedTestScene : TestScene
    {
        [Cached(typeof(IThemeSource))]
        private readonly TestThemeSource content;

        protected override Container<Drawable> Content => content;

        protected readonly BasicDropdown<Theme> Selector;

        protected ThemeProvidedTestScene()
        {
            base.Content.AddRange(new Drawable[]
            {
                content = new TestThemeSource
                {
                    RelativeSizeAxes = Axes.Both
                },
                Selector = new BasicDropdown<Theme>
                {
                    Width = 200,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Margin = new MarginPadding(20),
                    Items = new[]
                    {
                        Theme.Light,
                        Theme.Dark,
                    }
                },
            });

            Selector.Current = content.Current;
        }

        private class TestThemeSource : Container, IThemeSource
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
