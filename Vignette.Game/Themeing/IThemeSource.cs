// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;

namespace Vignette.Game.Themeing
{
    /// <summary>
    /// An interface denoting that this is capable of providing a theme to <see cref="IThemable{T}"/>s.
    /// </summary>
    public interface IThemeSource
    {
        event Action ThemeChanged;

        Theme Current { get; }
    }
}
