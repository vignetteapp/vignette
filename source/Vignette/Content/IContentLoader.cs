// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;

namespace Vignette.Content;

/// <summary>
/// Defines a mechanism for loading content.
/// </summary>
public interface IContentLoader
{
}

/// <summary>
/// Defines a mechanism for loading <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of content it loads.</typeparam>
public interface IContentLoader<T> : IContentLoader
    where T : class
{
    /// <summary>
    /// Loads a <see cref="ReadOnlySpan{byte}"/> as <typeparamref name="T"/>.
    /// </summary>
    /// <param name="bytes">The byte data to be read.</param>
    /// <returns>The loaded content.</returns>
    T Load(ReadOnlySpan<byte> bytes);
}
