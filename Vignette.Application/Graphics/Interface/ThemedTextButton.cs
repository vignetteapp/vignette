// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Graphics.Interface
{
    public class ThemedTextButton : ThemedButton, IHasText
    {
        private ThemedSpriteText spriteText;

        public LocalisableString Text
        {
            get => spriteText.Text;
            set => spriteText.Text = value;
        }

        public ThemedTextButton()
        {
            Height = 25;
        }

        protected override Drawable CreateLabel() => spriteText = new ThemedSpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Font = SegoeUI.Regular.With(size: 16),
        };
    }
}
