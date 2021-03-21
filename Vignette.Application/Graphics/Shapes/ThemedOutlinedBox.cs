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
    public class ThemedOutlinedBox : Container, IThemeable
    {
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

        [Resolved]
        private ThemeStore themes { get; set; }

        private Bindable<string> theme;

        private ThemeColour themeColour = ThemeColour.NeutralLight;

        public ThemedOutlinedBox()
        {
            BorderThickness = 1.75f;
            Masking = true;
            Child = new Box { RelativeSizeAxes = Axes.Both, Colour = Colour4.Transparent };
        }

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager appConfig)
        {
            theme = appConfig.GetBindable<string>(ApplicationSetting.Theme);
            theme.BindValueChanged(state => updateColour(), true);
        }

        private void updateColour()
        {
            Schedule(() => BorderColour = themes?.Get(theme.Value)?.Get(themeColour) ?? Colour4.White);
        }
    }
}
