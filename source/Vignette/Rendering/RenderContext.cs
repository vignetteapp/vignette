// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Sekai.Graphics;
using Sekai.Mathematics;
using Vignette.Rendering.Primitives;
using Vignette.Rendering.Vertices;

namespace Vignette.Rendering;

/// <summary>
/// A render context.
/// </summary>
public abstract class RenderContext : IDisposable
{
    private RenderContextState state;
    private bool isDisposed;
    private int currentIndexLength;
    private int currentVertexCount;
    private int currentBufferLength;
    private readonly byte[] vbo;
    private readonly short[] ibo;
    private readonly Material material;
    private readonly Renderer renderer;
    private readonly Stack<RenderContextState> states = new();

    internal RenderContext(Renderer renderer, Material material, Matrix4x4 projection)
    {
        vbo = new byte[Renderer.BUFFER_SIZE];
        ibo = new short[Renderer.BUFFER_SIZE / Unsafe.SizeOf<short>()];

        state = new RenderContextState
        {
            View = Matrix4x4.Identity,
            Scale = Matrix4x4.Identity,
            Rotation = Matrix4x4.Identity,
            Translation = Matrix4x4.Identity,
            Projection = projection,
            Scissor = Rectangle.Empty,
            Material = material,
            Blend = BlendStateDescription.NonPremultiplied,
            Rasterizer = new RasterizerStateDescription
            (
                FaceCulling.None,
                FaceWinding.CounterClockwise,
                FillMode.Solid,
                false
            ),
            Layout = new InputLayoutDescription(Array.Empty<InputLayoutMember>()),
            DepthStencil = new DepthStencilStateDescription(false, false, ComparisonKind.Always),
        };

        this.renderer = renderer;
        this.material = material;
    }

    /// <summary>
    /// Saves the current state.
    /// </summary>
    public void Save()
    {
        states.Push(state);
    }

    /// <summary>
    /// Restores the previous state.
    /// </summary>
    public void Restore()
    {
        states.TryPop(out state);
    }

    /// <summary>
    /// Sets the render target.
    /// </summary>
    /// <param name="target">The render target to make active.</param>
    /// <remarks>
    /// Setting <paramref name="target"/> to <see langword="null"/> will use the default render target.
    /// </remarks>
    public void SetRenderTarget(RenderTarget? target = null)
    {
        if (ReferenceEquals(state.Target, target))
        {
            return;
        }

        Flush();

        state.Target = target;
    }

    /// <summary>
    /// Sets the material.
    /// </summary>
    /// <param name="material">The material to set.</param>
    /// <remarks>
    /// Setting <paramref name="material"/> to <see langword="null"/> will use the default material.
    /// </remarks>
    public void SetMaterial(Material? material = null)
    {
        material ??= this.material;

        if (ReferenceEquals(state.Material, material))
        {
            return;
        }

        Flush();

        state.Material = material;
    }

    /// <summary>
    /// Sets the scissor rectangle.
    /// </summary>
    /// <param name="scissor">The scissor rectangle to set.</param>
    /// <remarks>
    /// Setting <see cref="Rectangle.Empty"/> will disable the scissor.
    /// </remarks>
    public void SetScissor(Rectangle scissor)
    {
        if (state.Scissor.Equals(scissor) && state.Rasterizer.Scissor == !scissor.IsEmpty)
        {
            return;
        }

        Flush();

        state.Scissor = scissor;
        state.Rasterizer.Scissor = !scissor.IsEmpty;
    }

    /// <summary>
    /// Sets the face culling.
    /// </summary>
    /// <param name="culling">The faces culled.</param>
    public void SetFaceCulling(FaceCulling culling)
    {
        if (state.Rasterizer.Culling == culling)
        {
            return;
        }

        Flush();

        state.Rasterizer.Culling = culling;
    }

    /// <summary>
    /// Sets the winding order of the vertices.
    /// </summary>
    /// <param name="winding">The winding order.</param>
    public void SetFaceWinding(FaceWinding winding)
    {
        if (state.Rasterizer.Winding == winding)
        {
            return;
        }

        Flush();

        state.Rasterizer.Winding = winding;
    }

