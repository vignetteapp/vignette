// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Sekai.Graphics;

namespace Vignette.Rendering.Primitives;

/// <summary>
/// A primitive with an arbitrary shape.
/// </summary>
public struct Mesh : IPrimitive, IEquatable<Mesh>
{
    /// <summary>
    /// Determines how the vertices are used when drawn.
    /// </summary>
    public MeshDrawMode Mode { get; set; }

    /// <summary>
    /// Determines the vertex format of this mesh.
    /// </summary>
    public MeshFormat Format { get; }

    public ReadOnlySpan<byte> Vertices
    {
        readonly get => vertices is null ? Span<byte>.Empty : vertices;
        set => vertices = value.ToArray();
    }

    public ReadOnlySpan<short> Indices
    {
        readonly get => indices is null ? Span<short>.Empty : indices;
        set => indices = value.ToArray();
    }

    private byte[]? vertices;
    private short[]? indices;

    /// <summary>
    /// Creates a new <see cref="Mesh"/>.
    /// </summary>
    /// <param name="format">The format defining the mesh vertices.</param>
    public Mesh(MeshFormat format)
    {
        Format = format;
    }

    readonly int IPrimitive.VertexCount => Vertices.Length / Format.SizeOfFormat();
    readonly PrimitiveType IPrimitive.Mode => Mode.AsPrimitiveType();
    readonly ReadOnlySpan<short> IPrimitive.GetIndices() => indices is null ? Span<short>.Empty : indices;
    readonly ReadOnlySpan<byte> IPrimitive.GetVertices() => vertices is null ? Span<byte>.Empty : vertices;

    public readonly bool Equals(Mesh other)
    {
        return Mode == other.Mode &&
               Format == other.Format &&
               indices is not null && ((IStructuralEquatable)indices).Equals(other.indices, EqualityComparer<short>.Default) &&
               vertices is not null && ((IStructuralEquatable)vertices).Equals(other.vertices, EqualityComparer<byte>.Default);
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Mesh mesh && Equals(mesh);
    }

    public override readonly int GetHashCode()
    {
        int hash = HashCode.Combine(Mode, Format);

        if (indices is not null)
        {
            hash += ((IStructuralEquatable)indices).GetHashCode(EqualityComparer<short>.Default);
        }

        if (vertices is not null)
        {
            hash += ((IStructuralEquatable)vertices).GetHashCode(EqualityComparer<byte>.Default);
        }

        return hash;
    }

    public static bool operator ==(Mesh left, Mesh right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Mesh left, Mesh right)
    {
        return !(left == right);
    }
}

internal static class MeshFormatExtensions
{
    public static InputLayoutFormat AsLayoutFormat(this MeshAttribteType type) => type switch
    {
        MeshAttribteType.Byte => InputLayoutFormat.Byte,
        MeshAttribteType.UnsignedByte => InputLayoutFormat.UnsignedByte,
        MeshAttribteType.Short => InputLayoutFormat.Short,
        MeshAttribteType.UnsignedShort => InputLayoutFormat.UnsignedShort,
        MeshAttribteType.Int => InputLayoutFormat.Int,
        MeshAttribteType.UnsignedInt => InputLayoutFormat.UnsignedInt,
        MeshAttribteType.Half => InputLayoutFormat.Half,
        MeshAttribteType.Float => InputLayoutFormat.Float,
        MeshAttribteType.Double => InputLayoutFormat.Double,
        _ => throw new ArgumentOutOfRangeException(nameof(type)),
    };

    public static PrimitiveType AsPrimitiveType(this MeshDrawMode mode) => mode switch
    {
        MeshDrawMode.Points => PrimitiveType.PointList,
        MeshDrawMode.Triangles => PrimitiveType.TriangleList,
        MeshDrawMode.TriangleStrip => PrimitiveType.TriangleStrip,
        MeshDrawMode.Lines => PrimitiveType.LineList,
        MeshDrawMode.LineStrip => PrimitiveType.LineStrip,
        _ => throw new ArgumentOutOfRangeException(nameof(mode)),
    };

    public static InputLayoutDescription AsDescription(this MeshFormat format)
    {
        if (!formats.TryGetValue(format, out var layout))
        {
            var members = new InputLayoutMember[format.Attributes.Length];

            for (int i = 0; i < members.Length; i++)
            {
                members[i] = new InputLayoutMember
                (
                    format.Attributes[i].Count,
                    format.Attributes[i].Normalized,
                    format.Attributes[i].Type.AsLayoutFormat()
                );
            }

            layout = new InputLayoutDescription(members);
            formats.Add(format, layout);
        }

        return layout;
    }

    public static int SizeOfFormat(this MeshAttribteType type) => type.AsLayoutFormat().SizeOfFormat();

    public static int SizeOfFormat(this MeshAttribute attrib) => attrib.SizeOfFormat() * attrib.Count;

    public static int SizeOfFormat(this MeshFormat format)
    {
        int total = 0;

        for (int i = 0; i < format.Attributes.Length; i++)
        {
            total += format.Attributes[i].SizeOfFormat();
        }

        return total;
    }

    private static readonly Dictionary<MeshFormat, InputLayoutDescription> formats = new();
}
