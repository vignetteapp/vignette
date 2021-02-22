// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Shapes
{
    public class VignetteBox : Box, IThemeable
    {
        public double TransitionDuration { get; set; } = VignetteStyle.TransitionDuration;

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

        private ThemeColour themeColour = ThemeColour.NeutralLighter;

        private Bindable<string> theme;

        [Resolved]
        private ThemeStore themes { get; set; }

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager appConfig)
        {
            theme = appConfig.GetBindable<string>(ApplicationConfig.Theme);
            theme.BindValueChanged(state => updateColour(), true);
        }

        private void updateColour()
        {
            var colour = themes?.Get(theme.Value)?.Get(themeColour) ?? Colour4.White;
            Schedule(() => this.FadeColour(colour, TransitionDuration, Easing.OutQuint));
        }
    }
}
