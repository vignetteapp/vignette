// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

namespace Vignette.Application.Graphics.Themes
{
    /// <summary>
    /// A <see cref="osu.Framework.Graphics.Drawable"/> that is coloured based on the user's set theme.
    /// </summary>
    public interface IThemeable
    {
        /// <summary>
        /// The colour to apply on the themeable element.
        /// </summary>
        public ThemeColour ThemeColour { get; set; }

        /// <summary>
        /// The fade duration of the themeable element. Used when changing between themes.
        /// </summary>
        public double TransitionDuration { get; set; }
    }
}
