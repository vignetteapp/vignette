// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Vignette.Rendering.Primitives;

/// <summary>
/// Defines a mesh's vertex format.
/// </summary>
public readonly struct MeshFormat : IEquatable<MeshFormat>
{
    /// <summary>
    /// The attributes that make up this format.
    /// </summary>
    public MeshAttribute[] Attributes { get; }

    /// <summary>
    /// Creates a <see cref="MeshFormat"/>.
    /// </summary>
    /// <param name="attributes">The attributes that make up this format.</param>
    public MeshFormat(params MeshAttribute[] attributes)
    {
        Attributes = attributes;
    }

    public bool Equals(MeshFormat other)
    {
        return Attributes is not null && other.Attributes is not null && ((IStructuralEquatable)Attributes).Equals(other.Attributes, EqualityComparer<MeshAttribute>.Default);
    }

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is MeshFormat format && Equals(format);
    }

    public override readonly int GetHashCode()
    {
        return Attributes is not null ? ((IStructuralEquatable)Attributes).GetHashCode(EqualityComparer<MeshAttribute>.Default) : base.GetHashCode();
    }

    public static bool operator ==(MeshFormat left, MeshFormat right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MeshFormat left, MeshFormat right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Defines a single mesh attribute.
/// </summary>
/// <param name="Type">The attribute type.</param>
/// <param name="Count">The component count.</param>
/// <param name="Normalized">Whether values should be normalized.</param>
public readonly record struct MeshAttribute(MeshAttribteType Type, int Count, bool Normalized = false);
