// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Sekai.Graphics;

namespace Vignette.Graphics;

public readonly struct Effect : IEquatable<Effect>
{
    private readonly ShaderCode[] shaderCodes;

    private Effect(params ShaderCode[] shaderCodes)
    {
        this.shaderCodes = shaderCodes;
    }

    public readonly bool Equals(Effect other)
    {
        return ((IStructuralEquatable)shaderCodes).Equals(other.shaderCodes, EqualityComparer<ShaderCode>.Default);
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Effect effect && Equals(effect);
    }

    public override readonly int GetHashCode()
    {
        HashCode hash = default;

        for (int i = 0; i < shaderCodes.Length; i++)
        {
            hash.Add(shaderCodes[i]);
        }

        return hash.ToHashCode();
    }

    /// <summary>
    /// Creates a new <see cref="Effect"/> from HLSL shader code.
    /// </summary>
    /// <param name="code">The HLSL shader code.</param>
    /// <param name="layout">The reflected input layout.</param>
    /// <param name="properties">The reflected shader properties.</param>
    /// <returns>A new <see cref="Effect"/>.</returns>
    internal static Effect From(string code, out InputLayoutDescription layout, out IProperty[] properties)
    {
        var shVert = ShaderCode.From(code, ShaderStage.Vertex, sh_vert, ShaderLanguage.HLSL);
        var shFrag = ShaderCode.From(code, ShaderStage.Fragment, sh_frag, ShaderLanguage.HLSL);

        var shVertReflect = shVert.Reflect();
        var shFragReflect = shFrag.Reflect();

        if (shVertReflect.Inputs is not null)
        {
            var format = new InputLayoutMember[shVertReflect.Inputs.Length];

            for (int i = 0; i < shVertReflect.Inputs.Length; i++)
            {
                format[i] = format_members[shVertReflect.Inputs[i].Type];
            }

            layout = new(format);
        }
        else
        {
            layout = new();
        }

        var props = new List<IProperty>();

        if (shVertReflect.Uniforms is not null)
        {
            foreach (var uniform in shVertReflect.Uniforms)
                props.Add(new UniformProperty(uniform.Name, uniform.Binding));
        }

        if (shVertReflect.Textures is not null)
        {
            foreach (var texture in shVertReflect.Textures)
                props.Add(new TextureProperty(texture.Name, texture.Binding));
        }

        if (shFragReflect.Uniforms is not null)
        {
            foreach (var uniform in shFragReflect.Uniforms)
                props.Add(new UniformProperty(uniform.Name, uniform.Binding));
        }

        if (shFragReflect.Textures is not null)
        {
            foreach (var texture in shFragReflect.Textures)
                props.Add(new TextureProperty(texture.Name, texture.Binding));
        }

        properties = props.ToArray();

        return new Effect(shVert, shFrag);
    }

    public static bool operator ==(Effect left, Effect right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Effect left, Effect right)
    {
        return !(left == right);
    }

    public static explicit operator ShaderCode[](Effect effect) => effect.shaderCodes;

    private const string sh_vert = "Vertex";
    private const string sh_frag = "Pixel";

    private static readonly IReadOnlyDictionary<string, InputLayoutMember> format_members = ImmutableDictionary.CreateRange
    (
        new KeyValuePair<string, InputLayoutMember>[]
        {
            KeyValuePair.Create<string, InputLayoutMember>("int", new(1, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("ivec2", new(2, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("ivec3", new(3, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("ivec4", new(4, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat2", new(4, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat2x3", new(6, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat2x4", new(8, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat3", new(9, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat3x2", new(6, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat3x4", new(12, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat4", new(16, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat4x2", new(8, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("imat4x3", new(12, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("uint", new(1, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("uvec2", new(2, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("uvec3", new(3, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("uvec4", new(4, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat2", new(4, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat2x3", new(6, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat2x4", new(8, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat3", new(9, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat3x2", new(6, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat3x4", new(12, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat4", new(16, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat4x2", new(8, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("umat4x3", new(12, false, InputLayoutFormat.UnsignedInt)),
            KeyValuePair.Create<string, InputLayoutMember>("bool", new(1, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bvec2", new(2, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bvec3", new(3, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bvec4", new(4, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat2", new(4, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat2x3", new(6, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat2x4", new(8, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat3", new(9, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat3x2", new(6, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat3x4", new(12, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat4", new(16, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat4x2", new(8, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("bmat4x3", new(12, false, InputLayoutFormat.Int)),
            KeyValuePair.Create<string, InputLayoutMember>("float", new(1, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("vec2", new(2, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("vec3", new(3, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("vec4", new(4, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat2", new(4, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat2x3", new(6, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat2x4", new(8, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat3", new(9, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat3x2", new(6, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat3x4", new(12, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat4", new(16, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat4x2", new(8, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("mat4x3", new(12, false, InputLayoutFormat.Float)),
            KeyValuePair.Create<string, InputLayoutMember>("double", new(1, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dvec2", new(2, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dvec3", new(3, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dvec4", new(4, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat2", new(4, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat2x3", new(6, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat2x4", new(8, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat3", new(9, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat3x2", new(6, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat3x4", new(12, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat4", new(16, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat4x2", new(8, false, InputLayoutFormat.Double)),
            KeyValuePair.Create<string, InputLayoutMember>("dmat4x3", new(12, false, InputLayoutFormat.Double)),
        }
    );
}