    /// <summary>
    /// Sets the fill mode of primitives.
    /// </summary>
    /// <param name="mode">The fill mode.</param>
    public void SetFillMode(FillMode mode)
    {
        if (state.Rasterizer.Mode == mode)
        {
            return;
        }

        Flush();

        state.Rasterizer.Mode = mode;
    }

    /// <summary>
    /// Draws a <see cref="Mesh"/>.
    /// </summary>
    /// <param name="mesh">The mesh to draw.</param>
    public void DrawMesh(Mesh mesh)
    {
        Draw(mesh, mesh.Format.AsDescription());
    }

    /// <summary>
    /// Sets depth testing parameters.
    /// </summary>
    /// <param name="enabled">Whether depth testing is enabled or not.</param>
    /// <param name="write">Whether the depth buffer should be written to if the test passes.</param>
    /// <param name="comparison">The depth test comparison function.</param>
    public void SetDepthTest(bool enabled, bool write, ComparisonKind comparison)
    {
        if (state.DepthStencil.DepthTest == enabled && state.DepthStencil.DepthMask == write && state.DepthStencil.DepthComparison == comparison)
        {
            return;
        }

        Flush();

        state.DepthStencil.DepthTest = enabled;
        state.DepthStencil.DepthMask = write;
        state.DepthStencil.DepthComparison = comparison;
    }

    /// <summary>
    /// Sets stencil testing parameters.
    /// </summary>
    /// <param name="enabled">Whether stencil testing is enabled or not.</param>
    /// <param name="read">The stencil read mask.</param>
    /// <param name="write">The stencil write mask.</param>
    public void SetStencilTest(bool enabled, byte read, byte write)
    {
        if (state.DepthStencil.StencilTest == enabled && state.DepthStencil.StencilReadMask == read && state.DepthStencil.StencilWriteMask == write)
        {
            return;
        }

        Flush();

        state.DepthStencil.StencilTest = enabled;
        state.DepthStencil.StencilReadMask = read;
        state.DepthStencil.StencilWriteMask = write;
    }

    /// <summary>
    /// Sets custom stencil parameters.
    /// </summary>
    /// <param name="reference">The stencil reference value.</param>
    /// <param name="frontStencilPass">The operation performed for passing the front face stencil test.</param>
    /// <param name="frontStencilFail">The operation performed for failing the front face stencil test.</param>
    /// <param name="frontDepthFail">The operation performed for failing the front face depth test.</param>
    /// <param name="frontComparison">The comparison performed for the front face.</param>
    /// <param name="backStencilPass">The operation performed for passing the back face stencil test.</param>
    /// <param name="backStencilFail">The operation performed for failing the back face stencil test.</param>
    /// <param name="backDepthFail">The operation performed for failing the back face depth test.</param>
    /// <param name="backComparison">The comparison performed for the back face.</param>
    public void SetStencil(int reference, StencilOperation frontStencilPass, StencilOperation frontStencilFail, StencilOperation frontDepthFail, ComparisonKind frontComparison, StencilOperation backStencilPass, StencilOperation backStencilFail, StencilOperation backDepthFail, ComparisonKind backComparison)
    {
        if (state.Stencil == reference &&
            state.DepthStencil.Front.StencilPass == frontStencilPass && state.DepthStencil.Front.StencilFail == frontStencilFail &&
            state.DepthStencil.Front.DepthFail == frontDepthFail && state.DepthStencil.Front.Comparison == frontComparison &&
            state.DepthStencil.Back.StencilPass == backStencilPass && state.DepthStencil.Back.StencilFail == backStencilFail &&
            state.DepthStencil.Back.DepthFail == backDepthFail && state.DepthStencil.Back.Comparison == backComparison)
        {
            return;
        }

        Flush();

        state.Stencil = reference;
        state.DepthStencil.Front.StencilPass = frontStencilPass;
        state.DepthStencil.Front.StencilFail = frontStencilFail;
        state.DepthStencil.Front.DepthFail = frontDepthFail;
        state.DepthStencil.Front.Comparison = frontComparison;
        state.DepthStencil.Back.StencilPass = backStencilPass;
        state.DepthStencil.Back.StencilFail = backStencilFail;
        state.DepthStencil.Back.DepthFail = backDepthFail;
        state.DepthStencil.Back.Comparison = backComparison;
    }

