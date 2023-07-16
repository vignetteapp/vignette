// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System.Numerics;
using Sekai.Mathematics;
using Vignette.Graphics;

namespace Vignette;

/// <summary>
/// Represents a light source.
/// </summary>
public abstract class Light : Node, IProjector
{
    /// <summary>
    /// The light source's view matrix.
    /// </summary>
    protected abstract Matrix4x4 ViewMatrix { get; }

    /// <summary>
    /// The light source's projection matrix.
    /// </summary>
    protected abstract Matrix4x4 ProjMatrix { get; }

    /// <summary>
    /// The light source's bounding frustum.
    /// </summary>
    public BoundingFrustum Frustum => BoundingFrustum.FromMatrix(ProjMatrix);

    Matrix4x4 IProjector.ViewMatrix => ViewMatrix;
    Matrix4x4 IProjector.ProjMatrix => ProjMatrix;
    RenderGroup IProjector.Groups => RenderGroup.Default;
}
