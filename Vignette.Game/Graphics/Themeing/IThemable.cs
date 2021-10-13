// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Graphics.Themeing
{
    /// <summary>
    /// An interface denoting that this drawable is capable to be themed by <see cref="IThemeSource"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Drawable"/> to be themed.</typeparam>
    public interface IThemable<T> : IDrawable
        where T : IDrawable
    {
        T Target { get; }

        ThemeSlot Colour { get; set; }
    }
}
