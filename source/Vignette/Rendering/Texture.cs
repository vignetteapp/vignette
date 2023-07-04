// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Sekai.Graphics;
using Sekai.Mathematics;
using NativeTexture = Sekai.Graphics.Texture;

namespace Vignette.Rendering;

/// <summary>
/// A graphics resource representing image or arbitrary data.
/// </summary>
public sealed class Texture : IDisposable
{
    /// <summary>
    /// The texture width.
    /// </summary>
    public int Width => native.Width;

    /// <summary>
    /// The texture height.
    /// </summary>
    public int Height => native.Height;

    /// <summary>
    /// The texture depth.
    /// </summary>
    public int Depth => native.Depth;

    /// <summary>
    /// The texture mipmap levels.
    /// </summary>
    public int Levels => native.Levels;

    /// <summary>
    /// The texture layer count.
    /// </summary>
    public int Layers => native.Layers;

    /// <summary>
    /// The texture type.
    /// </summary>
    public TextureType Type => native.Type;

    /// <summary>
    /// The texture pixel format.
    /// </summary>
    public PixelFormat Format => native.Format;

    /// <summary>
    /// The texture usage.
    /// </summary>
    public TextureUsage Usage => native.Usage;

    /// <summary>
    /// The texture sample count.
    /// </summary>
    public TextureSampleCount Count => native.Count;

    /// <summary>
    /// The texture filtering mode.
    /// </summary>
    public TextureFilter Filter = TextureFilter.MinMagMipPoint;

    /// <summary>
    /// The texture address mode for the U component.
    /// </summary>
    public TextureAddress AddressU = TextureAddress.ClampToBorder;

    /// <summary>
    /// The texture address mode for the V component.
    /// </summary>
    public TextureAddress AddressV = TextureAddress.ClampToBorder;

    /// <summary>
    /// The texture address mode for the W component.
    /// </summary>
    public TextureAddress AddressW = TextureAddress.ClampToBorder;

    /// <summary>
    /// The maximum anisotropy.
    /// </summary>
    public int MaxAnisotropy;

    /// <summary>
    /// The texture border color when <see cref="TextureAddress.ClampToBorder"/> is used.
    /// </summary>
    public Color BorderColor = Color.White;

    /// <summary>
    /// The minimum level of detail.
    /// </summary>
    public float MinimumLOD;

    /// <summary>
    /// The maximum level of detail.
    /// </summary>
    public float MaximumLOD;

    /// <summary>
    /// The bias to level of detail.
    /// </summary>
    public float LODBias;

    private bool isDisposed;
    private readonly NativeTexture native;

    internal Texture(NativeTexture native)
    {
        this.native = native;
    }

    public void SetData<T>(ReadOnlySpan<T> data, int level, int layer, int x, int y, int z, int width, int height, int depth)
        where T : unmanaged
    {
        native.SetData(data, level, layer, x, y, z, width, height, depth);
    }

    public void GetData<T>(Span<T> data, int level, int layer, int x, int y, int z, int width, int height, int depth)
        where T : unmanaged
    {
        native.GetData(data, level, layer, x, y, z, width, height, depth);
    }

    /// <summary>
    /// Create a new 1D texture.
    /// </summary>
    /// <param name="device">The graphics device.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="levels">The mipmap levels of the texture.</param>
    /// <param name="layers">The layer count of the texture.</param>
    /// <param name="format">The pixel format of the texture.</param>
    /// <param name="usage">The texture's usage.</param>
    /// <param name="count">The texture's sample count.</param>
    /// <returns>A new 1D texture.</returns>
    public static Texture Create(GraphicsDevice device, int width, int levels = 1, int layers = 1, PixelFormat format = PixelFormat.R8G8B8A8_UNorm, TextureUsage usage = TextureUsage.Resource, TextureSampleCount count = TextureSampleCount.Count1)
    {
        return create(device, width, 0, 0, levels, layers, TextureType.Texture1D, format, usage, count);
    }

    /// <summary>
    /// Create a new 2D texture.
    /// </summary>
    /// <param name="device">The graphics device.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="levels">The mipmap levels of the texture.</param>
    /// <param name="layers">The layer count of the texture.</param>
    /// <param name="format">The pixel format of the texture.</param>
    /// <param name="usage">The texture's usage.</param>
    /// <param name="count">The texture's sample count.</param>
    /// <returns>A new 2D texture.</returns>
    public static Texture Create(GraphicsDevice device, int width, int height, int levels = 1, int layers = 1, PixelFormat format = PixelFormat.R8G8B8A8_UNorm, TextureUsage usage = TextureUsage.Resource, TextureSampleCount count = TextureSampleCount.Count1)
    {
        return create(device, width, height, 0, levels, layers, TextureType.Texture2D, format, usage, count);
    }

    /// <summary>
    /// Create a new 3D texture.
    /// </summary>
    /// <param name="device">The graphics device.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="levels">The mipmap levels of the texture.</param>
    /// <param name="layers">The layer count of the texture.</param>
    /// <param name="format">The pixel format of the texture.</param>
    /// <param name="usage">The texture's usage.</param>
    /// <param name="count">The texture's sample count.</param>
    /// <returns>A new 3D texture.</returns>
    public static Texture Create(GraphicsDevice device, int width, int height, int depth, int levels = 1, int layers = 1, PixelFormat format = PixelFormat.R8G8B8A8_UNorm, TextureUsage usage = TextureUsage.Resource, TextureSampleCount count = TextureSampleCount.Count1)
    {
        return create(device, width, height, depth, levels, layers, TextureType.Texture3D, format, usage, count);
    }

    private static Texture create(GraphicsDevice device, int width, int height, int depth, int levels, int layers, TextureType type, PixelFormat format, TextureUsage usage, TextureSampleCount count)
    {
        return new(device.CreateTexture(new TextureDescription(type, width, height, depth, format, levels, layers, usage, count)));
    }

    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }

        native.Dispose();

        isDisposed = true;
    }

    public static explicit operator NativeTexture(Texture texture) => texture.native;
}
