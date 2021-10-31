// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Vignette.Game.Graphics
{
    public static class Utils
    {
        public static byte[] ConvertRaw<TPixelIn, TPixelOut>(byte[] data, int width, int height)
        where TPixelIn : unmanaged, IPixel<TPixelIn>
        where TPixelOut : unmanaged, IPixel<TPixelOut>
        {
            Image<TPixelIn> start = Image.LoadPixelData<TPixelIn>(data, width, height);

            Span<TPixelIn> pixels;
            if (!start.TryGetSinglePixelSpan(out pixels))
            {
                throw new InvalidOperationException("Image is too big");
            }

            TPixelOut[] dest = new TPixelOut[pixels.Length];
            Span<TPixelOut> destination = new Span<TPixelOut>(dest);
            PixelOperations<TPixelIn>.Instance.To<TPixelOut>(new SixLabors.ImageSharp.Configuration(), pixels, destination);
            start.Dispose();
            return MemoryMarshal.AsBytes(destination).ToArray();
        }
    }
}
