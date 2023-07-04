// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Sekai.Graphics;
using NativeTexture = Sekai.Graphics.Texture;

namespace Vignette.Rendering;

/// <summary>
/// An output target.
/// </summary>
public sealed class RenderTarget : IDisposable
{
    /// <summary>
    /// The render target's width.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// The render target's height.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// The color texture.
    /// </summary>
    public Texture Color { get; }

    /// <summary>
    /// The depth texture.
    /// </summary>
    public Texture? Depth { get; }

    private bool isDisposed;
    private readonly Framebuffer framebuffer;

    private RenderTarget(int width, int height, Framebuffer framebuffer, NativeTexture color, NativeTexture? depth)
    {
        Depth = depth is not null ? new Texture(depth) : null;
        Color = new Texture(color);
        Width = width;
        Height = height;
        this.framebuffer = framebuffer;
    }

    public static RenderTarget Create(GraphicsDevice device, int width, int height, int layers = 1, PixelFormat colorFormat = PixelFormat.R8G8B8A8_UNorm, PixelFormat? depthFormat = null)
    {
        if (layers <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(layers), "Layer count must be greater than zero.");
        }

        if (colorFormat.IsDepthStencil())
        {
            throw new ArgumentException("Invalid color format.", nameof(colorFormat));
        }

        if (depthFormat.HasValue && !depthFormat.Value.IsDepthStencil())
        {
            throw new ArgumentException("Invalid depth format.", nameof(depthFormat));
        }

        var colorDescriptor = new TextureDescription
        (
            TextureType.Texture2D,
            width,
            height,
            1,
            colorFormat,
            1,
            layers,
            TextureUsage.RenderTarget | TextureUsage.Resource
        );

        var color = device.CreateTexture(colorDescriptor);

        var colorAttachments = new FramebufferAttachment[layers];

        for (int i = 0; i < layers; i++)
        {
            colorAttachments[i] = new FramebufferAttachment(color, i, 0);
        }

        if (!depthFormat.HasValue)
        {
            return new RenderTarget(width, height, device.CreateFramebuffer(null, colorAttachments), color, null);
        }

        var depthDescriptor = new TextureDescription
        (
            TextureType.Texture2D,
            width,
            height,
            1,
            depthFormat.Value,
            1,
            1,
            TextureUsage.RenderTarget | TextureUsage.Resource
        );

        var depth = device.CreateTexture(depthDescriptor);
        var depthAttachment = new FramebufferAttachment(depth, 0, 0);

        return new RenderTarget(width, height, device.CreateFramebuffer(depthAttachment, colorAttachments), color, depth);
    }

    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }

        Color.Dispose();
        Depth?.Dispose();
        framebuffer.Dispose();

        isDisposed = true;
    }

    public static explicit operator Framebuffer(RenderTarget target) => target.framebuffer;
}
