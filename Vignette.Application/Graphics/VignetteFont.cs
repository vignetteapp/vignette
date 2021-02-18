// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Sprites;

namespace Vignette.Application.Graphics
{
    public static class VignetteFont
    {
        public static FontUsage Thin => GetFont(FontWeight.Thin);

        public static FontUsage ExtraLight => GetFont(FontWeight.ExtraLight);

        public static FontUsage Light => GetFont(FontWeight.Light);

        public static FontUsage Regular => GetFont(FontWeight.Regular);

        public static FontUsage Medium => GetFont(FontWeight.Medium);

        public static FontUsage SemiBold => GetFont(FontWeight.SemiBold);

        public static FontUsage Bold => GetFont(FontWeight.Bold);

        public static FontUsage ExtraBold => GetFont(FontWeight.ExtraBold);

        public static FontUsage Black => GetFont(FontWeight.Black);

        public static FontUsage GetFont(FontWeight weight = FontWeight.Regular, float size = 14.0f, bool italics = false, bool fixedWidth = false)
            => new FontUsage("Raleway", size, weight.ToString(), italics, fixedWidth);
    }

    public static class VignetteFontExtensions
    {
        public static FontUsage With(this FontUsage usage, FontWeight? weight = null, float? size = null, bool? italics = null, bool? fixedWidth = null)
            => usage.With("Raleway", size, weight?.ToString(), italics, fixedWidth);
    }

    public enum FontWeight
    {
        Thin,

        ExtraLight,

        Light,

        Regular,

        Medium,

        SemiBold,

        Bold,

        ExtraBold,

        Black,
    }
}
