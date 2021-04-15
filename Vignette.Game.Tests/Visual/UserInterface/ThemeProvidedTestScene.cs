// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using Vignette.Game.Graphics.Themes;

namespace Vignette.Game.Tests.Visual.UserInterface
{
    public abstract class ThemeProvidedTestScene : TestScene
    {
        protected override Container<Drawable> Content => ThemeProvider;

        protected ThemeProvidingContainer ThemeProvider { get; }

        protected ThemeProvidedTestScene()
        {
            base.Content.Add(ThemeProvider = new ThemeProvidingContainer
            {
                RelativeSizeAxes = Axes.Both,
            });
        }
    }
}
