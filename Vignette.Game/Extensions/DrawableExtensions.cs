// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Extensions
{
    public static class DrawableExtensions
    {
        public static T FindNearestParent<T>(this Drawable drawable)
        {
            var cursor = drawable;

            while ((cursor = cursor.Parent) != null)
            {
                if (cursor is T match)
                    return match;
            }

            return default;
        }
    }
}
