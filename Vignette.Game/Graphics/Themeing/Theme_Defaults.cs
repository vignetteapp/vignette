// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
            .Set(ThemeSlot.AccentDarker, "6b3172")
            .Set(ThemeSlot.AccentDark, "91439b")
            .Set(ThemeSlot.AccentDarkAlt, "ab4fb8")
            .Set(ThemeSlot.AccentPrimary, "BE58CB")
            .Set(ThemeSlot.AccentSecondary, "c669d2")
            .Set(ThemeSlot.AccentTertiary, "d794e0")
            .Set(ThemeSlot.AccentLight, "ebc7f0")
            .Set(ThemeSlot.AccentLighter, "f4e0f7")
            .Set(ThemeSlot.AccentLighterAlt, "fcf7fd");

        /// <summary>
        /// The default Light Theme.
        /// </summary>
        public static readonly Theme Light = new Theme("Light")
            .Set(ThemeSlot.Black, "000000")
            .Set(ThemeSlot.Gray190, "201f1e")
            .Set(ThemeSlot.Gray160, "323130")
            .Set(ThemeSlot.Gray150, "3b3a39")
            .Set(ThemeSlot.Gray130, "605e5c")
            .Set(ThemeSlot.Gray110, "8a8886")
            .Set(ThemeSlot.Gray90, "a19f9d")
            .Set(ThemeSlot.Gray60, "c8c6c4")
            .Set(ThemeSlot.Gray50, "d2d0ce")
            .Set(ThemeSlot.Gray40, "e1dfdd")
            .Set(ThemeSlot.Gray30, "edebe9")
            .Set(ThemeSlot.Gray20, "f3f2f1")
            .Set(ThemeSlot.Gray10, "faf9f8")
            .Set(ThemeSlot.White, "ffffff")
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
