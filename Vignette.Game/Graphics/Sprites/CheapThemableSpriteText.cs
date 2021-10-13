// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Sprites
{
    /// <summary>
    /// An alternative to <see cref="ThemableSpriteText"/> which directly inherits from <see cref="SpriteText"/>.
    /// It does not contain a full implementation of a usual <see cref="ThemableDrawable{T}"/> and is only used
    /// in cases where an actual <see cref="SpriteText"/> is needed as a parameter or return type.
    /// </summary>
    public class CheapThemableSpriteText : SpriteText, IThemable<SpriteText>
    {
        public SpriteText Target => this;

        public new ThemeSlot Colour
        {
            get => colour;
            set
            {
                if (colour == value)
                    return;

                colour = value;
                scheduleThemeChange();
            }
        }

        private IThemeSource source;
        private ThemeSlot colour = ThemeSlot.Gray190;

        public CheapThemableSpriteText()
        {
            Font = SegoeUI.Regular;
        }

        [BackgroundDependencyLoader]
        private void load(IThemeSource source)
        {
            this.source = source;
            this.source.ThemeChanged += scheduleThemeChange;
            scheduleThemeChange();
        }

        private void scheduleThemeChange()
            => Scheduler.AddOnce(() => base.Colour = source.Current.Value.Get(Colour));
    }
}
