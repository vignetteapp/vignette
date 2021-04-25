// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.Sprites
{
    public class ThemableSpriteText : ThemableDrawable<SpriteText>, IHasText
    {
        public LocalisableString Text
        {
            get => Target.Text;
            set => Target.Text = value;
        }

        public FontUsage Font
        {
            get => Target.Font;
            set => Target.Font = value;
        }

        public ThemableSpriteText(bool attached = true)
            : base(attached)
        {
            AutoSizeAxes = Axes.Both;
        }

        protected override SpriteText CreateDrawable() => new SpriteText
        {
            Font = SegoeUI.Regular,
        };
    }
}
