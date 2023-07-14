// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Sekai.Graphics;

namespace Vignette.Graphics;

/// <summary>
/// An output target for rendering.
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

    private bool isDisposed;
    private readonly Texture? color;
    private readonly Texture? depth;
    private readonly Framebuffer framebuffer;

    private RenderTarget(int width, int height, Texture? color, Texture? depth, Framebuffer framebuffer)
    {
        this.color = color;
        this.depth = depth;
        this.Width = width;
        this.Height = height;
        this.framebuffer = framebuffer;
    }

    /// <summary>
    /// Creates a new <see cref="RenderTarget"/>.
    /// </summary>
    /// <param name="device">The graphics device used in creating the render target.</param>
    /// <param name="width">The width of the render target.</param>
    /// <param name="height">The height of the render target.</param>
    /// <param name="layers">The layer count of the render target.</param>
    /// <param name="colorFormat">The color texture pixel format.</param>
    /// <param name="depthFormat">The depth texture pixel format.</param>
    /// <returns>A new <see cref="RenderTarget"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="layers"/> is less than or equal to zero.</exception>
    /// <exception cref="InvalidOperationException">Thrown when both <paramref name="colorFormat"/> and <paramref name="depthFormat"/> are <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when either <paramref name="colorFormat"/> or <paramref name="depthFormat"/> are passed with an invalid format.</exception>
    public static RenderTarget Create(GraphicsDevice device, int width, int height, int layers = 1, PixelFormat? colorFormat = null, PixelFormat? depthFormat = null)
    {
        if (layers <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(layers), "Layer count must be greater than zero.");
        }

        if (!depthFormat.HasValue && !colorFormat.HasValue)
        {
            throw new InvalidOperationException("There must be a defined format for either or both the color and depth textures.");
        }

        if (colorFormat.HasValue && colorFormat.Value.IsDepthStencil())
        {
            throw new ArgumentException("Invalid color format.", nameof(colorFormat));
        }

        if (depthFormat.HasValue && !depthFormat.Value.IsDepthStencil())
        {
            throw new ArgumentException("Invalid depth format.", nameof(depthFormat));
        }

        var color = default(Texture);
        var depth = default(Texture);

        FramebufferAttachment? depthAttachment = null;
        FramebufferAttachment[] colorAttachments;

        if (colorFormat.HasValue)
        {
            color = device.CreateTexture(new TextureDescription
            (
                TextureType.Texture2D,
                width,
                height,
                1,
                colorFormat.Value,
                1,
                layers,
                TextureUsage.RenderTarget | TextureUsage.Resource
            ));

            colorAttachments = new FramebufferAttachment[layers];

            for (int i = 0; i < layers; i++)
            {
                colorAttachments[i] = new FramebufferAttachment(color, i, 0);
            }
        }
        else
        {
            colorAttachments = Array.Empty<FramebufferAttachment>();
        }

        if (depthFormat.HasValue)
        {
            depth = device.CreateTexture(new TextureDescription
            (
                TextureType.Texture2D,
                width,
                height,
                1,
                depthFormat.Value,
                1,
                1,
                TextureUsage.RenderTarget | TextureUsage.Resource
            ));

            depthAttachment = new FramebufferAttachment(depth, 0, 0);
        }

        return new RenderTarget(width, height, color, depth, device.CreateFramebuffer(depthAttachment, colorAttachments));
    }

    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }

        color?.Dispose();
        depth?.Dispose();
        framebuffer.Dispose();

        isDisposed = true;
    }

    public static explicit operator Framebuffer(RenderTarget target) => target.framebuffer;
}
