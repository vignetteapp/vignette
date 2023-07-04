// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Numerics;
using Sekai.Graphics;
using Sekai.Mathematics;

namespace Vignette.Rendering;

internal struct RenderContextState : IEquatable<RenderContextState>
{
    public int Stencil;
    public Material Material;
    public Matrix4x4 View;
    public Matrix4x4 Scale;
    public Matrix4x4 Rotation;
    public Matrix4x4 Translation;
    public Matrix4x4 Projection;
    public Rectangle Scissor;
    public RenderTarget? Target;
    public PrimitiveType Primitive;
    public BlendStateDescription Blend;
    public InputLayoutDescription Layout;
    public RasterizerStateDescription Rasterizer;
    public DepthStencilStateDescription DepthStencil;

    public readonly bool Equals(RenderContextState other)
    {
        return Stencil == other.Stencil &&
                ReferenceEquals(Material, other.Material) &&
                ReferenceEquals(Target, other.Target) &&
                View.Equals(other.View) &&
                Scale.Equals(other.Scale) &&
                Rotation.Equals(other.Rotation) &&
                Translation.Equals(other.Translation) &&
                Projection.Equals(other.Projection) &&
                Scissor.Equals(other.Scissor) &&
                Primitive == other.Primitive &&
                Blend.Equals(other.Blend) &&
                Layout.Equals(other.Layout) &&
                Rasterizer.Equals(other.Rasterizer) &&
                DepthStencil.Equals(other.DepthStencil);
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is RenderContextState state && Equals(state);
    }

    public override readonly int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Stencil);
        hash.Add(Material);
        hash.Add(View);
        hash.Add(Scale);
        hash.Add(Rotation);
        hash.Add(Translation);
        hash.Add(Projection);
        hash.Add(Scissor);
        hash.Add(Primitive);
        hash.Add(Blend);
        hash.Add(Layout);
        hash.Add(Rasterizer);
        hash.Add(DepthStencil);
        return hash.ToHashCode();
    }

    public static bool operator ==(RenderContextState left, RenderContextState right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(RenderContextState left, RenderContextState right)
    {
        return !(left == right);
    }
}
