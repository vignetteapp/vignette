// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Vignette.Game.Themeing
{
    /// <summary>
    /// A drawable capable of being coloured by an <see cref="IThemeSource"/>.
    /// </summary>
    public abstract class ThemableDrawable<T> : CompositeDrawable, IThemable<T>
        where T : Drawable
    {
        /// <summary>
        /// The target drawable to be themed.
        /// The target drawable is only alive when the managing <see cref="ThemableDrawable<T>"/> is also alive.
        /// </summary>
        public T Target { get; private set; }

        private bool hasCreated;

        private bool isAttached;

        private ThemeSlot colour = ThemeSlot.White;

        public new ThemeSlot Colour
        {
            get => colour;
            set
            {
                if (value == colour)
                    return;

                colour = value;
                ScheduleThemeChange();
            }
        }

        protected IThemeSource CurrentSource { get; private set; }

        /// <summary>
        /// Create a themable drawable.
        /// </summary>
        /// <param name="attached">Whether to add the drawable as its child.</param>
        public ThemableDrawable(bool attached = true)
        {
            if (isAttached = attached)
                AddInternal(Create());
        }

        /// <summary>
        /// Creates and references the drawable. It can be useful if the drawable is required to be created elsewhere in the scenegraph.
        /// It will throw when there is already a referenced drawable.
        /// </summary>
        /// <returns>The drawable.</returns>
        public T Create()
        {
            if (Target != null && hasCreated)
                throw new InvalidOperationException("A drawable has already been created. You cannot create any more.");

            hasCreated = true;

            return Target = CreateDrawable();
        }

        [BackgroundDependencyLoader]
        private void load(IThemeSource source)
        {
            CurrentSource = source;
            CurrentSource.SourceChanged += ScheduleThemeChange;
        }

        protected override void Update()
        {
            base.Update();

            if (!Target.IsAlive && hasCreated)
                Expire();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (!isAttached)
                Target.Expire();

            base.Dispose(isDisposing);
        }

        protected void ScheduleThemeChange()
            => Scheduler.AddOnce(() => ThemeChanged(CurrentSource.GetCurrent()));

        protected abstract T CreateDrawable();

        protected virtual void ThemeChanged(Theme theme)
            => Target.Colour = theme.GetColour(Colour);
    }

    public static class ThemableDrawableExtensions
    {
        public static TThemable Apply<TThemable, TDrawable>(this TThemable themable, Action<TDrawable> adjustment)
            where TThemable : IThemable<TDrawable>
            where TDrawable : Drawable
        {
            themable.Target.With(adjustment);
            return themable;
        }
    }
}
