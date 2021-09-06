// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Graphics.Themeing
{
    public partial class Theme
    {
        /// <summary>
        /// Create a theme from base colours.
        /// </summary>
        /// <param name="background">The base background colour.</param>
        /// <param name="foreground">The base foreground colour.</param>
        /// <param name="accent">The base accent colour.</param>
        public static Theme From(string accent)
            => From(Colour4.FromHex(accent));

        /// <summary>
        /// Create a theme from base colours.
        /// </summary>
        /// <param name="accent">The base accent colour.</param>
        public static Theme From(Colour4 accent)
            => new Theme()
            .Set(ThemeSlot.AccentDarker, accent.Darken(0.24f))
            .Set(ThemeSlot.AccentDark, accent.Darken(0.12f))
            .Set(ThemeSlot.AccentDarkAlt, accent.Darken(0.6f))
            .Set(ThemeSlot.AccentPrimary, accent)
            .Set(ThemeSlot.AccentSecondary, accent.Lighten(0.6f))
            .Set(ThemeSlot.AccentTertiary, accent.Lighten(0.12f))
            .Set(ThemeSlot.AccentLight, accent.Lighten(0.18f))
            .Set(ThemeSlot.AccentLighter, accent.Lighten(0.24f))
            .Set(ThemeSlot.AccentLighterAlt, accent.Lighten(0.3f));
    }
}
