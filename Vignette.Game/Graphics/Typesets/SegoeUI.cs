// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Sprites;

namespace Vignette.Game.Graphics.Typesets
{
    public static class SegoeUI
    {
        public static FontUsage Light => GetFont(FontWeight.Light);

        public static FontUsage SemiLight => GetFont(FontWeight.SemiLight);

        public static FontUsage Regular => GetFont();

        public static FontUsage SemiBold => GetFont(FontWeight.SemiBold);

        public static FontUsage Bold => GetFont(FontWeight.Bold);

        public static FontUsage Black => GetFont(FontWeight.Black);

        public static FontUsage GetFont(FontWeight? weight = null, float size = 14.0f, bool italics = false, bool fixedWidth = false)
            => new FontUsage("SegoeUI", size, weight?.ToString(), italics, fixedWidth);
    }

    public static class SegoeUIFontExtensions
    {
        public static FontUsage With(this FontUsage usage, FontWeight? weight = null, float? size = null, bool? italics = null, bool? fixedWidth = null)
            => usage.With("SegoeUI", size, weight?.ToString(), italics, fixedWidth);
    }

    public enum FontWeight
    {
        Light,

        SemiLight,

        SemiBold,

        Bold,

        Black
    }
}
