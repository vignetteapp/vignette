// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Graphics.Containers
{
    /// <summary>
    /// An interface denoting that this <see cref="IDrawable"/> can be inserted as a child of a <see cref="ViewContainer"/>.
    /// </summary>
    public interface IView : IDrawable
    {
        /// <summary>
        /// Called when this view is being switched out to another.
        /// </summary>
        void OnExiting(IView last);

        /// <summary>
        /// Called when this view is being switched in from another.
        /// </summary>
        void OnEntering(IView last);
    }
}
