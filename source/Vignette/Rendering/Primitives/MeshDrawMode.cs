// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

namespace Vignette.Rendering.Primitives;

/// <summary>
/// An enumeration of mesh drawing modes.
/// </summary>
public enum MeshDrawMode
{
    /// <summary>
    /// The vertices are ordered as a list of triangles.
    /// </summary>
    Triangles,

    /// <summary>
    /// The vertices are ordered as a strip of triangles.
    /// </summary>
    TriangleStrip,

    /// <summary>
    /// The vertices are ordered as a list of lines.
    /// </summary>
    Lines,

    /// <summary>
    /// The vertices are ordered as a strip of lines.
    /// </summary>
    LineStrip,

    /// <summary>
    /// The vertices are ordered as a list of points.
    /// </summary>
    Points,
}
