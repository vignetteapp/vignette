// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using Sekai.Graphics;

namespace Vignette.Graphics;

/// <summary>
/// A material that is unlit.
/// </summary>
public sealed class UnlitMaterial : IMaterial
{
    /// <summary>
    /// The default unlit material.
    /// </summary>
    public static readonly IMaterial Default = new UnlitMaterial(true);

    /// <summary>
    /// The material's texture.
    /// </summary>
    public Texture? Texture
    {
        get => ((TextureProperty)properties[0]).Texture;
        set
        {
            if (isDefault)
            {
                throw new InvalidOperationException("Cannot modify the default instance.");
            }

            var texture = (TextureProperty)properties[0];
            texture.Texture = value;
        }
    }

    /// <summary>
    /// The material's sampler.
    /// </summary>
    public Sampler? Sampler
    {
        get => ((TextureProperty)properties[0]).Sampler;
        set
        {
            if (isDefault)
            {
                throw new InvalidOperationException("Cannot modify the default instance.");
            }

            var texture = (TextureProperty)properties[0];
            texture.Sampler = value;
        }
    }

    /// <summary>
    /// The material's primitive type.
    /// </summary>
    public PrimitiveType Primitives { get; set; } = PrimitiveType.TriangleList;

    private readonly bool isDefault;
    private readonly Effect effect;
    private readonly IProperty[] properties;
    private readonly InputLayoutDescription layout;
    private readonly RasterizerStateDescription rasterizer;
    private readonly DepthStencilStateDescription depthStencil;

    public UnlitMaterial()
        : this(false)
    {
    }

    private UnlitMaterial(bool isDefault)
    {
        effect = Effect.From(shader, out layout, out properties);

        rasterizer = new RasterizerStateDescription
        (
            FaceCulling.None,
            FaceWinding.CounterClockwise,
            FillMode.Solid,
            false
        );

        depthStencil = new DepthStencilStateDescription
        (
            false,
            false,
            ComparisonKind.Always
        );

        this.isDefault = isDefault;
    }

    int IMaterial.Stencil => 0;
    Effect IMaterial.Effect => effect;
    InputLayoutDescription IMaterial.Layout => layout;
    BlendStateDescription IMaterial.Blend => BlendStateDescription.NonPremultiplied;
    RasterizerStateDescription IMaterial.Rasterizer => rasterizer;
    DepthStencilStateDescription IMaterial.DepthStencil => depthStencil;
    IEnumerable<IProperty> IMaterial.Properties => properties;

    private const string shader =
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
