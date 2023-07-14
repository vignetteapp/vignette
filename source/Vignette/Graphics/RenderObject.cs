// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using Sekai.Graphics;
using Sekai.Mathematics;

namespace Vignette.Graphics;

/// <summary>
/// An object that can be drawn.
/// </summary>
public class RenderObject
{
    /// <summary>
    /// The bounding box of this <see cref="RenderObject"/>.
    /// </summary>
    public BoundingBox Bounds { get; set; } = BoundingBox.Empty;

    /// <summary>
    /// The rendering groups this <see cref="RenderObject"/> is visible to.
    /// </summary>
    public RenderGroup Groups { get; set; } = RenderGroup.Default;

    /// <summary>
    /// The material this <see cref="RenderObject"/> uses.
    /// </summary>
    public IMaterial Material { get; set; } = UnlitMaterial.Default;

    /// <summary>
    /// The number of indices to be drawn.
    /// </summary>
    public int IndexCount { get; set; }

    /// <summary>
    /// The type of indices being to be interpreted in the <see cref="IndexBuffer"/>.
    /// </summary>
    public IndexType IndexType { get; set; } = IndexType.UnsignedShort;

    /// <summary>
    /// The index buffer for this render object.
    /// </summary>
    public GraphicsBuffer? IndexBuffer { get; set; }

    /// <summary>
    /// The vertex buffer for this render object.
    /// </summary>
    public GraphicsBuffer? VertexBuffer { get; set; }
}
