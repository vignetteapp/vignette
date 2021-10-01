// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Runtime.InteropServices;
using osu.Framework.Graphics;
using osu.Framework.Graphics.OpenGL;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Textures;
using osuTK;
using osuTK.Graphics.ES30;

namespace Vignette.Live2D.Graphics
{
    /// <summary>
    /// A drawable that draws a single mesh.
    /// </summary>
    public class CubismDrawable : CubismId, IDisposable
    {
        #region Drawable Properties

        /// <summary>
        /// The vertex positions of the drawable used in drawing.
        /// </summary>
        public Vector2[] Positions { get; internal set; }

        /// <summary>
        /// The coordinates of the drawable used in drawing.
        /// </summary>
        public Vector2[] TexturePositions { get; init; }

        /// <summary>
        /// The indices of the drawable used in drawing.
        /// </summary>
        public short[] Indices { get; init; }

        /// <summary>
        /// The render order of this drawable.
        /// </summary>
        public int RenderOrder { get; internal set; }

        /// <summary>
        /// The texture of this drawable.
        /// </summary>
        public Texture Texture { get; internal set; }

        /// <summary>
        /// The rectangular bounds of the drawable.
        /// </summary>
        public RectangleF VertexBounds
        {
            get
            {
                if (Positions == null || Positions.Length <= 0)
                    return RectangleF.Empty;

                float minX = 0;
                float minY = 0;
                float maxX = 0;
                float maxY = 0;

                foreach (var v in Positions)
                {
                    minX = Math.Min(minX, v.X);
                    minY = Math.Min(minY, v.Y);
                    maxX = Math.Max(maxX, v.X);
                    maxY = Math.Max(maxY, v.Y);
                }

                return new RectangleF(minX, minY, maxX - minX, maxY - minY);
            }
        }

        /// <summary>
        /// Whether this drawable is double-sided.
        /// </summary>
        public bool IsDoubleSided { get; internal set; }

        /// <summary>
        /// Whether this drawable uses an inverted mask.
        /// </summary>
        public bool IsInvertedMask { get; internal set; }

        /// <summary>
        /// The blending parameters used in drawing this drawable.
        /// </summary>
        public BlendingParameters Blending { get; internal set; }

        /// <summary>
        /// The alpha component of this drawable.
        /// </summary>
        public float Alpha { get; set; }

        /// <summary>
        /// The context used to mask this drawable.
        /// </summary>
        internal MaskingContext MaskingContext { get; set; }

        #endregion

        private int vaoID = -1;
        private int iboID = -1;

        protected void Initialise()
        {
            GL.GenBuffers(1, out iboID);
            GLWrapper.BindBuffer(BufferTarget.ElementArrayBuffer, iboID);

            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indices.Length * sizeof(short)), Indices, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out vaoID);

            if (GLWrapper.BindBuffer(BufferTarget.ArrayBuffer, vaoID))
                VertexUtils<MeshVertex>.Bind();

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Positions.Length * VertexUtils<MeshVertex>.STRIDE), IntPtr.Zero, BufferUsageHint.DynamicDraw);
        }

        public void Draw()
        {
            if (vaoID == -1 && iboID == -1)
                Initialise();

            if (GLWrapper.BindBuffer(BufferTarget.ArrayBuffer, vaoID))
                VertexUtils<MeshVertex>.Bind();

            var vertices = new MeshVertex[Positions.Length];
            for (int i = 0; i < Positions.Length; i++)
            {
                vertices[i] = new MeshVertex
                {
                    Position = Positions[i],
                    TexturePosition = TexturePositions[i],
                };
            }

            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, (IntPtr)(Positions.Length * VertexUtils<MeshVertex>.STRIDE), vertices);

            GLWrapper.BindBuffer(BufferTarget.ElementArrayBuffer, iboID);
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedShort, IntPtr.Zero);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MeshVertex : IVertex
        {
            [VertexMember(2, VertexAttribPointerType.Float, false)]
            public Vector2 Position;

            [VertexMember(2, VertexAttribPointerType.Float, false)]
            public Vector2 TexturePosition;
        }

        protected bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (vaoID != -1 && iboID != -1)
            {
                GL.DeleteBuffers(2, new[] { vaoID, iboID });
                vaoID = iboID = -1;
            }

            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
