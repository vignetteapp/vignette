// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Jint;
using Jint.Native;
using Jint.Native.Object;

namespace Vignette.Scripting;

/// <summary>
/// An object that can be scripted in JavaScript.
/// </summary>
public abstract class ScriptObject : IScriptObject
{
    private readonly WeakReference jsObject = new(null);

    /// <summary>
    /// Invokes a JavaScript function.
    /// </summary>
    /// <param name="thisObj">The object implementing <see cref="IScriptObject"/>.</param>
    /// <param name="property">The property to call.</param>
    /// <param name="arguments">The arguments to pass to the function.</param>
    /// <returns>The result of calling the function.</returns>
    public object? Invoke(JsValue property, params object?[] arguments)
    {
        if (jsObject.Target is not ObjectInstance obj)
        {
            return null;
        }

        var target = obj.Get(property);

        if (target.IsUndefined())
        {
            return null;
        }

        return obj.Engine.Invoke(target, obj, arguments);
    }

    /// <summary>
    /// Invokes a JavaScript function.
    /// </summary>
    /// <typeparam name="T">The type to cast the result as.</typeparam>
    /// <param name="thisObj">The object implementing <see cref="IScriptObject"/>.</param>
    /// <param name="property">The property to call.</param>
    /// <param name="arguments">The arguments to pass to the function.</param>
    /// <returns>The result of calling the function as <typeparamref name="T"/>.</returns>
    public T? Invoke<T>(JsValue property, params object?[] arguments)
    {
        return (T?)Invoke(property, arguments);
    }

    ObjectInstance? IScriptObject.JSObject
    {
        get => jsObject.Target as ObjectInstance;
        set => jsObject.Target = value;
    }
}

/// <summary>
/// Denotes that a the object can be scripted in JavaScript.
/// </summary>
internal interface IScriptObject
{
    /// <summary>
    /// The JavaScript Object wrapping <see langword="this"/> object.
    /// </summary>
    ObjectInstance? JSObject { set; get; }
}
