// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using Sekai.Graphics;

namespace Vignette.Graphics;

/// <summary>
/// Defines a <see cref="ShaderMaterial"/> property.
/// </summary>
public interface IProperty
{
    /// <summary>
    /// The property name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The property slot.
    /// </summary>
    int Slot { get; }
}

/// <summary>
/// Defines a <see cref="ShaderMaterial"/> property that contains a <see cref="GraphicsBuffer"/> as its value.
/// </summary>
/// <param name="Name">The property name.</param>
/// <param name="Slot">The property slot.</param>
/// <param name="Buffer">The buffer.</param>
public record struct UniformProperty(string Name, int Slot, GraphicsBuffer? Uniform = null) : IProperty;

/// <summary>
/// Defines a <see cref="ShaderMaterial"/> property that contains a <see cref="Sekai.Graphics.Texture"/> and <see cref="Sekai.Graphics.Sampler"/> pair as its value.
/// </summary>
/// <param name="Name">The property name.</param>
/// <param name="Slot">The property slot.</param>
/// <param name="Texture">The texture.</param>
/// <param name="Sampler">The sampler.</param>
public record struct TextureProperty(string Name, int Slot, Texture? Texture = null, Sampler? Sampler = null) : IProperty;
