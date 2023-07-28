// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Jint.Native;
using Vignette.Scripting;

namespace Vignette;

/// <summary>
/// A <see cref="Node"/> that has behavior.
/// </summary>
public class Behavior : Node, IComparable<Behavior>
{
    /// <summary>
    /// The processing order for this <see cref="Node"/>.
    /// </summary>
    [ScriptVisible]
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
    /// Whether this <see cref="Node"/> should perform <see cref="Update(double)"/> calls.
    /// </summary>
    [ScriptVisible]
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
    private bool enabled = false;

    /// <summary>
    /// Called once after entering a world.
    /// </summary>
    public virtual void Load()
    {
        Invoke(load);
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    /// <param name="elapsed">The elapsed time between frames.</param>
    public virtual void Update(TimeSpan elapsed)
    {
        Invoke(update, elapsed.TotalSeconds);
    }

    /// <summary>
    /// Called once before leaving a world.
    /// </summary>
    public virtual void Unload()
    {
        Invoke(unload);
    }

    int IComparable<Behavior>.CompareTo(Behavior? other)
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

    private static readonly JsValue load = new JsString("load");
    private static readonly JsValue update = new JsString("update");
    private static readonly JsValue unload = new JsString("unload");
}
