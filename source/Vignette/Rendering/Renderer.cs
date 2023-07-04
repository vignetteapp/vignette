// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Sekai.Graphics;
using Sekai.Mathematics;

namespace Vignette.Rendering;

/// <summary>
/// Performs rendering operations and screen presentation.
/// </summary>
public sealed class Renderer
{
    /// <summary>
    /// A 1x1 white pixel texture.
    /// </summary>
    public Texture WhitePixel { get; }

    private readonly GraphicsBuffer ibo;
    private readonly GraphicsBuffer vbo;
    private readonly GraphicsBuffer trs;
    private readonly GraphicsDevice device;
    private readonly Material defaultMaterial;
    private readonly Dictionary<Type, IDictionary> caches = new();

    internal Renderer(GraphicsDevice device)
    {
        ibo = device.CreateBuffer(BufferType.Index, BUFFER_SIZE, true);
        vbo = device.CreateBuffer(BufferType.Vertex, BUFFER_SIZE, true);
        trs = device.CreateBuffer<Matrix4x4>(BufferType.Uniform, 3, true);

        Span<byte> whitePixelData = stackalloc byte[] { 255, 255, 255, 255 };
        WhitePixel = Texture.Create(device, 1, 1, 1, 1, PixelFormat.R8G8B8A8_UNorm, TextureUsage.Resource);
        WhitePixel.SetData((ReadOnlySpan<byte>)whitePixelData, 0, 0, 0, 0, 0, 1, 1, 0);

        defaultMaterial = Material.Create(Effect.From(sh_default));
        defaultMaterial.SetProperty("AlbedoTexture", WhitePixel);

        this.device = device;
    }

    /// <summary>
    /// Begins a 2D rendering context.
    /// </summary>
    /// <param name="rectangle">The rectangle to define the screen ortho.</param>
    /// <param name="nearPlane">The near clipping plane.</param>
    /// <param name="farPlane">The far clipping plane.</param>
    /// <returns>A 2D rendering context.</returns>
    public RenderContext2D Begin2D(RectangleF rectangle, float nearPlane = 0.0f, float farPlane = 1.0f)
    {
        return new RenderContext2D(this, defaultMaterial, Matrix4x4.CreateOrthographicOffCenter(rectangle.Left, rectangle.Right, rectangle.Bottom, rectangle.Top, nearPlane, farPlane));
    }

    /// <summary>
    /// Begins a 2D rendering context.
    /// </summary>
    /// <param name="position">The top left offset of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="nearPlane">The near clipping plane.</param>
    /// <param name="farPlane">The far clipping plane.</param>
    /// <returns>A 2D rendering context.</returns>
    public RenderContext2D Begin2D(Vector2 position, SizeF size, float nearPlane = 0.0f, float farPlane = 1.0f)
    {
        return Begin2D(new RectangleF(position, size), nearPlane, farPlane);
    }

    /// <summary>
    /// Begins a 2D rendering context.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    /// <param name="nearPlane">The near clipping plane.</param>
    /// <param name="farPlane">The far clipping plane.</param>
    /// <returns>A 2D rendering context.</returns>
    public RenderContext2D Begin2D(SizeF size, float nearPlane = 0.0f, float farPlane = 1.0f)
    {
        return Begin2D(new RectangleF(Vector2.Zero, size), nearPlane, farPlane);
    }

    /// <summary>
    /// Begins a 2D rendering context.
    /// </summary>
    /// <param name="x">The x offset of the rectangle.</param>
    /// <param name="y">The y offset of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="nearPlane">The near clipping plane.</param>
    /// <param name="farPlane">The far clipping plane.</param>
    /// <returns>A 2D rendering context.</returns>
    public RenderContext2D Begin2D(float x, float y, float width, float height, float nearPlane = 0.0f, float farPlane = 1.0f)
    {
        return Begin2D(new Vector2(x, y), new SizeF(width, height), nearPlane, farPlane);
    }

    /// <summary>
    /// Begins a 2D rendering context.
    /// </summary>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <param name="nearPlane">The near clipping plane.</param>
    /// <param name="farPlane">The far clipping plane.</param>
    /// <returns>A 2D rendering context.</returns>
    public RenderContext2D Begin2D(float width, float height, float nearPlane = 0.0f, float farPlane = 1.0f)
    {
        return Begin2D(new SizeF(width, height), nearPlane, farPlane);
    }

