// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;

namespace Vignette.Application.Graphics
{
    public class VignetteColour
    {
        public IBindable<Colour4> Accent => accent;

        public IBindable<bool> DarkMode => darkMode;

        private readonly Bindable<Colour4> accent = new Bindable<Colour4>();

        private readonly BindableBool darkMode = new BindableBool();

        public VignetteColour(IBindable<Colour4> accent = null, IBindable<bool> darkMode = null)
        {
            accent?.BindTo(this.accent);
            darkMode?.BindTo(this.darkMode);
        }

        public Colour4 GetBackgroundColor(int level)
        {
            if (level < 0 || level > 10)
                throw new ArgumentOutOfRangeException($@"{nameof(level)} is greater than 10 or less than 0.");

            float offset = 0.1f * level;
            float lightness = darkMode.Value ? 0.1f + offset : 1.0f - offset;
            return Colour4.FromHSL(0, 0, lightness);
        }

        public static Colour4 Black => Colour4.Black;

        public static Colour4 White => Colour4.White;

        public static Colour4 Information => Colour4.FromHex("303F9F");

        public static Colour4 Critical => Colour4.FromHex("DD6E6E");

        public static Colour4 Affirmative => Colour4.FromHex("689F38");

        public static Colour4 Warning => Colour4.FromHex("F57F17");
    }

    public enum Colouring
    {
        /// <summary>
        /// Uses the theme's main color. Usually between Light Mode and Dark Mode.
        /// </summary>
        Background,

        /// <summary>
        /// Uses the user's specified color.
        /// </summary>
        Accent,

        /// <summary>
        /// Allows to directly manipulate <see cref="Drawable.Colour"/>.
        /// </summary>
        Override,
    }
}
