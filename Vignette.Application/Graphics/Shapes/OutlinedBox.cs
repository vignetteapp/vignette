// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Shapes
{
    public class OutlinedBox : Container, IThemeable
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

        private Bindable<string> theme;

        private ThemeColour themeColour = ThemeColour.NeutralLight;

        [Resolved]
        private ThemeStore themes { get; set; }

        public OutlinedBox()
        {
            Masking = true;
            BorderThickness = VignetteStyle.BorderThickness;
            Add(new Box { RelativeSizeAxes = Axes.Both, Colour = Colour4.Transparent });
        }

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager appConfig)
        {
            theme = appConfig.GetBindable<string>(ApplicationConfig.Theme);
            theme.BindValueChanged(state => updateColour(), true);
        }

        private void updateColour()
        {
            var colour = themes?.Get(theme.Value)?.Get(themeColour) ?? Colour4.White;
            Schedule(() => this.TransformTo<OutlinedBox, SRGBColour>("BorderColour", colour, TransitionDuration, Easing.OutQuint));
        }
    }
}
