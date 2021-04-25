// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Sprites;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.Sprites
{
    public class ThemableSprite : ThemableDrawable<Sprite>
    {
        public ThemableSprite(bool attached = true)
            : base(attached)
        {
        }

        protected override Sprite CreateDrawable() => new Sprite();
    }
}
