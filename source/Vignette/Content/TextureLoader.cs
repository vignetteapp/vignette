// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using Sekai.Graphics;
using StbiSharp;

namespace Vignette.Content;

internal sealed class TextureLoader : IContentLoader<Texture>
{
    private readonly GraphicsDevice device;

    public TextureLoader(GraphicsDevice device)
    {
        this.device = device;
    }

    public Texture Load(ReadOnlySpan<byte> bytes)
    {
        var image = Stbi.LoadFromMemory(bytes, 4);

        var texture = device.CreateTexture(new TextureDescription
        (
            image.Width,
            image.Height,
            PixelFormat.R8G8B8A8_UNorm,
            1,
            1,
            TextureUsage.Resource
        ));

        texture.SetData(image.Data, 0, 0, 0, 0, 0, image.Width, image.Height, 0);

        return texture;
    }
}
