// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Sekai.Graphics;
using Vignette.Rendering.Vertices;

namespace Vignette.Rendering.Primitives;

/// <summary>
/// Represents a geometric primitive.
/// </summary>
public interface IPrimitive
{
    /// <summary>
    /// Gets the number of vertices of this primitive.
    /// </summary>
    int VertexCount { get; }

    /// <summary>
    /// Gets the primitive drawing mode.
    /// </summary>
    PrimitiveType Mode { get; }

    /// <summary>
    /// Gets the indices of this primitive.
    /// </summary>
    ReadOnlySpan<short> GetIndices();

    /// <summary>
    /// Gets the vertex data of this primitive.
    /// </summary>
    ReadOnlySpan<byte> GetVertices();
}

/// <summary>
/// Represents a geometric primitive.
/// </summary>
/// <typeparam name="T">The type of vertices this primitive contains.</typeparam>
public interface IPrimitive<T> : IPrimitive
    where T : unmanaged, IVertex
{
    /// <summary>
    /// Gets the vertices of this primitive.
    /// </summary>
    new ReadOnlySpan<T> GetVertices();
}
