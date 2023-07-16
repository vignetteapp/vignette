// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Sekai.Graphics;
using Sekai.Mathematics;

namespace Vignette.Graphics;

public sealed class Renderer
{
    /// <summary>
    /// A white pixel texture.
    /// </summary>
    public Texture WhitePixel { get; }

    /// <summary>
    /// A point sampler.
    /// </summary>
    public Sampler SamplerPoint { get; }

    /// <summary>
    /// A linear sampler.
    /// </summary>
    public Sampler SamplerLinear { get; }

    /// <summary>
    /// A 4x anisotropic sampler.
    /// </summary>
    public Sampler SamplerAniso4x { get; }

    private readonly GraphicsBuffer ubo;
    private readonly GraphicsDevice device;
    private readonly Dictionary<Type, IDictionary> caches = new();

    internal Renderer(GraphicsDevice device)
    {
        ubo = device.CreateBuffer<Matrix4x4>(BufferType.Uniform, 3, true);

        Span<byte> whitePixel = stackalloc byte[] { 255, 255, 255, 255 };

        WhitePixel = device.CreateTexture(new(1, 1, PixelFormat.R8G8B8A8_UNorm, 1, 1, TextureUsage.Resource));
        WhitePixel.SetData((ReadOnlySpan<byte>)whitePixel, 0, 0, 0, 0, 0, 1, 1, 0);

        SamplerPoint = device.CreateSampler(new(TextureFilter.MinMagMipPoint, TextureAddress.Repeat, TextureAddress.Repeat, TextureAddress.Repeat, 0, Color.White, 0, 0, 0));
        SamplerLinear = device.CreateSampler(new(TextureFilter.MinMagMipLinear, TextureAddress.Repeat, TextureAddress.Repeat, TextureAddress.Repeat, 0, Color.White, 0, 0, 0));
        SamplerAniso4x = device.CreateSampler(new(TextureFilter.Anisotropic, TextureAddress.Repeat, TextureAddress.Repeat, TextureAddress.Repeat, 4, Color.White, 0, 0, 0));

        this.device = device;
    }

    /// <summary>
    /// Draws a single <paramref name="renderable"/> to <paramref name="target"/>.
    /// </summary>
    /// <param name="renderable">The renderable to draw.</param>
    /// <param name="target">The target to draw to. A value of <see langword="null"/> to draw to the backbuffer.</param>
    public void Draw(RenderData renderable, RenderTarget? target = null)
    {
        Draw(new[] { renderable }, target);
    }

    /// <summary>
    /// Draws <paramref name="renderables"/> to <paramref name="target"/>.
    /// </summary>
    /// <param name="renderables">The renderables to draw.</param>
    /// <param name="target">The target to draw to. A value of <see langword="null"/> to draw to the backbuffer.</param>
    public void Draw(IEnumerable<RenderData> renderables, RenderTarget? target = null)
    {
        var currentLayout = default(InputLayout);
        int currentMaterialID = -1;

        foreach (var data in renderables)
        {
            using (var mvp = ubo.Map<Matrix4x4>(MapMode.Write))
            {
                mvp[0] = data.Projector.ProjMatrix;
                mvp[1] = data.Projector.ViewMatrix;
                mvp[2] = data.World.WorldMatrix;
            }

            device.SetUniformBuffer(ubo, Effect.GLOBAL_TRANSFORM_ID);

            if (target is not null)
            {
                device.SetFramebuffer((Framebuffer)target);
            }
            else
            {
                device.SetFramebuffer(null);
            }

            draw(data.Renderable, ref currentMaterialID, ref currentLayout!);
        }
    }

    private void draw(RenderObject renderObject, ref int currentMaterialID, ref InputLayout currentLayout)
    {
        if (renderObject.IndexCount <= 0 || renderObject.VertexBuffer is null || renderObject.IndexBuffer is null)
        {
            return;
        }

        int materialID = renderObject.Material.GetMaterialID();

        if (currentMaterialID != materialID)
        {
            apply(renderObject.Material, ref currentLayout);
            currentMaterialID = materialID;
        }

        foreach (var property in renderObject.Material.Properties)
        {
            if (property is UniformProperty uniform)
            {
                if (uniform.Uniform is not null)
                {
                    device.SetUniformBuffer(uniform.Uniform, (uint)uniform.Slot);
                }
            }

            if (property is TextureProperty texture)
            {
                device.SetTexture(texture.Texture ?? WhitePixel, texture.Sampler ?? SamplerPoint, (uint)texture.Slot);
            }
        }

        device.SetVertexBuffer(renderObject.VertexBuffer, currentLayout);
        device.SetIndexBuffer(renderObject.IndexBuffer, renderObject.IndexType);
        device.DrawIndexed(renderObject.Material.Primitives, (uint)renderObject.IndexCount);
    }

    private void apply(IMaterial material, ref InputLayout layout)
    {
        var blendCache = getCache<BlendStateDescription, BlendState>();

        if (!blendCache.TryGetValue(material.Blend, out var blendState))
        {
            blendState = device.CreateBlendState(material.Blend);
            blendCache.Add(material.Blend, blendState);
        }

        device.SetBlendState(blendState);

        var shaderCache = getCache<Effect, Shader>();

        if (!shaderCache.TryGetValue(material.Effect, out var shader))
        {
            shader = device.CreateShader((ShaderCode[])material.Effect);
            shaderCache.Add(material.Effect, shader);
        }

        device.SetShader(shader);

        var layoutCache = getCache<InputLayoutDescription, InputLayout>();

        if (!layoutCache.TryGetValue(material.Layout, out var layoutState))
        {
            layoutState = device.CreateInputLayout(material.Layout);
            layoutCache.Add(material.Layout, layoutState);
        }

        layout = layoutState;

        var rasterizerCache = getCache<RasterizerStateDescription, RasterizerState>();

        if (!rasterizerCache.TryGetValue(material.Rasterizer, out var rasterizerState))
        {
            rasterizerState = device.CreateRasterizerState(material.Rasterizer);
            rasterizerCache.Add(material.Rasterizer, rasterizerState);
        }

        device.SetRasterizerState(rasterizerState);

        var depthStencilCache = getCache<DepthStencilStateDescription, DepthStencilState>();

        if (!depthStencilCache.TryGetValue(material.DepthStencil, out var depthStencilState))
        {
            depthStencilState = device.CreateDepthStencilState(material.DepthStencil);
            depthStencilCache.Add(material.DepthStencil, depthStencilState);
        }

        device.SetDepthStencilState(depthStencilState, material.Stencil);
    }

    private IDictionary<T, U> getCache<T, U>()
        where T : struct, IEquatable<T>
        where U : notnull
    {
        if (!caches.TryGetValue(typeof(T), out var cache))
        {
            cache = new Dictionary<T, U>();
            caches.Add(typeof(T), cache);
        }

        return (IDictionary<T, U>)cache;
    }
}