    /// <summary>
    /// Sets stencil parameters for both the front and back faces.
    /// </summary>
    /// <param name="reference"></param>
    /// <param name="pass">The operation performed for passing the stencil test.</param>
    /// <param name="fail">The operation performed for failing the stencil test.</param>
    /// <param name="depthFail">The operation performed for failing the depth test.</param>
    /// <param name="comparison">The comparison performed.</param>
    public void SetStencil(int reference, StencilOperation pass, StencilOperation fail, StencilOperation depthFail, ComparisonKind comparison)
    {
        SetStencil(reference, pass, fail, depthFail, comparison, pass, fail, depthFail, comparison);
    }

    /// <summary>
    /// Sets custom blending parameters.
    /// </summary>
    /// <param name="enabled">Whether blending should be enabled or not.</param>
    /// <param name="srcColor">The source color blending.</param>
    /// <param name="srcAlpha">The source alpha blending.</param>
    /// <param name="dstColor">The destination color blending.</param>
    /// <param name="dstAlpha">The destination alpha blending.</param>
    /// <param name="colorOperation">The operation to perform between <paramref name="srcColor"/> and <paramref name="dstColor"/>.</param>
    /// <param name="alphaOperation">The operation to perform between <paramref name="srcAlpha"/> and <paramref name="dstAlpha"/>.</param>
    public void SetBlend(bool enabled, BlendType srcColor, BlendType srcAlpha, BlendType dstColor, BlendType dstAlpha, BlendOperation colorOperation, BlendOperation alphaOperation)
    {
        if (state.Blend.Enabled == enabled &&
            state.Blend.SourceColor == srcColor && state.Blend.SourceAlpha == srcAlpha &&
            state.Blend.DestinationColor == dstColor && state.Blend.DestinationAlpha == dstAlpha &&
            state.Blend.ColorOperation == colorOperation && state.Blend.AlphaOperation == alphaOperation)
        {
            return;
        }

        Flush();

        state.Blend.Enabled = enabled;
        state.Blend.SourceColor = srcColor;
        state.Blend.SourceAlpha = srcAlpha;
        state.Blend.DestinationColor = dstColor;
        state.Blend.DestinationAlpha = dstAlpha;
        state.Blend.ColorOperation = colorOperation;
        state.Blend.AlphaOperation = alphaOperation;
    }

    /// <summary>
    /// Sets individual blending parameters.
    /// </summary>
    /// <param name="srcColor">The source color blending.</param>
    /// <param name="srcAlpha">The source alpha blending.</param>
    /// <param name="dstColor">The destination color blending.</param>
    /// <param name="dstAlpha">The destination alpha blending.</param>
    public void SetBlend(BlendType srcColor, BlendType srcAlpha, BlendType dstColor, BlendType dstAlpha)
    {
        SetBlend(true, srcColor, srcAlpha, dstColor, dstAlpha, BlendOperation.Add, BlendOperation.Add);
    }

    /// <summary>
    /// Sets the source and destination blending parameters.
    /// </summary>
    /// <param name="source">The source blending.</param>
    /// <param name="destination">The destination blending.</param>
    public void SetBlend(BlendType source, BlendType destination)
    {
        SetBlend(source, source, destination, destination);
    }

