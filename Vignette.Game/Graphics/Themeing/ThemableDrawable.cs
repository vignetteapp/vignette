// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Vignette.Game.Graphics.Themeing
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

        private double lifetimeStart = double.MinValue;

        public override double LifetimeStart
        {
            get => Target?.LifetimeStart ?? lifetimeStart;
            set
            {
                if (Target != null)
                    Target.LifetimeStart = value;

                lifetimeStart = value;
            }
        }

        private double lifetimeEnd = double.MaxValue;

        public override double LifetimeEnd
        {
            get => Target?.LifetimeEnd ?? lifetimeEnd;
            set
            {
                if (Target != null)
                    Target.LifetimeEnd = value;

                lifetimeEnd = value;
            }
        }

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
            if (attached)
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
            CurrentSource.ThemeChanged += ScheduleThemeChange;
            ScheduleThemeChange();
        }

        protected void ScheduleThemeChange()
            => Scheduler.AddOnce(() => ThemeChanged(CurrentSource.Current.Value));

        /// <summary>
        /// Create the containing drawable.
        /// </summary>
        protected abstract T CreateDrawable();

        /// <summary>
        /// Called when the theme has been changed.
        /// </summary>
        protected virtual void ThemeChanged(Theme theme)
            => Target.Colour = theme.Get(Colour);
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
