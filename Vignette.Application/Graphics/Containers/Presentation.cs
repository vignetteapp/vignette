// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace Vignette.Application.Graphics.Containers
{
    /// <summary>
    /// Similar to a <see cref="osu.Framework.Screens.ScreenStack"> but offers more flexibility on switching between its children.
    /// </summary>
    public class Presentation : Presentation<PresentationSlide>
    {
    }

    /// <summary>
    /// Similar to a <see cref="osu.Framework.Screens.ScreenStack"> but offers more flexibility on switching between its children.
    /// </summary>
    public class Presentation<T> : CompositeDrawable, IHasCurrentValue<T>
        where T : PresentationSlide
    {
        private readonly BindableWithCurrent<T> current = new BindableWithCurrent<T>();

        private readonly List<T> items = new List<T>();

        private readonly IBindableList<T> itemSource = new BindableList<T>();

        private IBindableList<T> boundItemSource;

        /// <summary>
        /// Whether we can perform <see cref="Next"/> or <see cref="Previous"/> at the ends of the slides.
        /// </summary>
        public bool CanWrap { get; set; }

        public IEnumerable<T> Items
        {
            get => boundItemSource != null ? itemSource : items;
            set
            {
                if (boundItemSource != null)
                    throw new InvalidOperationException($"Cannot manually set {nameof(Items)} when an {nameof(ItemSource)} is bound.");

                setSlides(value);
            }
        }

        public IBindableList<T> ItemSource
        {
            get => itemSource;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (boundItemSource != null)
                    itemSource.UnbindFrom(boundItemSource);

                itemSource.BindTo(boundItemSource = value);
            }
        }

        public Bindable<T> Current
        {
            get => current.Current;
            set => current.Current = value;
        }

        public Presentation()
        {
            ItemSource.BindCollectionChanged(handleCollectionUpdate);
            Current.BindValueChanged(handleCurrentChange, true);
        }

        /// <summary>
        /// Selects the slide to present.
        /// </summary>
        /// <param name="index">The index of the slide to present.</param>
        public void Select(int index)
        {
            if (index > items.Count || index < 0)
                throw new IndexOutOfRangeException();

            Current.Value = Items.ElementAt(index);
        }

        /// <summary>
        /// Selects the slide to present.
        /// </summary>
        /// <param name="slide">The slide to present.</param>
        public void Select(T slide)
        {
            if (!Items.Contains(slide))
                throw new InvalidOperationException($"{slide} is not a child of {nameof(Presentation<T>)}");

            Current.Value = slide;
        }

        /// <summary>
        /// Goes to the next slide. Will move back to the start when it has reached the end if <see cref="CanWrap"/> is true.
        /// </summary>
        public void Next() => moveIndex(1);

        /// <summary>
        /// Goes to the previous slide. Will move to the end when it has reached the start if <see cref="CanWrap"/> is true.
        /// </summary>
        public void Previous() => moveIndex(-1);

        /// <summary>
        /// Goes to the first slide.
        /// </summary>
        public void First() => Select(0);

        /// <summary>
        /// Goes to the last slide.
        /// </summary>
        public void Last() => Select(items[^1]);

        /// <summary>
        /// Adds a single slide.
        /// </summary>
        /// <param name="slide">The slide to add.</param>
        public void Add(T slide) => AddRange(new[] { slide });

        /// <summary>
        /// Adds a range of slides.
        /// </summary>
        /// <param name="slides">The slides to add.</param>
        public void AddRange(IEnumerable<T> slides)
        {
            if (boundItemSource != null)
                throw new InvalidOperationException($"Cannot manually add {nameof(Items)} when an {nameof(ItemSource)} is bound.");

            setSlides(slides, false);
        }

        /// <summary>
        /// Removes a single slide.
        /// </summary>
        /// <param name="slide"></param>
        public void Remove(T slide) => RemoveRange(new[] { slide });

        /// <summary>
        /// Removes a range of slides.
        /// </summary>
        /// <param name="slides"></param>
        public void RemoveRange(IEnumerable<T> slides)
        {
            if (boundItemSource != null)
                throw new InvalidOperationException($"Cannot manually remove {nameof(Items)} when an {nameof(ItemSource)} is bound.");

            removeSlides(slides);
        }

        /// <summary>
        /// Removes all slides.
        /// </summary>
        public void Clear() => RemoveRange(Items.ToArray());

        private void moveIndex(int direction)
        {
            var itemList = Items.ToList();

            int nextIndex = itemList.IndexOf(Current.Value) + direction;

            if (CanWrap)
                Select(wrap(nextIndex, 0, itemList.Count));
            else if (nextIndex > -1 && nextIndex < itemList.Count)
                Select(nextIndex);

            static int wrap(int val, int min, int max)
                => (((val - min) % (max - min)) + (max - min)) % ((max - min) + min);
        }

        private void handleCollectionUpdate(object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    setSlides(args.NewItems.OfType<T>(), false);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    removeSlides(args.OldItems.OfType<T>());
                    break;

                case NotifyCollectionChangedAction.Reset:
                    setSlides(Array.Empty<T>());
                    break;
            }
        }

        private void handleCurrentChange(ValueChangedEvent<T> slide)
        {
            if (slide.NewValue == null || slide.NewValue == slide.OldValue)
                return;

            ChangeInternalChildDepth(slide.NewValue, slide.OldValue?.Depth + 1 ?? 0);

            slide.OldValue?.OnExiting();
            slide.NewValue.OnEntering();
        }

        private void setSlides(IEnumerable<T> slides, bool clear = true)
        {
            if (clear)
                items.Clear();

            items.AddRange(slides);
            AddRangeInternal(slides);

            if (Current.Value == null)
                Select(slides.FirstOrDefault());
        }

        private void removeSlides(IEnumerable<T> slides)
        {
            foreach (var slide in slides)
                items.Remove(slide);

            if (!items.Contains(Current.Value) && items.Count > 0)
                Select(items.FirstOrDefault());
            else
                Current.Value = null;

            foreach (var slide in slides)
            {
                if (slide == Current.Value)
                    slide.OnExiting();

                slide.Expire();
            }
        }
    }
}
