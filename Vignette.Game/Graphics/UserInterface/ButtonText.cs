// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Graphics.UserInterface
{
    public class ButtonText : VignetteButton, IHasText
    {
        protected new SpriteText Label => base.Label as SpriteText;

        public LocalisableString Text
        {
            get => Label.Text;
            set => Label.Text = value;
        }

        protected override Drawable CreateLabel() => new SpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Font = SegoeUI.Bold.With(size: 20),
        };
    }
}
