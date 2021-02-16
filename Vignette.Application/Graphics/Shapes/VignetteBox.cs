// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Shapes
{
    public class VignetteBox : Box, IThemeable
    {
        private ThemeColour themeColour = ThemeColour.NeutralLighter;

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

        public double TransitionDuration { get; set; } = VignetteStyle.TransitionDuration;

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
