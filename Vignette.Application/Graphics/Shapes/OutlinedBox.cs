// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Shapes
{
    public class OutlinedBox : Container, IThemeable
    {
        private ThemeColour themeColour = ThemeColour.NeutralLight;

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

        public OutlinedBox()
        {
            Masking = true;
            BorderThickness = VignetteStyle.BorderThickness;
            Add(new Box { RelativeSizeAxes = Axes.Both, Colour = Colour4.Transparent });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            themes?.Current.BindValueChanged(_ => updateColour(), true);
        }

        private void updateColour()
        {
            Schedule(() => this.TransformTo<OutlinedBox, SRGBColour>("BorderColour", themes.Current.Value?.Get(ThemeColour) ?? Colour4.White, TransitionDuration, Easing.OutQuint));
        }
    }
}
