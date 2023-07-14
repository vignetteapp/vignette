// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Numerics;
using Vignette.Graphics;

namespace Vignette;

/// <summary>
/// A <see cref="Node"/> that is capable of drawing.
/// </summary>
public abstract class Drawable : Behavior, IWorld
{
    /// <summary>
    /// Whether this <see cref="Drawable"/> should be drawn or not.
    /// </summary>
    public bool Visible
    {
        get => visible;
        set
        {
            if (visible.Equals(value))
            {
                return;
            }

            visible = value;
            VisibleChanged?.Invoke(this, EventArgs.Empty);
        }
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

    /// <summary>
    /// Called when <see cref="Visible"/> has been changed.
    /// </summary>
    public event EventHandler? VisibleChanged;

    private bool visible = true;
    private Matrix4x4 scale = Matrix4x4.Identity;
    private Matrix4x4 shear = Matrix4x4.Identity;

    /// <summary>
    /// Called every frame to perform drawing operations on this <see cref="Drawable"/>.
    /// </summary>
    /// <param name="context">The drawable rendering context.</param>
    public virtual void Draw(RenderContext context)
    {
    }

    Matrix4x4 IWorld.LocalMatrix => shear * scale * Matrix4x4.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z) * Matrix4x4.CreateTranslation(Position);
    Matrix4x4 IWorld.WorldMatrix => Parent is not IWorld provider ? ((IWorld)this).LocalMatrix : provider.LocalMatrix * ((IWorld)this).LocalMatrix;
}
