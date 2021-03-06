// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Configuration.Settings.Components
{
    public class Label : VignetteSpriteText
    {
        public Label()
        {
            Font = VignetteFont.SemiBold.With(size: 12);
            ThemeColour = Graphics.Themes.ThemeColour.NeutralSecondary;
        }
    }
}
