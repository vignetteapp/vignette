// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Sprites
{
    public class ThemableSpriteIcon : ThemableDrawable<SpriteIcon>, IHasIcon
    {
        public IconUsage Icon
        {
            get => Target.Icon;
            set => Target.Icon = value;
        }

        public ThemableSpriteIcon(bool attached = true)
            : base(attached)
        {
        }

        protected override SpriteIcon CreateDrawable() => new SpriteIcon
        {
            RelativeSizeAxes = Axes.Both,
        };
    }
}
