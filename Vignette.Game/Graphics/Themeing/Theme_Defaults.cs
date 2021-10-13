// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

namespace Vignette.Game.Graphics.Themeing
{
    public partial class Theme
    {
        private static readonly Theme accent_ins = new Theme()
            .Set(ThemeSlot.AccentDarker, "31724b")
            .Set(ThemeSlot.AccentDark, "439b66")
            .Set(ThemeSlot.AccentDarkAlt, "4fb879")
            .Set(ThemeSlot.AccentPrimary, "58CB86")
            .Set(ThemeSlot.AccentSecondary, "69d293")
            .Set(ThemeSlot.AccentTertiary, "94e0b2")
            .Set(ThemeSlot.AccentLight, "c7f0d7")
            .Set(ThemeSlot.AccentLighter, "e0f7e9")
            .Set(ThemeSlot.AccentLighterAlt, "f7fdf9");

        private static readonly Theme accent_pub = new Theme()
            .Set(ThemeSlot.AccentDarker, "790f36")
            .Set(ThemeSlot.AccentDark, "a51449")
            .Set(ThemeSlot.AccentDarkAlt, "c31756")
            .Set(ThemeSlot.AccentPrimary, "d81b60")
            .Set(ThemeSlot.AccentSecondary, "dd3271")
            .Set(ThemeSlot.AccentTertiary, "e86e9a")
            .Set(ThemeSlot.AccentLight, "f4b3cb")
            .Set(ThemeSlot.AccentLighter, "f9d6e3")
            .Set(ThemeSlot.AccentLighterAlt, "fdf5f8");

        /// <summary>
        /// The default Light Theme.
        /// </summary>
        public static readonly Theme Light = new Theme("Light")
            .Set(ThemeSlot.Black, "0b0b0b")
            .Set(ThemeSlot.Gray190, "151515")
            .Set(ThemeSlot.Gray160, "000000")
            .Set(ThemeSlot.Gray150, "2f2f2f")
            .Set(ThemeSlot.Gray130, "373737")
            .Set(ThemeSlot.Gray110, "8a8886")
            .Set(ThemeSlot.Gray90, "595959")
            .Set(ThemeSlot.Gray60, "c2c2c2")
            .Set(ThemeSlot.Gray50, "cacaca")
            .Set(ThemeSlot.Gray40, "d3d3d3")
            .Set(ThemeSlot.Gray30, "e3e3e3")
            .Set(ThemeSlot.Gray20, "ededed")
            .Set(ThemeSlot.Gray10, "f1f1f1")
            .Set(ThemeSlot.White, "f7f7f7")
            .Merge(VignetteGameBase.IsInsidersBuild ? accent_ins : accent_pub);

        /// <summary>
        /// The default Dark Theme.
        /// </summary>
        public static readonly Theme Dark = new Theme("Dark")
            .Set(ThemeSlot.Black, "f8f8f8")
            .Set(ThemeSlot.Gray190, "f4f4f4")
            .Set(ThemeSlot.Gray160, "ffffff")
            .Set(ThemeSlot.Gray150, "dadada")
            .Set(ThemeSlot.Gray130, "d0d0d0")
            .Set(ThemeSlot.Gray110, "8a8886")
            .Set(ThemeSlot.Gray90, "c8c8c8")
            .Set(ThemeSlot.Gray60, "6d6d6d")
            .Set(ThemeSlot.Gray50, "4f4f4f")
            .Set(ThemeSlot.Gray40, "484848")
            .Set(ThemeSlot.Gray30, "3f3f3f")
            .Set(ThemeSlot.Gray20, "313131")
            .Set(ThemeSlot.Gray10, "282828")
            .Set(ThemeSlot.White, "1f1f1f")
            .Merge(VignetteGameBase.IsInsidersBuild ? accent_ins : accent_pub);
    }
}
