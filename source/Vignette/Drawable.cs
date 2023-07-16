// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Vignette.Graphics;

namespace Vignette;

/// <summary>
/// A <see cref="Node"/> that is capable of drawing.
/// </summary>
public abstract class Drawable : Behavior
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
    /// Called when <see cref="Visible"/> has been changed.
    /// </summary>
    public event EventHandler? VisibleChanged;

    private bool visible = true;

    /// <summary>
    /// Called every frame to perform drawing operations on this <see cref="Drawable"/>.
    /// </summary>
    /// <param name="context">The drawable rendering context.</param>
    public virtual void Draw(RenderContext context)
    {
    }
}
