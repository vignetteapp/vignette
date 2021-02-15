// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

namespace Vignette.Application.Graphics
{
    /// <summary>
    /// A <see cref="osu.Framework.Graphics.Drawable"/> that is coloured based on the user's set colour scheme.
    /// </summary>
    public interface IThemeable
    {
        /// <summary>
        /// Used by <see cref="Colouring.Background"/> to set both <see cref="DarknessLevel"/> and <see cref="BrightnessLevel"/> simultaneously.
        /// </summary>
        public int Level { set; }

        /// <summary>
        /// Determines how this <see cref="IThemeable"/> element should be colored.
        /// </summary>
        public Colouring Colouring { get; set; }
    }
}
