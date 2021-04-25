// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Themeing
{
    public interface IThemable<T> : IDrawable
        where T : IDrawable
    {
        T Target { get; }

        ThemeSlot Colour { get; set; }
    }
}
