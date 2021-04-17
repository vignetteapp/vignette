// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Vignette.Game.Presentations
{
    public class Presentation : CompositeDrawable
    {
        public ISlide CurrentSlide { get; private set; }

        public event Action<ISlide, ISlide> SlidePushed;

        public event Action<ISlide, ISlide> SlideExited;

        private readonly List<ISlide> slides = new List<ISlide>();

        public Presentation(ISlide slide = null)
        {
            RelativeSizeAxes = Axes.Both;
            if (slide != null)
                Push(slide);

        }

        public void Push(ISlide slide)
        {
            if (!slides.Contains(slide))
            {
                slides.Add(slide);
                AddInternal(slide.AsDrawable());
            }

            changeDepths(CurrentSlide, slide);
            SlidePushed?.Invoke(CurrentSlide, slide);
            CurrentSlide = slide;
        }

        public void Exit()
        {
            if (slides.Count < 1)
                return;

            var next = slides[Math.Clamp(slides.IndexOf(CurrentSlide), 0, slides.Count - 2)];
            changeDepths(CurrentSlide, next);

            CurrentSlide.AsDrawable().Expire();
            SlideExited?.Invoke(CurrentSlide, next);
            CurrentSlide = next;
        }

        private void changeDepths(ISlide from, ISlide to)
        {
            ChangeInternalChildDepth(to.AsDrawable(), from?.AsDrawable().Depth + 1 ?? 0);

            from?.OnExiting(to);
            to.OnEntering(from);
        }
    }
}
