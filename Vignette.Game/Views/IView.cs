// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Views
{
    public interface IView : IDrawable
    {
    }

    public static class IViewExtensions
    {
        /// <summary>
        /// Makes this view the current view of its parent <see cref="ViewContainer"/>.
        /// </summary>
        public static void MakeCurrent(this IView view)
        {
            if (view.Parent is not ViewContainer container)
                return;

            container.Show(view);
        }

        /// <summary>
        /// Returns to the previous view with the option of expiring this view.
        /// </summary>
        /// <param name="expire">Should we expire or not.</param>
        public static void Return(this IView view, bool expire = false)
        {
            if (view.Parent is not ViewContainer container)
                return;

            container.Return(expire);
        }

        public static View AsDrawable(this IView view) => (View)view;
    }
}
