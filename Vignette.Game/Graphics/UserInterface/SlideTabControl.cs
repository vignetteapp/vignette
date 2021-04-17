// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Presentations;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class SlideTabControl<T> : TabControl<T>
        where T : ISlide
    {
        private readonly Presentation presentation;

        public SlideTabControl(Presentation presentation)
        {
            this.presentation = presentation;
            presentation.SlidePushed += OnScreenPushed;
            presentation.SlideExited += OnScreenExited;

            if (presentation.CurrentSlide != null)
                OnScreenPushed(null, presentation.CurrentSlide);

            Current.ValueChanged += _ => presentation.Push(Current.Value);
        }


        protected virtual void OnScreenPushed(ISlide prev, ISlide next)
        {
            Current.Value = (T)next;
        }

        protected virtual void OnScreenExited(ISlide prev, ISlide next)
        {
            if (next != null)
                Current.Value = (T)next;
        }
    }
}