    /// <summary>
    /// Submits the <paramref name="state"/> and draws the <paramref name="vertices"/> and <paramref name="indices"/>.
    /// </summary>
    /// <param name="state">The context state.</param>
    /// <param name="vertices">The vertex data.</param>
    /// <param name="indices">The index data.</param>
    internal void Submit(in RenderContextState state, ReadOnlySpan<byte> vertices, ReadOnlySpan<short> indices)
    {
        device.SetScissor(state.Scissor);

        var rasterizerCache = getCache<RasterizerStateDescription, RasterizerState>();

        if (!rasterizerCache.TryGetValue(state.Rasterizer, out var rasterizer))
        {
            rasterizer = device.CreateRasterizerState(state.Rasterizer);
            rasterizerCache[state.Rasterizer] = rasterizer;
        }

        device.SetRasterizerState(rasterizer);

        var blendCache = getCache<BlendStateDescription, BlendState>();

        if (!blendCache.TryGetValue(state.Blend, out var blend))
        {
            blend = device.CreateBlendState(state.Blend);
            blendCache[state.Blend] = blend;
        }

        device.SetBlendState(blend);

        var depthStencilCache = getCache<DepthStencilStateDescription, DepthStencilState>();

        if (!depthStencilCache.TryGetValue(state.DepthStencil, out var depthStencil))
        {
            depthStencil = device.CreateDepthStencilState(state.DepthStencil);
            depthStencilCache[state.DepthStencil] = depthStencil;
        }

        device.SetDepthStencilState(depthStencil, state.Stencil);

        var samplerCache = getCache<SamplerDescription, Sampler>();

        foreach (var parameter in state.Material.Effect.GetParameters())
        {
            switch (parameter)
            {
                case Parameter<Texture> textureParam when state.Material.TryGetProperty<Texture>(textureParam.Name, out var texture):
                    {
                        var samplerState = new SamplerDescription
                        (
                            texture.Filter,
                            texture.AddressU,
                            texture.AddressV,
                            texture.AddressW,
                            texture.MaxAnisotropy,
                            texture.BorderColor,
                            texture.MinimumLOD,
                            texture.MaximumLOD,
                            texture.LODBias
                        );

                        if (!samplerCache.TryGetValue(samplerState, out var sampler))
                        {
                            sampler = device.CreateSampler(samplerState);
                            samplerCache[samplerState] = sampler;
                        }

                        device.SetTexture((Sekai.Graphics.Texture)texture, sampler, (uint)textureParam.Binding);
                        break;
                    }

                case Parameter<GraphicsBuffer> bufferParam when state.Material.TryGetProperty<GraphicsBuffer>(bufferParam.Name, out var buffer):
                    {
                        device.SetUniformBuffer(buffer, (uint)bufferParam.Binding);
                        break;
                    }
            }
        }

        var shaderCache = getCache<Effect, Shader>();

        if (!shaderCache.TryGetValue(state.Material.Effect, out var shader))
        {
            shader = device.CreateShader(state.Material.Effect);
            shaderCache[state.Material.Effect] = shader;
        }

        device.SetShader(shader);

        var layoutCache = getCache<InputLayoutDescription, InputLayout>();

        if (!layoutCache.TryGetValue(state.Layout, out var layout))
        {
            layout = device.CreateInputLayout(state.Layout);
            layoutCache[state.Layout] = layout;
        }

        if (state.Target is not null)
        {
            device.SetFramebuffer((Framebuffer)state.Target);
        }
        else
        {
            device.SetFramebuffer(null);
        }

        Span<Matrix4x4> transform = stackalloc Matrix4x4[]
        {
            state.Projection,
            state.View,
            state.Scale * state.Rotation * state.Translation
        };

        ibo.SetData(indices);
        vbo.SetData(vertices);
        trs.SetData((ReadOnlySpan<Matrix4x4>)transform);

        device.SetUniformBuffer(trs, 89);
        device.SetVertexBuffer(vbo, layout);
        device.SetIndexBuffer(ibo, IndexType.UnsignedShort);
        device.DrawIndexed(state.Primitive, (uint)indices.Length);
    }

    private Dictionary<T, U> getCache<T, U>()
        where T : struct, IEquatable<T>
        where U : class
    {
        if (!caches.TryGetValue(typeof(T), out var cache))
        {
            cache = new Dictionary<T, U>();
            caches.Add(typeof(T), cache);
        }

        return (Dictionary<T, U>)cache;
    }

    internal const int BUFFER_SIZE = 8000;

    private const string sh_default =
@"
struct VSInput
{
    float3 Position : POSITION;
    float2 TexCoord : TEXCOORD;
    float4 Color    : COLOR;
};

struct PSInput
{
    float4 Position : SV_POSITION;
    float2 TexCoord : TEXCOORD;
    float4 Color    : COLOR;
};

Texture2D       AlbedoTexture : register(t0);
SamplerState    AlbedoSampler : register(s0);

PSInput Vertex(in VSInput input)
{
    PSInput output;

    output.Color = input.Color;
    output.Position = OBJECT_TO_VIEW(float4(input.Position, 1.0));
    output.TexCoord = input.TexCoord;

    return output;
}

float4 Pixel(in PSInput input) : SV_TARGET
{
    return AlbedoTexture.Sample(AlbedoSampler, input.TexCoord) * input.Color;
}
";
}
