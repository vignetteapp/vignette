// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class ButtonIcon : VignetteButton
    {
        protected new SpriteIcon Label => base.Label as SpriteIcon;

        public IconUsage Icon
        {
            get => Label.Icon;
            set => Label.Icon = value;
        }

        protected override Drawable CreateLabel() => new SpriteIcon
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Size = new Vector2(16),
        };
    }
}
