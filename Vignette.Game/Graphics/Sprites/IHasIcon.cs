// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Vignette.Game.Graphics.Sprites
{
    public interface IHasIcon : IDrawable
    {
        IconUsage Icon { get; set; }
    }
}
