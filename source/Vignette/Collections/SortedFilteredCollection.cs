// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Vignette.Collections;

/// <summary>
/// A collection whose items are sorted and filtered.
/// </summary>
/// <typeparam name="T">The type this collection will contain.</typeparam>
public class SortedFilteredCollection<T> : ICollection<T>
{
    public int Count => items.Count;

    private bool shouldRebuildCache;
    private readonly List<T> items = new();
    private readonly List<T> cache = new();
    private readonly IComparer<T> sorter;
    private readonly Predicate<T> filter;
    private readonly Action<T, EventHandler> sorterChangedSubscriber;
    private readonly Action<T, EventHandler> sorterChangedUnsubscriber;
    private readonly Action<T, EventHandler> filterChangedSubscriber;
    private readonly Action<T, EventHandler> filterChangedUnsubscriber;

    public SortedFilteredCollection(
        IComparer<T> sorter,
        Predicate<T> filter,
        Action<T, EventHandler> sorterChangedSubscriber,
        Action<T, EventHandler> sorterChangedUnsubscriber,
        Action<T, EventHandler> filterChangedSubscriber,
        Action<T, EventHandler> filterChangedUnsubscriber
    )
    {
        this.sorter = sorter;
        this.filter = filter;
        this.sorterChangedSubscriber = sorterChangedSubscriber;
        this.sorterChangedUnsubscriber = sorterChangedUnsubscriber;
        this.filterChangedSubscriber = filterChangedSubscriber;
        this.filterChangedUnsubscriber = filterChangedUnsubscriber;
    }

    public void Add(T item)
    {
        items.Add(item);
        invalidate();
    }

    public bool Remove(T item)
    {
        if (!items.Remove(item))
        {
            return false;
        }

        invalidate();
        unsubscribeFromEvents(item);

        return true;
    }

    public void Clear()
    {
        for (int i = 0; i < items.Count; i++)
        {
            unsubscribeFromEvents(items[i]);
        }

        items.Clear();
        invalidate();
    }

    public bool Contains(T item)
    {
        return items.Contains(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (shouldRebuildCache)
        {
            cache.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                if (filter(item))
                {
                    cache.Add(item);
                    subscribeToEvents(item);
                }
            }

            if (cache.Count > 0)
            {
                cache.Sort(sorter);
            }

            shouldRebuildCache = false;
        }

        return cache.GetEnumerator();
    }

    private void invalidate()
    {
        shouldRebuildCache = true;
    }

    private void subscribeToEvents(T item)
    {
        sorterChangedSubscriber(item, handleSorterChanged);
        filterChangedSubscriber(item, handleFilterChanged);
    }

    private void unsubscribeFromEvents(T item)
    {
        sorterChangedUnsubscriber(item, handleSorterChanged);
        filterChangedUnsubscriber(item, handleFilterChanged);
    }

    private void handleSorterChanged(object? sender, EventArgs args)
    {
        unsubscribeFromEvents((T)sender!);
        invalidate();
    }

    private void handleFilterChanged(object? sender, EventArgs args)
    {
        invalidate();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    bool ICollection<T>.IsReadOnly => false;

    void ICollection<T>.CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);
}
