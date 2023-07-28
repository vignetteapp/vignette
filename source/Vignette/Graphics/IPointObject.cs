// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System.Numerics;

namespace Vignette.Graphics;

/// <summary>
/// An object that has a position and rotation
/// </summary>
public interface IPointObject
{
    /// <summary>
    /// The point's position.
    /// </summary>
    Vector3 Position { get; }

    /// <summary>
    /// The point's rotation.
    /// </summary>
    Vector3 Rotation { get; }

    /// <summary>
    /// The point's matrix.
    /// </summary>
    Matrix4x4 Matrix { get; }
}
