// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Collections.Specialized;
using osu.Framework.Bindables;

namespace Vignette.Game.Bindables
{
    /// <summary>
    /// An readonly interface which can be bound to other <see cref="IBindableDictionary{TKey, TValue}"/>s in order to watch for state and content changes.
    /// </summary>
    /// <typeparam name="TKey">The type of key encapsulated by this <see cref="IBindableDictionary{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TValue">The type of value encapsulated by this <see cref="IBindableDictionary{TKey, TValue}"/>.</typeparam>
    public interface IBindableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IBindable, INotifyCollectionChanged
    {
        /// <summary>
        /// Binds self to another bindable such that we receive any values and value limitations of the bindable we bind width.
        /// </summary>
        /// <param name="them">The foreign bindable. This should always be the most permanent end of the bind (ie. a ConfigManager)</param>
        void BindTo(IBindableDictionary<TKey, TValue> them);

        /// <summary>
        /// An alias of <see cref="BindTo"/> provided for use in object initializer scenarios.
        /// Passes the provided value as the foreign (more permanent) bindable.
        /// </summary>
        new sealed IBindableDictionary<TKey, TValue> BindTarget
        {
            set => BindTo(value);
        }

        /// <summary>
        /// Retrieve a new bindable instance weakly bound to the configuration backing.
        /// If you are further binding to events of a bindable retrieved using this method, ensure to hold
        /// a local reference.
        /// </summary>
        /// <returns>A weakly bound copy of the specified bindable.</returns>
        new IBindableDictionary<TKey, TValue> GetBoundCopy();
    }
}
