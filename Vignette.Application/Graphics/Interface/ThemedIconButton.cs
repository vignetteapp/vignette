// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Graphics.Interface
{
    public class ThemedIconButton : ThemedButton
    {
        private ThemedSpriteIcon spriteIcon;

        public Vector2 IconSize
        {
            get => spriteIcon.Size;
            set => spriteIcon.Size = value;
        }

        public IconUsage Icon
        {
            get => spriteIcon.Icon;
            set => spriteIcon.Icon = value;
        }

        public Axes IconRelativeSizeAxes
        {
            get => spriteIcon.RelativeSizeAxes;
            set => spriteIcon.RelativeSizeAxes = value;
        }

        protected override Drawable CreateLabel() => spriteIcon = new ThemedSpriteIcon
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
        };
    }
}
