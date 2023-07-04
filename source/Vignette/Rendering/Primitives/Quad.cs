// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Sekai.Graphics;
using Vignette.Rendering.Vertices;

namespace Vignette.Rendering.Primitives;

/// <summary>
/// A planar figure with 4 points.
/// </summary>
/// <typeparam name="T">The vertex type.</typeparam>
[StructLayout(LayoutKind.Sequential)]
public struct Quad<T> : IPrimitive<T>, IEquatable<Quad<T>>
    where T : unmanaged, IVertex, IEquatable<T>
{
    /// <summary>
    /// The top left vertex.
    /// </summary>
    public T TopLeft;

    /// <summary>
    /// The bottom left vertex.
    /// </summary>
    public T BottomLeft;

    /// <summary>
    /// The bottom right vertex.
    /// </summary>
    public T BottomRight;

    /// <summary>
    /// The top right vertex.
    /// </summary>
    public T TopRight;

    /// <summary>
    /// Creates a quad
    /// </summary>
    /// <param name="topLeft">The top left vertex.</param>
    /// <param name="bottomLeft">The bottom left vertex.</param>
    /// <param name="bottomRight">The bottom right vertex.</param>
    /// <param name="topRight">The top right vertex.</param>
    public Quad(T topLeft, T bottomLeft, T bottomRight, T topRight)
    {
        TopLeft = topLeft;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
        TopRight = topRight;
    }

    public readonly bool Equals(Quad<T> other)
    {
        return TopLeft.Equals(other.TopLeft) && BottomLeft.Equals(other.BottomLeft) && BottomRight.Equals(other.BottomRight) && TopRight.Equals(other.TopRight);
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Quad<T> quad && Equals(quad);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(TopLeft, BottomLeft, BottomRight, TopRight);
    }

    readonly PrimitiveType IPrimitive.Mode => PrimitiveType.TriangleList;

    readonly int IPrimitive.VertexCount => 4;

    readonly ReadOnlySpan<short> IPrimitive.GetIndices() => indices;

    ReadOnlySpan<T> IPrimitive<T>.GetVertices() => MemoryMarshal.CreateReadOnlySpan(ref TopLeft, 4);

    readonly ReadOnlySpan<byte> IPrimitive.GetVertices() => MemoryMarshal.AsBytes(((IPrimitive<T>)this).GetVertices());

    private static readonly short[] indices = new short[] { 0, 1, 3, 2, 3, 1 };

    public static bool operator ==(Quad<T> left, Quad<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Quad<T> left, Quad<T> right)
    {
        return !(left == right);
    }
}
