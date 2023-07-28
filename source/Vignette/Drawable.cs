// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Numerics;
using Jint.Native;
using Vignette.Graphics;
using Vignette.Scripting;

namespace Vignette;

/// <summary>
/// A <see cref="Node"/> that can be drawn.
/// </summary>
public class Drawable : Behavior, ISpatialObject
{
    /// <summary>
    /// Whether this <see cref="Node"/> should perform <see cref="Draw()"/> calls.
    /// </summary>
    [ScriptVisible]
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
    /// The node's scaling.
    /// </summary>
    [ScriptVisible]
    public Vector3 Scale { get; set; } = Vector3.One;

    /// <summary>
    /// The node's shearing.
    /// </summary>
    [ScriptVisible]
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

    private bool visible = false;
    private Matrix4x4 shear = Matrix4x4.Identity;

    /// <summary>
    /// The node's matrix.
    /// </summary>
    protected override Matrix4x4 Matrix => shear * Matrix4x4.CreateScale(Scale) * base.Matrix;

    /// <summary>
    /// Called when performing draw operations.
    /// </summary>
    /// <param name="context">The rendering context.</param>
    public virtual void Draw(RenderContext context)
    {
        Invoke(draw, context);
    }

    private static readonly JsValue draw = new JsString("draw");
}
