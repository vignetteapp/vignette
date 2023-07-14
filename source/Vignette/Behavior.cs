// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;

namespace Vignette;

/// <summary>
/// A <see cref="Node"/> that processes itself per-frame.
/// </summary>
public abstract class Behavior : Node, IComparable<Behavior>
{
    /// <summary>
    /// The processing order for this <see cref="Behavior"/>.
    /// </summary>
    public int Order
    {
        get => order;
        set
        {
            if (order.Equals(value))
            {
                return;
            }

            order = value;
            OrderChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Whether this <see cref="Behavior"/> should be enabled or not affecting <see cref="Update(TimeSpan)"/> calls.
    /// </summary>
    public bool Enabled
    {
        get => enabled;
        set
        {
            if (enabled.Equals(value))
            {
                return;
            }

            enabled = value;
            EnabledChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Called when <see cref="Order"/> has been changed.
    /// </summary>
    public event EventHandler? OrderChanged;

    /// <summary>
    /// Called when <see cref="Enabled"/> has been changed.
    /// </summary>
    public event EventHandler? EnabledChanged;

    private int order;
    private bool enabled = true;

    /// <summary>
    /// Called once in the update loop after the <see cref="Node"/> has entered the node graph.
    /// </summary>
    public virtual void Load()
    {
    }

    /// <summary>
    /// Called every frame to perform updates on this <see cref="Behavior"/>.
    /// </summary>
    /// <param name="elapsed">The time elapsed between frames.</param>
    public virtual void Update(TimeSpan elapsed)
    {
    }

    /// <summary>
    /// Called once in the update loop before the <see cref="Node"/> exits the node graph.
    /// </summary>
    public virtual void Unload()
    {
    }

    public int CompareTo(Behavior? other)
    {
        if (other is null)
        {
            return -1;
        }

        int value = Depth.CompareTo(other.Depth);

        if (value != 0)
        {
            return value;
        }

        return Order.CompareTo(other.Order);
    }
}
