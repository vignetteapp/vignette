// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System.Reflection;
using Jint;
using Jint.Runtime.Interop;
using Sekai;
using Vignette.Scripting;

namespace Vignette;

public class VignetteGameOptions : GameOptions
{
    /// <summary>
    /// The scripting engine options.
    /// </summary>
    public Options Engine = new();
}

public static class VignetteGameOptionsExtensions
{
    /// <summary>
    /// Enable use of scripts.
    /// </summary>
    /// <param name="options"></param>
    public static void UseScripts(this VignetteGameOptions options)
    {
        options.Engine.Interop.TypeResolver = typeResolver;
        options.Engine.Interop.WrapObjectHandler = wrapObjectHandler;
    }

    private static readonly WrapObjectDelegate wrapObjectHandler = static (Engine engine, object target) =>
    {
        if (target is IScriptObject wrapped)
        {
            return (ObjectWrapper)(wrapped.JSObject ??= new ObjectWrapper(engine, target));
        }
        else
        {
            return new ObjectWrapper(engine, target);
        }
    };

    private static readonly TypeResolver typeResolver = new()
    {
        MemberFilter = m =>
        {
            return m.GetCustomAttribute<ScriptVisibleAttribute>() is not null;
        },
    };
}
