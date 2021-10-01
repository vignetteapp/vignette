// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Collections.Generic;
using osu.Framework.Graphics.OpenGL.Buffers;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace Vignette.Live2D.Graphics
{
    internal class MaskingContext : IDisposable
    {
        /// <summary>
        /// The drawables being masked by this context. Used in calculating <see cref="Bounds"/>.
        /// </summary>
        public readonly List<CubismDrawable> Drawables = new List<CubismDrawable>();

        /// <summary>
        /// The masks to draw to the frame buffer.
        /// </summary>
        public IEnumerable<CubismDrawable> Masks { get; init; }

        /// <summary>
        /// The matrix used for drawing.
        /// </summary>
        public Matrix4 DrawMatrix { get; set; } = Matrix4.Identity;

        /// <summary>
        /// The matrix used for drawing the mask.
        /// </summary>
        public Matrix4 MaskMatrix { get; set; } = Matrix4.Identity;

        /// <summary>
        /// The frame buffer that contains the masking texture.
        /// </summary>
        public FrameBuffer FrameBuffer { get; } = new FrameBuffer { Size = new Vector2(256) };

        /// <summary>
        /// The total clipped bounds.
        /// </summary>
        public RectangleF Bounds
        {
            get
            {
                var bounds = RectangleF.Empty;

                foreach (var drawable in Drawables)
                    bounds = RectangleF.Union(bounds, drawable.VertexBounds);

                return bounds;
            }
        }

        public bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            FrameBuffer.Dispose();
            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
