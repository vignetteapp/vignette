// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System.Numerics;
using Sekai.Mathematics;
using Vignette.Rendering.Primitives;
using Vignette.Rendering.Vertices;

namespace Vignette.Rendering;

/// <summary>
/// A 2D render context.
/// </summary>
public sealed class RenderContext2D : RenderContext
{
    private Color4 color = Color4.White;

    internal RenderContext2D(Renderer renderer, Material material, Matrix4x4 projection)
        : base(renderer, material, projection)
    {
    }

    /// <summary>
    /// Sets the color of the drawn psrimitives.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <remarks>
    /// This is applicable to any primitive except the <see cref="Mesh"/>.
    /// </remarks>
    public void SetColor(Color color)
    {
        this.color = color;
    }

    /// <summary>
    /// Translates the coordinate system.
    /// </summary>
    /// <param name="translation">The amount to translate in each axis.</param>
    public void Translate(Vector2 translation)
    {
        Translate(translation.X, translation.Y);
    }

    /// <summary>
    /// Translates the coordinate system.
    /// </summary>
    /// <param name="x">The amount to translate in the X axis.</param>
    /// <param name="y">The amount to translate in the Y axis.</param>
    public void Translate(float x, float y)
    {
        Translate(Matrix4x4.CreateTranslation(new Vector3(x, y, 0)));
    }

    /// <summary>
    /// Rotates the coordinate system.
    /// </summary>
    /// <param name="radians">The amount to rotate in radians.</param>
    public void Rotate(float radians)
    {
        Rotate(Matrix4x4.CreateRotationZ(radians));
    }

    /// <summary>
    /// Scales the coordinate system.
    /// </summary>
    /// <param name="scale">The amount to scale in each axis.</param>
    public void Scale(Vector2 scale)
    {
        Scale(scale.X, scale.Y);
    }

    /// <summary>
    /// Scales the coodinate system.
    /// </summary>
    /// <param name="scale">The amount to scale in all axes.</param>
    public void Scale(float scale)
    {
        Scale(scale, scale);
    }

    /// <summary>
    /// Scales the coordinate system.
    /// </summary>
    /// <param name="x">The amount to scale in the X axis.</param>
    /// <param name="y">The amount to scale in the Y axis.</param>
    public void Scale(float x, float y)
    {
        Scale(Matrix4x4.CreateScale(x, y, 1));
    }

    /// <summary>
    /// Draws a quadrilateral.
    /// </summary>
    /// <param name="rectangle">The rectangle to draw.</param>
    public void DrawQuad(RectangleF rectangle)
    {
        var quad = new Quad<TexturedVertex>
        (
            new TexturedVertex
            {
                Position = new Vector3(rectangle.TopLeft, 0),
                TexCoord = Vector2.Zero,
                Color = color,
            },
            new TexturedVertex
            {
                Position = new Vector3(rectangle.BottomLeft, 0),
                TexCoord = Vector2.UnitX,
                Color = color,
            },
            new TexturedVertex
            {
                Position = new Vector3(rectangle.BottomRight, 0),
                TexCoord = Vector2.One,
                Color = color,
            },
            new TexturedVertex
            {
                Position = new Vector3(rectangle.TopRight, 0),
                TexCoord = Vector2.UnitY,
                Color = color,
            }
        );

        Draw<Quad<TexturedVertex>, TexturedVertex>(quad);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="position">The rectangle position.</param>
    /// <param name="size">The rectangle size.</param>
    public void DrawQuad(Vector2 position, SizeF size)
    {
        DrawQuad(new RectangleF(position, size));
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="x">The rectangle's horizontal positon.</param>
    /// <param name="y">The rectangle's vertical position.</param>
    /// <param name="width">The rectangle's width.</param>
    /// <param name="height">The rectangle's height.</param>
    public void DrawQuad(float x, float y, float width, float height)
    {
        DrawQuad(new RectangleF(x, y, width, height));
    }

    /// <summary>
    /// Draws a line.
    /// </summary>
    /// <param name="start">The start position.</param>
    /// <param name="end">The end position.</param>
    public void DrawLine(Vector2 start, Vector2 end)
    {
        var line = new Line<TexturedVertex>
        (
            new TexturedVertex
            {
                Position = new Vector3(start, 0),
                TexCoord = Vector2.Zero,
                Color = color,
            },
            new TexturedVertex
            {
                Position = new Vector3(end, 0),
                TexCoord = Vector2.Zero,
                Color = color,
            }
        );

        Draw<Line<TexturedVertex>, TexturedVertex>(line);
    }

    /// <summary>
    /// Draws a line.
    /// </summary>
    /// <param name="x1">The starting x position.</param>
    /// <param name="y1">The starting y position.</param>
    /// <param name="x2">The ending x position.</param>
    /// <param name="y2">The ending y position.</param>
    public void DrawLine(float x1, float y1, float x2, float y2)
    {
        DrawLine(new Vector2(x1, y1), new Vector2(x2, y2));
    }
}
