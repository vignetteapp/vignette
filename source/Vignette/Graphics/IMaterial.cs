// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using Sekai.Graphics;

namespace Vignette.Graphics;

/// <summary>
/// Defines properties of a surface and how it should be drawn.
/// </summary>
public interface IMaterial
{
    /// <summary>
    /// The stencil reference.
    /// </summary>
    int Stencil { get; }

    /// <summary>
    /// The material effect.
    /// </summary>
    Effect Effect { get; }

    /// <summary>
    /// The primitive type.
    /// </summary>
    PrimitiveType Primitives { get; }

    /// <summary>
    /// The layout descriptor.
    /// </summary>
    InputLayoutDescription Layout { get; }

    /// <summary>
    /// The blend descriptor.
    /// </summary>
    BlendStateDescription Blend { get; }

    /// <summary>
    /// The rasterizer descriptor.
    /// </summary>
    RasterizerStateDescription Rasterizer { get; }

    /// <summary>
    /// The depth stencil descriptor.
    /// </summary>
    DepthStencilStateDescription DepthStencil { get; }

    /// <summary>
    /// The material properties.
    /// </summary>
    IEnumerable<IProperty> Properties { get; }

    /// <summary>
    /// Gets a unique ID for this <see cref="IMaterial"/>.
    /// </summary>
    int GetMaterialID()
    {
        return HashCode.Combine(Stencil, Blend, Rasterizer, DepthStencil, Effect, Layout);
    }
}
