// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
