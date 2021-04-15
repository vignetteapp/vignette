// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;

namespace Vignette.Game.Graphics.Themes
{
    public class ThemeProvidingContainer : Container
    {
        [Cached]
        [Cached(typeof(IBindable<Theme>))]
        public readonly Bindable<Theme> Current = new Bindable<Theme>(Theme.Light);
    }
}