    /// <summary>
    /// Sets a predefined blending operation.
    /// </summary>
    /// <param name="mode">The predefined blending mode.</param>
    public void SetBlend(BlendMode mode)
    {
        switch (mode)
        {
            case BlendMode.Disabled:
                SetBlend(false, BlendType.One, BlendType.Zero, BlendType.One, BlendType.Zero, BlendOperation.Add, BlendOperation.Add);
                break;

            case BlendMode.AlphaBlend:
                SetBlend(BlendType.One, BlendType.OneMinusSourceAlpha);
                break;

            case BlendMode.Opaque:
                SetBlend(BlendType.One, BlendType.Zero);
                break;

            case BlendMode.Additive:
                SetBlend(BlendType.SourceAlpha, BlendType.One);
                break;

            case BlendMode.NonPremultiplied:
                SetBlend(BlendType.SourceAlpha, BlendType.OneMinusSourceAlpha);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(mode));
        }
    }

    /// <summary>
    /// Sets the color write mask.
    /// </summary>
    /// <param name="mask">The color write mask to set.</param>
    public void SetColorMask(ColorWriteMask mask)
    {
        if (state.Blend.WriteMask == mask)
        {
            return;
        }

        Flush();

        state.Blend.WriteMask = mask;
    }

    /// <summary>
    /// Sets the scale matrix.
    /// </summary>
    /// <param name="scale">The scale matrix to set.</param>
    protected void Scale(Matrix4x4 scale)
    {
        if (state.Scale.Equals(scale))
        {
            return;
        }

        Flush();

        state.Scale = scale;
    }

    /// <summary>
    /// Sets the rotation matrix.
    /// </summary>
    /// <param name="rotation">The rotation matrix to set.</param>
    protected void Rotate(Matrix4x4 rotation)
    {
        if (state.Rotation.Equals(rotation))
        {
            return;
        }

        Flush();

        state.Rotation = rotation;
    }

    /// <summary>
    /// Sets the translation matrix.
    /// </summary>
    /// <param name="translation">The translation matrix to set.</param>
    protected void Translate(Matrix4x4 translation)
    {
        if (state.Translation.Equals(translation))
        {
            return;
        }

        Flush();

        state.Translation = translation;
    }

    /// <summary>
    /// Sets the view matrix.
    /// </summary>
    /// <param name="view">The view matrixt to set.</param>
    protected void LookAt(Matrix4x4 view)
    {
        if (state.View.Equals(view))
        {
            return;
        }

        Flush();

        state.View = view;
    }

    /// <summary>
    /// Draws a <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The primitive type.</typeparam>
    /// <typeparam name="U">The primitive vertex type.</typeparam>
    /// <param name="primitive">The primitive to be drawn.</param>
    protected void Draw<T, U>(in T primitive)
        where T : unmanaged, IPrimitive<U>
        where U : unmanaged, IVertex, IEquatable<U>
    {
        Draw(primitive, U.Layout);
    }

    /// <summary>
    /// Draws a primitive.
    /// </summary>
    /// <param name="primitive">The primitive to be drawn.</param>
    protected void Draw(in IPrimitive primitive, InputLayoutDescription layout)
    {
        if (state.Layout != layout || state.Primitive != primitive.Mode)
        {
            Flush();

            state.Layout = layout;
            state.Primitive = primitive.Mode;
        }

        var indexs = primitive.GetIndices();
        var buffer = primitive.GetVertices();

        if (currentBufferLength + buffer.Length >= vbo.Length || currentIndexLength + indexs.Length >= ibo.Length)
        {
            Flush();
        }

        for (int i = 0; i < indexs.Length; i++)
        {
            ibo[i + currentIndexLength] = (short)(currentVertexCount + indexs[i]);
        }

        currentIndexLength += indexs.Length;
        currentVertexCount += primitive.VertexCount;

        for (int i = 0; i < buffer.Length; i++)
        {
            vbo[i + currentBufferLength] = buffer[i];
        }

        currentBufferLength += buffer.Length;
    }

    /// <summary>
    /// Flushes and submits the buffered vertices and indices.
    /// </summary>
    protected void Flush()
    {
        if (currentBufferLength == 0 || currentVertexCount == 0 || currentIndexLength == 0)
        {
            return;
        }

        renderer.Submit(state, vbo.AsSpan()[..currentBufferLength], ibo.AsSpan()[..currentIndexLength]);

        currentIndexLength = 0;
        currentVertexCount = 0;
        currentBufferLength = 0;
    }

    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }

        states.Clear();
        Flush();

        isDisposed = true;

        GC.SuppressFinalize(this);
    }
}
