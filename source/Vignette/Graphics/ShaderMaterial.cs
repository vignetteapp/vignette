// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using Sekai.Graphics;

namespace Vignette.Graphics;

/// <summary>
/// A composable material that created from shader code.
/// </summary>
public sealed class ShaderMaterial : IMaterial, ICloneable
{
    private int stencil;
    private PrimitiveType primitives;
    private BlendStateDescription blend;
    private RasterizerStateDescription rasterizer;
    private DepthStencilStateDescription depthStencil;
    private readonly Effect effect;
    private readonly InputLayoutDescription layout;
    private readonly IProperty[] properties;

    private ShaderMaterial(InputLayoutDescription layout, Effect effect, IProperty[] properties)
    {
        this.layout = layout;
        this.effect = effect;
        this.properties = properties;
    }

    private ShaderMaterial(PrimitiveType primitives, BlendStateDescription blend, RasterizerStateDescription rasterizer, DepthStencilStateDescription depthStencil, InputLayoutDescription layout, Effect effect, IProperty[] properties)
    {
        this.blend = blend;
        this.layout = layout;
        this.effect = effect;
        this.properties = properties;
        this.primitives = primitives;
        this.rasterizer = rasterizer;
        this.depthStencil = depthStencil;
    }

    /// <summary>
    /// Sets the primitive type for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="primitives">The primitive type.</param>
    public ShaderMaterial SetPrimitives(PrimitiveType primitives)
    {
        this.primitives = primitives;
        return this;
    }

    /// <summary>
    /// Sets the face culling mode for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="culling">The face culling mode.</param>
    public ShaderMaterial SetFaceCulling(FaceCulling culling)
    {
        rasterizer.Culling = culling;
        return this;
    }

    /// <summary>
    /// Sets the face winding mode for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="winding">The face winding mode.</param>
    public ShaderMaterial SetFaceWinding(FaceWinding winding)
    {
        rasterizer.Winding = winding;
        return this;
    }

    /// <summary>
    /// Sets the polygon fill mode for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="mode">The polygon fill mode.</param>
    public ShaderMaterial SetFillMode(FillMode mode)
    {
        rasterizer.Mode = mode;
        return this;
    }

    /// <summary>
    /// Sets custom stencil parameters for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="reference">The stencil reference.</param>
    /// <param name="frontStencilPass">The operation performed for the passing front face stencil test.</param>
    /// <param name="frontStencilFail">The operation performed for the failing front face stencil test.</param>
    /// <param name="frontDepthFail">The operation performed for the failing front face depth test.</param>
    /// <param name="frontComparison">The comparison performed for the front face.</param>
    /// <param name="backStencilPass">The operation performed for the passing back face stencil test.</param>
    /// <param name="backStencilFail">The operation performed for the failing back face stencil test.</param>
    /// <param name="backDepthFail">The operation performed for the failing back face depth test.</param>
    /// <param name="backComparison">The comparison performed for the back face.</param>
    public ShaderMaterial SetStencil(int reference, StencilOperation frontStencilPass, StencilOperation frontStencilFail, StencilOperation frontDepthFail, ComparisonKind frontComparison, StencilOperation backStencilPass, StencilOperation backStencilFail, StencilOperation backDepthFail, ComparisonKind backComparison)
    {
        stencil = reference;
        depthStencil.Front.StencilPass = frontStencilPass;
        depthStencil.Front.StencilFail = frontStencilFail;
        depthStencil.Front.DepthFail = frontDepthFail;
        depthStencil.Front.Comparison = frontComparison;
        depthStencil.Back.StencilPass = backStencilPass;
        depthStencil.Back.StencilFail = backStencilFail;
        depthStencil.Back.DepthFail = backDepthFail;
        depthStencil.Back.Comparison = backComparison;
        return this;
    }

    /// <summary>
    /// Sets custom stencil parameters for both the front and back faces for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="reference">The stencil reference.</param>
    /// <param name="pass">The operation performed for passing the stencil test.</param>
    /// <param name="fail">The operation performed for failing the stencil test.</param>
    /// <param name="depthFail">The operation performed for failing the depth test.</param>
    /// <param name="comparison">The comparison performed.</param>
    public ShaderMaterial SetStencil(int reference, StencilOperation pass, StencilOperation fail, StencilOperation depthFail, ComparisonKind comparison)
    {
        return SetStencil(reference, pass, fail, depthFail, comparison, pass, fail, depthFail, comparison);
    }

    /// <summary>
    /// Sets custom blending parameters for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="enabled">Whether to enable or disable blending.</param>
    /// <param name="srcColor">The source color blending.</param>
    /// <param name="srcAlpha">The source alpha blending.</param>
    /// <param name="dstColor">The destination color blending.</param>
    /// <param name="dstAlpha">The destination alpha blending.</param>
    /// <param name="colorOperation">The operation performed between <paramref name="srcColor"/> and <paramref name="dstColor"/>.</param>
    /// <param name="alphaOperation">The operation performed between <paramref name="srcAlpha"/> and <paramref name="dstAlpha"/>.</param>
    public ShaderMaterial SetBlend(bool enabled, BlendType srcColor, BlendType srcAlpha, BlendType dstColor, BlendType dstAlpha, BlendOperation colorOperation, BlendOperation alphaOperation)
    {
        blend.Enabled = enabled;
        blend.SourceColor = srcColor;
        blend.SourceAlpha = srcAlpha;
        blend.DestinationColor = dstColor;
        blend.DestinationAlpha = dstAlpha;
        blend.ColorOperation = colorOperation;
        blend.AlphaOperation = alphaOperation;
        return this;
    }

