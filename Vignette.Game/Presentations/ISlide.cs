// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;

namespace Vignette.Game.Presentations
{
    public interface ISlide : IDrawable
    {
        void OnEntering(ISlide last);

        void OnExiting(ISlide next);
    }

    public static class SlideExtensions
    {
        public static Drawable AsDrawable(this ISlide slide) => (Drawable)slide;

        public static void Push(this ISlide slide, ISlide newSlide)
            => getOwner(slide)?.Push(newSlide);

        public static void Exit(this ISlide slide)
            => getOwner(slide)?.Exit();

        private static Presentation getOwner(IDrawable current)
        {
            while (current != null)
            {
                if (current is Presentation presentation)
                    return presentation;

                current = current.Parent;
            }

            return null;
        }
    }
}
