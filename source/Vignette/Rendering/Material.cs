// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Sekai.Graphics;

namespace Vignette.Rendering;

/// <summary>
/// Represents a collection of named shader resources.
/// </summary>
public sealed class Material
{
    /// <summary>
    /// The effect used by this material.
    /// </summary>
    public Effect Effect { get; }

    private readonly Dictionary<string, IProperty> properties = new();

    private Material(Effect effect)
    {
        Effect = effect;
    }

    /// <summary>
    /// Sets a named texture on this <see cref="Material"/>.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="texture">The texture to set.</param>
    public void SetProperty(string name, Texture texture)
    {
        if ((texture.Usage & TextureUsage.Resource) == 0)
        {
            throw new ArgumentException($"The texture must have the {nameof(TextureUsage.Resource)} flag to be used on materials.");
        }

        set(name, texture);
    }

    /// <summary>
    /// Sets a named buffer on this <see cref="Material"/>.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="buffer">The buffer to set.</param>
    /// <exception cref="ArgumentException">Thrown when the buffer is not a <see cref="BufferType.Uniform"/>.</exception>
    public void SetProperty(string name, GraphicsBuffer buffer)
    {
        if (buffer.Type is not BufferType.Uniform)
        {
            throw new ArgumentException($"The buffer must be a {nameof(BufferType.Uniform)}.", nameof(buffer));
        }

        set(name, buffer);
    }

    /// <summary>
    /// Gets the value of a named property stored on this <see cref="Material"/>.
    /// </summary>
    /// <typeparam name="T">The property type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <returns>The property value.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the property does not exist on this <see cref="Material"/>.</exception>
    /// <exception cref="InvalidCastException">Thrown when the property type does not match the type argument.</exception>
    public T GetProperty<T>(string name)
        where T : class
    {
        if (!properties.TryGetValue(name, out var prop))
        {
            throw new KeyNotFoundException($"Material has no property named \"{name}\".");
        }

        if (prop is not Property<T> typedProp)
        {
            throw new InvalidCastException($"Property value cannot be casted to {typeof(T)}.");
        }

        return typedProp.Value;
    }

    /// <summary>
    /// Checks whether a named property is present on this <see cref="Material"/>.
    /// </summary>
    /// <typeparam name="T">The property type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <returns><see langword="true"/> if the property exists. Otherwise, returns <see langword="false"/>.</returns>
    public bool HasProperty<T>(string name)
        where T : class
    {
        if (!properties.TryGetValue(name, out var prop))
        {
            return false;
        }

        return prop is Property<T>;
    }

    /// <summary>
    /// Gets the value of a named property.
    /// </summary>
    /// <typeparam name="T">The property type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <param name="value">The returned value.</param>
    /// <returns><see langword="true"/> if the value has returned. Otherwise, returns <see langword="false"/>.</returns>
    public bool TryGetProperty<T>(string name, [NotNullWhen(true)] out T? value)
        where T : class
    {
        if (!properties.TryGetValue(name, out var prop))
        {
            value = null;
            return false;
        }

        if (prop is not Property<T> typedProp)
        {
            value = null;
            return false;
        }

        value = typedProp.Value;
        return true;
    }

    /// <summary>
    /// Removes a named property from this <see cref="Material"/>.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns><see langword="true"/> if the property has been removed. Otherwise, returns <see langword="false"/>.</returns>
    public bool RemoveProperty(string name)
    {
        return properties.Remove(name);
    }

    /// <summary>
    /// Removes a named property of a given type from this <see cref="Material"/>.
    /// </summary>
    /// <typeparam name="T">The property type.</typeparam>
    /// <param name="name">The property name.</param>
    /// <param name="value">The removed property's value.</param>
    /// <returns><see langword="true"/> if the property has been removed. Otherwise, returns <see langword="false"/>.</returns>
    public bool RemoveProperty<T>(string name, [NotNullWhen(true)] out T? value)
        where T : class
    {
        if (!properties.TryGetValue(name, out var prop))
        {
            value = null;
            return false;
        }

        if (prop is not Property<T> typedProp)
        {
            value = null;
            return false;
        }

        value = typedProp.Value;
        properties.Remove(name);
        return true;
    }

    /// <summary>
    /// Creates a new <see cref="Material"/>.
    /// </summary>
    /// <param name="effect"></param>
    /// <returns></returns>
    public static Material Create(Effect effect)
    {
        return new Material(effect);
    }

    /// <summary>
    /// Creates a deep clone of an existing <see cref="Material"/>.
    /// </summary>
    /// <returns>A copy of a <see cref="Material"/>.</returns>
    public static Material Create(Material other)
    {
        var m = new Material(other.Effect);

        foreach (var prop in other.properties.Values)
        {
            switch (prop)
            {
                case Property<Texture> textureProp:
                    m.SetProperty(prop.Name, textureProp.Value);
                    break;

                case Property<GraphicsBuffer> bufferProp:
                    m.SetProperty(prop.Name, bufferProp.Value);
                    break;
            }
        }

        return m;
    }

    private void set<T>(string name, T value)
        where T : class
    {
        if (properties.TryGetValue(name, out var prop))
        {
            if (prop is not Property<T> typedProp)
            {
                throw new InvalidCastException($"Property value cannot be casted to {typeof(T)}.");
            }

            typedProp.Value = value;
        }
        else
        {
            properties[name] = new Property<T>(name, value);
        }
    }

    private interface IProperty
    {
        string Name { get; }
    }

    private struct Property<T> : IProperty
        where T : class
    {
        public string Name { get; }
        public T Value { get; set; }

        public Property(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }
}
