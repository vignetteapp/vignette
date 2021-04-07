// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Caching;
using osu.Framework.Lists;

namespace Vignette.Game.Bindables
{
    public class BindableDictionary<TKey, TValue> : IBindableDictionary<TKey, TValue>, IDictionary<TKey, TValue>, IDictionary
    {
        /// <summary>
        /// An event which is raised when this <see cref="BindableDictionary{TKey, TValue}"/> changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// An event which is raised when <see cref="Disabled"/>'s state has changed (or manually via <see cref="triggerDisabledChange(bool)"/>).
        /// </summary>
        public event Action<bool> DisabledChanged;

        private readonly Dictionary<TKey, TValue> collection = new Dictionary<TKey, TValue>();

        private readonly Cached<WeakReference<BindableDictionary<TKey, TValue>>> weakReferenceCache = new Cached<WeakReference<BindableDictionary<TKey, TValue>>>();

        private WeakReference<BindableDictionary<TKey, TValue>> weakReference =>
            weakReferenceCache.IsValid ? weakReferenceCache.Value : weakReferenceCache.Value = new WeakReference<BindableDictionary<TKey, TValue>>(this);

        private LockedWeakList<BindableDictionary<TKey, TValue>> bindings;

        /// <summary>
        /// Creates a new <see cref="BindableDictionary{TKey, TValue}"/>, optionally adding the items of the given collection.
        /// </summary>
        /// <param name="items">The items that are going to be contained in the newly created <see cref="BindableDictionary{TKey, TValue}"/>.</param>
        public BindableDictionary(IDictionary<TKey, TValue> items = null)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    collection.Add(item.Key, item.Value);
                }
            }
        }

        #region IDictionary<TKey, TValue>

        public ICollection<TKey> Keys
            => collection.Keys;

        public ICollection<TValue> Values
            => collection.Values;

        /// <summary>
        /// Gets or sets the item at an index in this <see cref="BindableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <exception cref="InvalidOperationException">Thrown when setting a value while this <see cref="BindableDictionary{TKey, TValue}"/> is <see cref="Disabled"/>.</exception>
        public TValue this[TKey key]
        {
            get => collection[key];
            set => setIndex(key, value, null);
        }

        private void setIndex(TKey index, TValue item, BindableDictionary<TKey, TValue> caller)
        {
            ensureMutationAllowed();

            TValue lastItem = collection[index];

            collection[index] = item;

            if (bindings != null)
            {
                foreach (var b in bindings)
                {
                    // prevent re-adding the item back to the callee.
                    // That would result in a <see cref="StackOverflowException"/>.
                    if (b != caller)
                        b.setIndex(index, item, this);
                }
            }

            var oldItem = new KeyValuePair<TKey, TValue>(index, item);
            var newItem = new KeyValuePair<TKey, TValue>(index, lastItem);
            var keyIndex = Keys.ToList().IndexOf(index);
            notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItem, newItem, keyIndex));
        }

        /// <summary>
        /// Adds a single item to this <see cref="BindableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key to be added.</param>
        /// <param name="value">The item to be added.</param>
        /// <exception cref="InvalidOperationException">Thrown when this <see cref="BindableDictionary{TKey, TValue}"/> is <see cref="Disabled"/>.</exception>
        public void Add(TKey key, TValue value)
            => Add(new KeyValuePair<TKey, TValue>(key, value));

        /// <summary>
        /// Adds a single item to this <see cref="BindableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        /// <exception cref="InvalidOperationException">Thrown when this <see cref="BindableDictionary{TKey, TValue}"/> is <see cref="Disabled"/>.</exception>
        public void Add(KeyValuePair<TKey, TValue> item)
            => add(item, null);

        private void add(KeyValuePair<TKey, TValue> item, BindableDictionary<TKey, TValue> caller)
        {
            ensureMutationAllowed();

            collection.Add(item.Key, item.Value);

            if (bindings != null)
            {
                foreach (var b in bindings)
                {
                    // prevent re-adding the item back to the callee.
                    // That would result in a <see cref="StackOverflowException"/>.
                    if (b != caller)
                        b.add(item, this);
                }
            }

            notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(item.Key, item.Value), collection.Count - 1));
        }

        /// <summary>
        /// Clears the contents of this <see cref="BindableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when this <see cref="BindableDictionary{TKey, TValue}"/> is <see cref="Disabled"/>.</exception>
        public void Clear()
            => clear(null);

        private void clear(BindableDictionary<TKey, TValue> caller)
        {
            ensureMutationAllowed();

            if (collection.Count <= 0)
                return;

            // Preserve items for subscribers
            var clearedItems = collection.ToList();

            collection.Clear();

            if (bindings != null)
            {
                foreach (var b in bindings)
                {
                    // prevent re-adding the item back to the callee.
                    // That would result in a <see cref="StackOverflowException"/>.
                    if (b != caller)
                        b.clear(this);
                }
            }

            notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, clearedItems, 0));
        }

        /// <summary>
        /// Determines if an item is in this <see cref="BindableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key to locate in this <see cref="BindableDictionary{TKey, TValue}"/>.</param>
        /// <returns><code>true</code> if this <see cref="BindableDictionary{TKey, TValue}"/> contains the given item.</returns>
        public bool ContainsKey(TKey key)
            => collection.ContainsKey(key);

        public bool Contains(KeyValuePair<TKey, TValue> item)
            => collection.Contains(item);

        /// <summary>
        /// Removes an item from this <see cref="BindableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key to remove from this <see cref="BindableDictionary{TKey, TValue}"/>.</param>
        /// <returns><code>true</code> if the removal was successful.</returns>
        /// <exception cref="InvalidOperationException">Thrown if this <see cref="BindableDictionary{TKey, TValue}"/> is <see cref="Disabled"/>.</exception>
        public bool Remove(TKey key)
            => remove(key, null);

        public bool Remove(KeyValuePair<TKey, TValue> item)
            => remove(item.Key, null);

        private bool remove(TKey key, BindableDictionary<TKey, TValue> caller)
        {
            ensureMutationAllowed();

            var containKey = collection.ContainsKey(key);

            if (!containKey)
                return false;

            // Removal may have come from an equality comparison.
            // Always return the original reference from the list to other bindings and events.
            var listItem = collection[key];
            var index = Keys.ToList().IndexOf(key);

            collection.Remove(key);

            if (bindings != null)
            {
                foreach (var b in bindings)
                {
                    // prevent re-adding the item back to the callee.
                    // That would result in a <see cref="StackOverflowException"/>.
                    if (b != caller)
                        b.remove(key, this);
                }
            }

            notifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, listItem), index));

            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
            => collection.TryGetValue(key, out value);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            => ((IDictionary)collection).CopyTo(array, arrayIndex);

        public int Count
            => collection.Count;

        public bool IsReadOnly
            => Disabled;

        #endregion

        #region IDictionary

        ICollection IDictionary.Keys => throw new NotImplementedException();

        ICollection IDictionary.Values => throw new NotImplementedException();

        public object this[object key]
        {
            get => this[key];
            set => this[key] = (TValue)value;
        }

        public void Add(object key, object value)
            => Add((TKey)key, (TValue)value);

        public bool Contains(object key)
            => ContainsKey((TKey)key);

        public void Remove(object key)
            => Remove((TKey)key);

        public void CopyTo(Array array, int index)
            => ((IDictionary)collection).CopyTo(array, index);

        public bool IsFixedSize => false;

        public bool IsSynchronized => ((ICollection)collection).IsSynchronized;

        public object SyncRoot => ((ICollection)collection).SyncRoot;

        #endregion

        #region IParseable

        /// <summary>
        /// Parse an object into this instance.
        /// A collection holding items of type <typeparamref name="TValue"/> can be parsed. Null results in an empty <see cref="BindableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="input">The input which is to be parsed.</param>
        /// <exception cref="InvalidOperationException">Thrown if this <see cref="BindableDictionary{TKey, TValue}"/> is <see cref="Disabled"/>.</exception>
        public void Parse(object input)
        {
            ensureMutationAllowed();

            switch (input)
            {
                case null:
                    Clear();
                    break;

                case IDictionary<TKey, TValue> enumerable:
                    Clear();

                    foreach (var key in enumerable)
                    {
                        Add(key);
                    }

                    break;

                default:
                    throw new ArgumentException($@"Could not parse provided {input.GetType()} ({input}) to key {typeof(TKey)} and value {typeof(TValue)}.");
            }
        }

        #endregion

        #region ICanBeDisabled

        private bool disabled;

        /// <summary>
        /// Whether this <see cref="BindableDictionary{TKey, TValue}"/> has been disabled. When disabled, attempting to change the contents of this <see cref="BindableDictionary{TKey, TValue}"/> will result in an <see cref="InvalidOperationException"/>.
        /// </summary>
        public bool Disabled
        {
            get => disabled;
            set
            {
                if (value == disabled)
                    return;

                disabled = value;

                triggerDisabledChange();
            }
        }

        public void BindDisabledChanged(Action<bool> onChange, bool runOnceImmediately = false)
        {
            DisabledChanged += onChange;
            if (runOnceImmediately)
                onChange(Disabled);
        }

        private void triggerDisabledChange(bool propagateToBindings = true)
        {
            // check a bound bindable hasn't changed the value again (it will fire its own event)
            bool beforePropagation = disabled;

            if (propagateToBindings && bindings != null)
            {
                foreach (var b in bindings)
                    b.Disabled = disabled;
            }

            if (beforePropagation == disabled)
                DisabledChanged?.Invoke(disabled);
        }

        #endregion ICanBeDisabled

        #region IUnbindable

        public void UnbindEvents()
        {
            CollectionChanged = null;
            DisabledChanged = null;
        }

        public void UnbindBindings()
        {
            if (bindings == null)
                return;

            foreach (var b in bindings)
                b.unbind(this);

            bindings?.Clear();
        }

        public void UnbindAll()
        {
            UnbindEvents();
            UnbindBindings();
        }

        public void UnbindFrom(IUnbindable them)
        {
            if (!(them is BindableDictionary<TKey, TValue> tThem))
                throw new InvalidCastException($"Can't unbind a bindable of type {them.GetType()} from a bindable of type {GetType()}.");

            removeWeakReference(tThem.weakReference);
            tThem.removeWeakReference(weakReference);
        }

        private void unbind(BindableDictionary<TKey, TValue> binding)
            => bindings.Remove(binding.weakReference);

        #endregion IUnbindable

        #region IHasDescription

        public string Description { get; set; }

        #endregion IHasDescription

        #region IBindableDictionary

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => throw new NotImplementedException();

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => throw new NotImplementedException();

        public void BindTo(IBindableDictionary<TKey, TValue> them)
        {
            if (!(them is BindableDictionary<TKey, TValue> tThem))
                throw new InvalidCastException($"Can't bind to a bindable of type {them.GetType()} from a bindable of type {GetType()}.");

            BindTo(tThem);
        }

        /// <summary>
        /// An alias of <see cref="BindTo"/> provided for use in object initializer scenarios.
        /// Passes the provided value as the foreign (more permanent) bindable.
        /// </summary>
        public IBindableDictionary<TKey, TValue> BindTarget
        {
            set => ((IBindableDictionary<TKey, TValue>)this).BindTo(value);
        }

        /// <summary>
        /// Binds this <see cref="BindableDictionary{TKey, TValue}"/> to another.
        /// </summary>
        /// <param name="them">The <see cref="BindableDictionary{TKey, TValue}"/> to be bound to.</param>
        public void BindTo(BindableDictionary<TKey, TValue> them)
        {
            if (them == null)
                throw new ArgumentNullException(nameof(them));
            if (bindings?.Contains(weakReference) ?? false)
                throw new ArgumentException("An already bound collection can not be bound again.");
            if (them == this)
                throw new ArgumentException("A collection can not be bound to itself");

            // copy state and content over
            Parse(them);
            Disabled = them.Disabled;

            addWeakReference(them.weakReference);
            them.addWeakReference(weakReference);
        }

        /// <summary>
        /// Bind an action to <see cref="CollectionChanged"/> with the option of running the bound action once immediately
        /// with an <see cref="NotifyCollectionChangedAction.Add"/> event for the entire contents of this <see cref="BindableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="onChange">The action to perform when this <see cref="BindableDictionary{TKey, TValue}"/> changes.</param>
        /// <param name="runOnceImmediately">Whether the action provided in <paramref name="onChange"/> should be run once immediately.</param>
        public void BindCollectionChanged(NotifyCollectionChangedEventHandler onChange, bool runOnceImmediately = false)
        {
            CollectionChanged += onChange;
            if (runOnceImmediately)
                onChange(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection.ToList()));
        }

        private void addWeakReference(WeakReference<BindableDictionary<TKey, TValue>> weakReference)
        {
            bindings ??= new LockedWeakList<BindableDictionary<TKey, TValue>>();
            bindings.Add(weakReference);
        }

        private void removeWeakReference(WeakReference<BindableDictionary<TKey, TValue>> weakReference) => bindings?.Remove(weakReference);

        public void BindTo(IBindable them)
        {
            if (!(them is BindableDictionary<TKey, TValue> tThem))
                throw new InvalidCastException($"Can't bind to a bindable of type {them.GetType()} from a bindable of type {GetType()}.");

            BindTo(tThem);
        }

        IBindable IBindable.GetBoundCopy()
            => GetBoundCopy();

        IBindableDictionary<TKey, TValue> IBindableDictionary<TKey, TValue>.GetBoundCopy()
            => GetBoundCopy();

        /// <summary>
        /// Create a new instance of <see cref="BindableList{T}"/> and binds it to this instance.
        /// </summary>
        /// <returns>The created instance.</returns>
        public BindableDictionary<TKey, TValue> GetBoundCopy()
        {
            var copy = new BindableDictionary<TKey, TValue>();
            copy.BindTo(this);
            return copy;
        }

        #endregion IBindableDictionary

        #region IEnumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        IDictionaryEnumerator IDictionary.GetEnumerator()
            => GetEnumerator() as IDictionaryEnumerator;

        #endregion IEnumerable

        private void notifyCollectionChanged(NotifyCollectionChangedEventArgs args) => CollectionChanged?.Invoke(this, args);

        private void ensureMutationAllowed()
        {
            if (Disabled)
                throw new InvalidOperationException($"Cannot mutate the {nameof(BindableDictionary<TKey, TValue>)} while it is disabled.");
        }

        public bool IsDefault => Count == 0;
    }
}
