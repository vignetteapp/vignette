// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Sekai.Graphics;
using Vignette.Rendering.Vertices;

namespace Vignette.Rendering.Primitives;

/// <summary>
/// A geometric primitive with 2 points.
/// </summary>
/// <typeparam name="T">The vertex type.</typeparam>
[StructLayout(LayoutKind.Sequential)]
public struct Line<T> : IPrimitive<T>, IEquatable<Line<T>>
    where T : unmanaged, IVertex, IEquatable<T>
{
    /// <summary>
    /// The start vertex.
    /// </summary>
    public T Start;

    /// <summary>
    /// The end vertex.
    /// </summary>
    public T End;

    /// <summary>
    /// Creates a line.
    /// </summary>
    /// <param name="start">The start vertex.</param>
    /// <param name="end">The end vertex.</param>
    public Line(T start, T end)
    {
        Start = start;
        End = end;
    }

    readonly PrimitiveType IPrimitive.Mode => PrimitiveType.LineList;

    readonly int IPrimitive.VertexCount => 2;

    readonly ReadOnlySpan<short> IPrimitive.GetIndices() => indices;

    ReadOnlySpan<T> IPrimitive<T>.GetVertices() => MemoryMarshal.CreateReadOnlySpan(ref Start, 2);

    readonly ReadOnlySpan<byte> IPrimitive.GetVertices() => MemoryMarshal.AsBytes(((IPrimitive<T>)this).GetVertices());

    public readonly bool Equals(Line<T> other)
    {
        return Start.Equals(other.Start) && End.Equals(other.End);
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Line<T> line && Equals(line);
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }

    public static bool operator ==(Line<T> left, Line<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Line<T> left, Line<T> right)
    {
        return !(left == right);
    }

    private static readonly short[] indices = new short[] { 0, 1 };
}
