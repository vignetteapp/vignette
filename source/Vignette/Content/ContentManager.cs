// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Sekai.Storages;

namespace Vignette.Content;

/// <summary>
/// Manages content.
/// </summary>
public sealed class ContentManager
{
    private readonly Storage storage;
    private readonly HashSet<string> extensions = new();
    private readonly HashSet<IContentLoader> loaders = new();
    private readonly Dictionary<ContentKey, WeakReference> content = new();

    internal ContentManager(Storage storage)
    {
        this.storage = storage;
    }

    /// <summary>
    /// Reads a file from <see cref="Storage"/> and loads it as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of content to load.</typeparam>
    /// <param name="path">The path to the content.</param>
    /// <returns>The loaded content.</returns>
    /// <exception cref="ArgumentException">Thrown when invalid path has been passed.</exception>
    public T Load<T>([StringSyntax(StringSyntaxAttribute.Uri)] string path)
        where T : class
    {
        string ext = Path.GetExtension(path);

        if (string.IsNullOrEmpty(ext))
        {
            throw new ArgumentException($"Failed to determine file extension.", nameof(path));
        }

        if (!extensions.Contains(ext))
        {
            throw new ArgumentException($"Cannot load unsupported file extension \"{ext}\".");
        }

        var key = new ContentKey(typeof(T), path);

        if (!content.TryGetValue(key, out var weak))
        {
            weak = new WeakReference(null);
            content.Add(key, weak);
        }

        if (!weak.IsAlive)
        {
            weak.Target = Load<T>(storage.Open(path, FileMode.Open, FileAccess.Read));
        }

        return (T)weak.Target!;
    }

    /// <summary>
    /// Loads a <see cref="Stream"/> as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of content to load.</typeparam>
    /// <param name="stream">The stream to be read.</param>
    /// <returns>The loaded content.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stream can't be read.</exception>
    public T Load<T>(Stream stream)
        where T : class
    {
        Span<byte> buffer = stackalloc byte[(int)stream.Length];

        if (stream.Read(buffer) <= 0)
        {
            throw new InvalidOperationException("Failed to read stream.");
        }

        return Load<T>((ReadOnlySpan<byte>)buffer);
    }

    /// <summary>
    /// Loads a <see cref="byte[]"/> as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of content to load.</typeparam>
    /// <param name="bytes">The byte data to be read.</param>
    /// <returns>The loaded content.</returns>
    public T Load<T>(byte[] bytes)
        where T : class
    {
        return Load<T>(bytes);
    }

    /// <summary>
    /// Loads a <see cref="ReadOnlySpan{byte}"/> as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of content to load.</typeparam>
    /// <param name="bytes">The byte data to be read.</param>
    /// <returns>The loaded content.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no loader was able to load the content.</exception>
    public T Load<T>(ReadOnlySpan<byte> bytes)
        where T : class
    {
        var result = default(T);

        foreach (var loader in loaders)
        {
            if (loader is not IContentLoader<T> typedLoader)
            {
                continue;
            }

            if (typedLoader.TryLoad(bytes, out result))
            {
                break;
            }
        }

        if (result is null)
        {
            throw new InvalidOperationException("Failed to load content.");
        }

        return result;
    }

    private void add(IContentLoader loader, params string[] extensions)
    {
        foreach (string extension in extensions)
        {
            string ext = extension.StartsWith(ext_separator) ? extension : ext_separator + extension;
            this.extensions.Add(ext);
        }

        loaders.Add(loader);
    }

    private const char ext_separator = '.';

    private readonly record struct ContentKey(Type Type, string Path);
}