    /// <summary>
    /// Sets individual blending parameters for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="srcColor">The source color blending.</param>
    /// <param name="srcAlpha">The source alpha blending.</param>
    /// <param name="dstColor">The destination color blending.</param>
    /// <param name="dstAlpha">The destination alpha blending.</param>
    public ShaderMaterial SetBlend(BlendType srcColor, BlendType srcAlpha, BlendType dstColor, BlendType dstAlpha)
    {
        return SetBlend(true, srcColor, srcAlpha, dstColor, dstAlpha, BlendOperation.Add, BlendOperation.Add);
    }

    /// <summary>
    /// Sets blending parameters for the source and destination colors for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="source">The source blending.</param>
    /// <param name="destination">The destination blending.</param>
    public ShaderMaterial SetBlend(BlendType source, BlendType destination)
    {
        return SetBlend(source, source, destination, destination);
    }

    /// <summary>
    /// Sets the color write mask.
    /// </summary>
    /// <param name="mask">The color write mask.</param>
    public ShaderMaterial SetColorMask(ColorWriteMask mask)
    {
        blend.WriteMask = mask;
        return this;
    }

    /// <summary>
    /// Sets a <see cref="Texture"/> for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="texture">The <see cref="Texture"/> to set. Setting <see langword="null"/> will use the default texture.</param>
    /// <param name="sampler">The <see cref="Sampler"/> to set. Setting <see langword="null"/> will use the default sampler.</param>
    /// <exception cref="ArgumentException">Thrown when the <see cref="Texture"/> is not usable as a resource.</exception>
    public ShaderMaterial SetProperty(string name, Texture? texture = null, Sampler? sampler = null)
    {
        var prop = getProperty<TextureProperty>(name);

        if (texture is not null && (texture.Usage & TextureUsage.Resource) == 0)
        {
            throw new ArgumentException($"The texture must have the {nameof(TextureUsage.Resource)} flag to be used on materials.", nameof(texture));
        }

        prop.Texture = texture;
        prop.Sampler = sampler;

        return this;
    }

    /// <summary>
    /// Sets a <see cref="GraphicsBuffer"/> for this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="buffer">The <see cref="GraphicsBuffer"/> to set. Setting <see langword="null"/> will not bind this property during rendering.</param>
    /// <exception cref="ArgumentException">Thrown when the <see cref="GraphicsBuffer"/> is not usable as a uniform.</exception>
    public ShaderMaterial SetProperty(string name, GraphicsBuffer? buffer = null)
    {
        var prop = getProperty<UniformProperty>(name);

        if (buffer is not null && buffer.Type is not BufferType.Uniform)
        {
            throw new ArgumentException($"The buffer must be a {nameof(BufferType.Uniform)} to be used on materials.", nameof(buffer));
        }

        prop.Uniform = buffer;

        return this;
    }

    /// <summary>
    /// Gets whether a property exists.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns><see langword="true"/> if the property exists or <see langword="false"/> if it does not.</returns>
    public bool HasProperty(string name)
    {
        foreach (var prop in properties)
        {
            if (prop.Name == name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Creates a shallow copy of this <see cref="ShaderMaterial"/>.
    /// </summary>
    /// <returns>A new <see cref="ShaderMaterial"/>.</returns>
    public ShaderMaterial Clone() => new
    (
        primitives,
        blend,
        rasterizer,
        depthStencil,
        layout,
        effect,
        properties.ToArray()
    );

    private T getProperty<T>(string name)
        where T : IProperty
    {
        var prop = default(IProperty);

        foreach (var p in properties)
        {
            if (p.Name == name)
            {
                prop = p;
                break;
            }
        }

        if (prop is null)
        {
            throw new KeyNotFoundException($"There is no property named \"{name}\" on this material.");
        }

        if (prop is not T typedProp)
        {
            throw new InvalidCastException($"Property \"{name}\" is not compatible with the type {typeof(T)}.");
        }

        return typedProp;
    }

    int IMaterial.Stencil => stencil;
    Effect IMaterial.Effect => effect;
    PrimitiveType IMaterial.Primitives => primitives;
    InputLayoutDescription IMaterial.Layout => layout;
    BlendStateDescription IMaterial.Blend => blend;
    RasterizerStateDescription IMaterial.Rasterizer => rasterizer;
    DepthStencilStateDescription IMaterial.DepthStencil => depthStencil;
    IEnumerable<IProperty> IMaterial.Properties => properties;
    object ICloneable.Clone() => Clone();

    /// <summary>
    /// Creates a new <see cref="ShaderMaterial"/> from HLSL shader code.
    /// </summary>
    /// <param name="code">The shader code to use.</param>
    /// <returns>A new <see cref="ShaderMaterial"/>.</returns>
    public static ShaderMaterial Create(string code)
    {
        var effect = Effect.From(code, out var layout, out var properties);
        return new(layout, effect, properties);
    }
}
