// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System.Numerics;
using Sekai.Mathematics;

namespace Vignette.Graphics;

/// <summary>
/// An interface for objects providing clip space info.
/// </summary>
public interface IProjector
{
    /// <summary>
    /// The projector's position.
    /// </summary>
    Vector3 Position { get; }

    /// <summary>
    /// The projector's rotation.
    /// </summary>
    Vector3 Rotation  { get; }

    /// <summary>
    /// The projector's view matrix.
    /// </summary>
    Matrix4x4 ViewMatrix { get; }

    /// <summary>
    /// The projector's projection matrix.
    /// </summary>
    Matrix4x4 ProjMatrix { get; }

    /// <summary>
    /// The projector's render groups.
    /// </summary>
    RenderGroup Groups { get; }

    /// <summary>
    /// The projector's bounding frustum.
    /// </summary>
    BoundingFrustum Frustum { get; }
}
