// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

namespace Vignette.Allocation;

/// <summary>
/// Defines a mechanism for objects that can pool <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of object being pooled.</typeparam>
public interface IObjectPool<T>
{
    /// <summary>
    /// Gets one <typeparamref name="T"/> from the pool.
    /// </summary>
    T Get();

    /// <summary>
    /// Returns <typeparamref name="T"/> back to the pool.
    /// </summary>
    /// <param name="item">The <typeparamref name="T"/> to return.</param>
    /// <returns><see langword="true"/> if the item has been returned. Otherwise, <see langword="false"/>.</returns>
    bool Return(T item);
}
