// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Numerics;
using Vignette.Graphics;

namespace Vignette;

/// <summary>
/// A <see cref="Node"/> that provides a world space.
/// </summary>
public abstract class Renderable : Behavior, IWorld
{
    /// <summary>
    /// The renderable's position.
    /// </summary>
    public Vector3 Position
    {
        get => position.Translation;
        set => position = Matrix4x4.CreateTranslation(value);
    }

    /// <summary>
    /// The renderable's rotation as pitch, yaw, and roll.
    /// </summary>
    public Vector3 Rotation
    {
        get => new(MathF.Atan2(rotation[0, 2], rotation[2, 2]), MathF.Asin(-rotation[1, 2]), MathF.Atan2(rotation[1, 0], rotation[1, 1]));
        set => rotation = Matrix4x4.CreateFromYawPitchRoll(value.Y, value.X, value.Z);
    }

    /// <summary>
    /// The renderable's scaling.
    /// </summary>
    public Vector3 Scale
    {
        get => new(scale[0, 0], scale[1, 1], scale[2, 2]);
        set => scale = Matrix4x4.CreateScale(value);
    }

    /// <summary>
    /// The renderable's shearing.
    /// </summary>
    public Vector3 Shear
    {
        get => new(shear[0, 1], shear[0, 2], shear[1, 2]);
        set
        {
            shear[0, 1] = value.X;
            shear[0, 2] = value.Y;
            shear[1, 2] = value.Z;
        }
    }

    private Matrix4x4 scale = Matrix4x4.Identity;
    private Matrix4x4 shear = Matrix4x4.Identity;
    private Matrix4x4 rotation = Matrix4x4.Identity;
    private Matrix4x4 position = Matrix4x4.Identity;

    Matrix4x4 IWorld.LocalMatrix => shear * scale * rotation * position;
    Matrix4x4 IWorld.WorldMatrix => Parent is not IWorld provider ? ((IWorld)this).LocalMatrix : provider.LocalMatrix * ((IWorld)this).LocalMatrix;
}
