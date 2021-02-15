// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Graphics.Interface
{
    public class ButtonText : VignetteButton, IHasText
    {
        private VignetteSpriteText spriteText;

        public string Text
        {
            get => spriteText.Text;
            set => spriteText.Text = value.ToUpperInvariant();
        }

        public ButtonText()
        {
            Height = 35;
        }

        protected override Drawable CreateLabel() => spriteText = new VignetteSpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Font = VignetteFont.SemiBold.With(size: 16),
        };
    }
}
