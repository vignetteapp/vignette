// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Bindables;

namespace Vignette.Game.Graphics.Themeing
{
    /// <summary>
    /// An interface denoting that this is capable of providing a theme to <see cref="IThemable{T}"/>s.
    /// </summary>
    public interface IThemeSource
    {
        Action ThemeChanged { get; set; }

        Bindable<Theme> Current { get; }
    }
}
