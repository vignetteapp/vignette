// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.
using System;

namespace Vignette.Game.Tracking.ImageUtils
{
    internal sealed class BitmapData
    {
        internal BitmapData(int width, int height, int stride)
        {
            this.Width = width;
            this.Height = height;
            this.Stride = stride;
        }

        public IntPtr Scan0 => IntPtr.Zero;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int Stride { get; private set; }
    }
}
