// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System.Numerics;

namespace Vignette.Graphics;

/// <summary>
/// An interface for objects providing spatial info.
/// </summary>
public interface ISpatialObject : IPointObject
{
    /// <summary>
    /// The world's scaling.
    /// </summary>
    Vector3 Scale { get; }

    /// <summary>
    /// The world's shearing.
    /// </summary>
    Vector3 Shear { get; }
}
