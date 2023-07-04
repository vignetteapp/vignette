// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Vignette.Rendering;

/// <summary>
/// Represents a typed parameter in an <see cref="Effect"/>.
/// </summary>
/// <typeparam name="T">The parameter type.</typeparam>
public readonly struct Parameter<T> : IParameter, IEquatable<Parameter<T>>
    where T : class
{
    public string Name { get; }

    public int Binding { get; }

    internal Parameter(string name, int binding)
    {
        Name = name;
        Binding = binding;
    }

    public bool Equals(Parameter<T> other)
    {
        return Name.Equals(other.Name) && Binding.Equals(other.Binding);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Parameter<T> parameter && Equals(parameter);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Binding);
    }

    public static bool operator ==(Parameter<T> left, Parameter<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Parameter<T> left, Parameter<T> right)
    {
        return !(left == right);
    }
}

/// <summary>
/// Represents a parameter in an <see cref="Effect"/>.
/// </summary>
public interface IParameter
{
    /// <summary>
    /// The parameter's name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The parameter's binding slot.
    /// </summary>
    int Binding { get; }
}
