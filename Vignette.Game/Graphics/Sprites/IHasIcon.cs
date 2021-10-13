// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Vignette.Game.Graphics.Sprites
{
    public interface IHasIcon : IDrawable
    {
        IconUsage Icon { get; set; }
    }
}
