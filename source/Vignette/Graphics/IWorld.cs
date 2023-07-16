// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System.Numerics;

namespace Vignette.Graphics;

/// <summary>
/// An interface for objects providing world space info.
/// </summary>
public interface IWorld
{
    /// <summary>
    /// The world's position.
    /// </summary>
    Vector3 Position { get; }

    /// <summary>
    /// The world's rotation.
    /// </summary>
    Vector3 Rotation { get; }

    /// <summary>
    /// The world's scaling.
    /// </summary>
    Vector3 Scale { get; }

    /// <summary>
    /// The world's shearing.
    /// </summary>
    Vector3 Shear { get; }

    /// <summary>
    /// The world's local matrix.
    /// </summary>
    Matrix4x4 LocalMatrix { get; }

    /// <summary>
    /// The world's world matrix.
    /// </summary>
    Matrix4x4 WorldMatrix { get; }
}
