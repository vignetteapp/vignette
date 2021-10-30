// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

// This is a reimplementation of the internal classes in osuTK's assembly.
// unfortunately we cannot access internal methods and classes...
// so we do it the icky way - reimplementing them.
using System.Drawing;
using System.Runtime.InteropServices;

namespace Vignette.Game.Tracking.ImageUtils
{
    public static class BitmapExtensions
    {
        public static byte[] ToByteArray(this Bitmap bitmap)
        {
            BitmapData bitmapData = null;

            try
            {
                bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                    PixelFormat.Format32BppArgb);
                int byteCount = bitmapData.Stride * bitmap.Height;
                byte[] byteData = new byte[byteCount];

                var ptr = bitmapData.Scan0;

                Marshal.Copy(ptr, byteData, 0, byteCount);

                return byteData;
            }
            finally
            {
                if (bitmapData != null)
                    bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
