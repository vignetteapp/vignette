// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Sekai.Graphics;

namespace Vignette.Rendering.Vertices;

/// <summary>
/// A textured vertex.
/// </summary>
public struct TexturedVertex : IVertex, IEquatable<TexturedVertex>
{
    /// <summary>
    /// The vertex position.
    /// </summary>
    public Vector3 Position;

    /// <summary>
    /// The vertex texture coordinate.
    /// </summary>
    public Vector2 TexCoord;

    /// <summary>
    /// The vertex color.
    /// </summary>
    public Vector4 Color;

    /// <summary>
    /// Creates a new <see cref="TexturedVertex"/>.
    /// </summary>
    /// <param name="position">The vertex position.</param>
    /// <param name="texCoord">The vertex texture coordinate.</param>
    /// <param name="color">The vertex color.</param>
    public TexturedVertex(Vector3 position, Vector2 texCoord, Vector4 color)
    {
        Color = color;
        Position = position;
        TexCoord = texCoord;
    }

    public readonly bool Equals(TexturedVertex other)
    {
        return Position.Equals(other.Position) && TexCoord.Equals(other.TexCoord) && Color.Equals(other.Color);
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is TexturedVertex vertex && Equals(vertex);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Position, TexCoord, Color);
    }

    public static bool operator ==(TexturedVertex left, TexturedVertex right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TexturedVertex left, TexturedVertex right)
    {
        return !(left == right);
    }

    static InputLayoutDescription IVertex.Layout { get; } = new
    (
        new InputLayoutMember(3, false, InputLayoutFormat.Float),
        new InputLayoutMember(2, false, InputLayoutFormat.Float),
        new InputLayoutMember(4, false, InputLayoutFormat.Float)
    );
}
