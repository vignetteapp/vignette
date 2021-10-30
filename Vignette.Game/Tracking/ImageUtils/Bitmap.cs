// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.
using System.Drawing;

namespace Vignette.Game.Tracking.ImageUtils
{
    public sealed class Bitmap : Image
    {
        public Bitmap()
        {
        }

        public Bitmap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        internal int Width { get; }

        public int Height { get; }

        public Color GetPixel(int x, int y) => new Color();

        internal void UnlockBits(BitmapData data)
        {
        }

        internal BitmapData LockBits(Rectangle rectangle, ImageLockMode imageLockMode, PixelFormat format) => new BitmapData(this.Width, this.Height, 0);

        internal static int GetPixelFormatSize(PixelFormat format) => 32;

    }
}
