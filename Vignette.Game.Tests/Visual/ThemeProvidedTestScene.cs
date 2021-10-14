// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Tests.Visual
{
    public abstract class ThemeProvidedTestScene : VignetteTestScene
    {
        protected readonly ThemeProvidingContainer Provider;

        protected override Container<Drawable> Content => Provider;

        protected readonly BasicDropdown<Theme> Selector;

        public ThemeProvidedTestScene()
        {
            base.Content.AddRange(new Drawable[]
            {
                Provider = new ThemeProvidingContainer
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

            Selector.Current = Provider.Current;
        }
    }
}
