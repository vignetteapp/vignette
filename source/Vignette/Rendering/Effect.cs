// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Sekai.Graphics;

namespace Vignette.Rendering;

/// <summary>
/// Defines how primitives should be drawn to the screen.
/// </summary>
public readonly struct Effect : IEquatable<Effect>
{
    private readonly ShaderCode[] shaderCode;
    private readonly IDictionary<string, IParameter> parameters;

    public Effect()
    {
        shaderCode = Array.Empty<ShaderCode>();
        parameters = ImmutableDictionary<string, IParameter>.Empty;
    }

    private Effect(ShaderCode shVert, ShaderCode shFrag, IDictionary<string, IParameter> parameters)
    {
        this.parameters = parameters;
        this.shaderCode = new[] { shVert, shFrag };
    }

    /// <summary>
    /// Checks whether a named parameter of a given type is present on this <see cref="Effect"/>.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <param name="name">The parameter name.</param>
    /// <returns><see langword="true"/> if the parameter exists. Otherwise, returns <see langword="false"/>.</returns>
    public bool HasParameter<T>(string name)
        where T : class
    {
        if (!parameters.TryGetValue(name, out var param))
        {
            return false;
        }

        return param is T;
    }

    /// <summary>
    /// Gets the named parameter.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <param name="name">The parameter name.</param>
    /// <returns>The parameter.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the parameter is not found in this <see cref="Effect"/>.</exception>
    /// <exception cref="InvalidCastException">Thrown when the parameter exists but the type argument does not match the parameter type.</exception>
    public Parameter<T> GetParameter<T>(string name)
        where T : class
    {
        if (!parameters.TryGetValue(name, out var param))
        {
            throw new KeyNotFoundException($"The effect has no parameter named \"{name}\".");
        }

        if (param is not Parameter<T> typedParam)
        {
            throw new InvalidCastException($"The effect parameter \"{name}\" is not a {nameof(Parameter<T>)}.");
        }

        return typedParam;
    }

    /// <summary>
    /// Gets all parameters this effect has.
    /// </summary>
    /// <returns>An enumeration of parameters.</returns>
    public IEnumerable<IParameter> GetParameters() => parameters.Values;

    /// <summary>
    /// Gets the named parameter.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <param name="name">The parameter name.</param>
    /// <param name="typedParam">The retrieved parameter.</param>
    /// <returns><see langword="true"/> if the parameter exists. Otherwise, returns <see langword="false"/>.</returns>
    public bool TryGetParameter<T>(string name, out Parameter<T> typedParam)
        where T : class
    {
        try
        {
            typedParam = GetParameter<T>(name);
            return true;
        }
        catch
        {
            typedParam = default;
            return false;
        }
    }

    /// <summary>
    /// Creates an <see cref="Effect"/> from a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">The stream to be read.</param>
    /// <param name="encoding">The text encoding.</param>
    /// <returns>An effect.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stream failed to be read.</exception>
    public static Effect From(Stream stream, Encoding? encoding = null)
    {
        Span<byte> buffer = stackalloc byte[(int)stream.Length];

        if (stream.Read(buffer) != stream.Length)
        {
            throw new InvalidOperationException("Failed to read stream.");
        }

        return From(buffer, encoding);
    }

    /// <summary>
    /// Creates an <see cref="Effect"/> from a <see cref="ReadOnlySpan{byte}"/>.
    /// </summary>
    /// <param name="bytes">The bytes to be read.</param>
    /// <param name="encoding">The text encoding.</param>
    /// <returns>An effect.</returns>
    public static Effect From(ReadOnlySpan<byte> bytes, Encoding? encoding = null)
    {
        return From((encoding ?? Encoding.UTF8).GetString(bytes));
    }

    /// <summary>
    /// Creates an <see cref="Effect"/> from a <see langword="string"/>.
    /// </summary>
    /// <param name="text">The text to be compiled.</param>
    /// <returns>An effect.</returns>
    public static Effect From(string text)
    {
        string code = sh_common + text;

        var shVert = ShaderCode.From(code, ShaderStage.Vertex, sh_vert, ShaderLanguage.HLSL);
        var shFrag = ShaderCode.From(code, ShaderStage.Fragment, sh_frag, ShaderLanguage.HLSL);

        var shVertReflect = shVert.Reflect();
        var shFragReflect = shFrag.Reflect();

        var parameters = new Dictionary<string, IParameter>();

        if (shVertReflect.Uniforms is not null)
        {
            foreach (var buffer in shVertReflect.Uniforms)
                parameters[buffer.Name] = new Parameter<GraphicsBuffer>(buffer.Name, buffer.Binding);
        }

        if (shVertReflect.Textures is not null)
        {
            foreach (var texture in shVertReflect.Textures)
                parameters[texture.Name] = new Parameter<Texture>(texture.Name, texture.Binding);
        }

        if (shFragReflect.Uniforms is not null)
        {
            foreach (var buffer in shFragReflect.Uniforms)
                parameters[buffer.Name] = new Parameter<GraphicsBuffer>(buffer.Name, buffer.Binding);
        }

        if (shFragReflect.Textures is not null)
        {
            foreach (var texture in shFragReflect.Textures)
                parameters[texture.Name] = new Parameter<Texture>(texture.Name, texture.Binding);
        }

        return new(shVert, shFrag, parameters);
    }

    public bool Equals(Effect other)
    {
        return ((IStructuralEquatable)shaderCode).Equals(other.shaderCode, EqualityComparer<ShaderCode>.Default);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Effect effect && Equals(effect);
    }

    public override int GetHashCode()
    {
        return ((IStructuralEquatable)shaderCode).GetHashCode(EqualityComparer<ShaderCode>.Default);
    }

    public static bool operator ==(Effect left, Effect right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Effect left, Effect right)
    {
        return !(left == right);
    }

    public static implicit operator ShaderCode[](Effect effect) => effect.shaderCode;

    private const string sh_frag = "Pixel";
    private const string sh_vert = "Vertex";

    private const string sh_common =
@"
#define P_MATRIX g_internal_ProjMatrix
#define V_MATRIX g_internal_ViewMatrix
#define M_MATRIX g_internal_ModelMatrix
#define OBJECT_TO_CLIP(a) mul(mul(V_MATRIX, M_MATRIX), a)
#define OBJECT_TO_VIEW(a) mul(P_MATRIX, OBJECT_TO_CLIP(a))

cbuffer g_internal_Transform : register(b89)
{
    float4x4 g_internal_ProjMatrix;
    float4x4 g_internal_ViewMatrix;
    float4x4 g_internal_ModelMatrix;
};
";
}
