// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using Vignette.Game.Graphics.Containers;

namespace Vignette.Game.Extensions
{
    public static class IViewExtensions
    {
        /// <summary>
        /// Makes this view the current view of its parent <see cref="ViewContainer"/>.
        /// </summary>
        public static void MakeCurrent(this IView view)
        {
            if (view.Parent is not ViewContainer container)
                throw new InvalidOperationException($"Cannot make this view current as it is not inside a {nameof(ViewContainer)}");

            container.Show(view);
        }

        /// <summary>
        /// Returns to the previous view with the option of expiring this view.
        /// </summary>
        /// <param name="expire">Should we expire or not.</param>
        public static void Return(this IView view, bool expire = false)
        {
            if (view.Parent is not ViewContainer container)
                throw new InvalidOperationException($"Cannot return view as it is not inside a {nameof(ViewContainer)}");

            container.Return(expire);
        }

        public static View AsDrawable(this IView view) => (View)view;
    }
}
