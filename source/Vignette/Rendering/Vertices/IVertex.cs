// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using Sekai.Graphics;

namespace Vignette.Rendering.Vertices;

/// <summary>
/// Represents a single vertex in a geometric primitive.
/// </summary>
public interface IVertex
{
    static abstract InputLayoutDescription Layout { get; }
}
