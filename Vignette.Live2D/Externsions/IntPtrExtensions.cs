// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Runtime.InteropServices;

namespace Vignette.Live2D.Extensions
{
    internal static class IntPtrExtensions
    {
        public static IntPtr Align(this IntPtr ptr, int offset)
        {
            IntPtr aligned;
            int totalOffset = 0;

            do
            {
                aligned = IntPtr.Add(ptr, totalOffset);
                totalOffset++;
            } while ((ulong)aligned % (ulong)offset != 0);

            return aligned;
        }
    }
}
