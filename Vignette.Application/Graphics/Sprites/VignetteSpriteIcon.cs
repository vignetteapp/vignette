// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Sprites
{
    public class VignetteSpriteIcon : SpriteIcon, IThemeable
    {
        private ThemeColour themeColour = ThemeColour.Black;

        public ThemeColour ThemeColour
        {
            get => themeColour;
            set
            {
                if (themeColour == value)
                    return;

                themeColour = value;
                updateColour();
            }
        }

        public double TransitionDuration { get; set; } = 250;

        [Resolved]
        private ThemeStore themes { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            themes?.Current.BindValueChanged(_ => updateColour(), true);
        }

        private void updateColour()
        {
            Schedule(() => this.FadeColour(themes.Current.Value?.Get(ThemeColour) ?? Colour4.White, TransitionDuration, Easing.OutQuint));
        }
    }
}
