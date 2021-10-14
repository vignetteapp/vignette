// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

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

        public Vector2 Spacing
        {
            get => Target.Spacing;
            set => Target.Spacing = value;
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
