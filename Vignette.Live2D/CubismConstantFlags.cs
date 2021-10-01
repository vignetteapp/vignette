// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;

namespace Vignette.Live2D
{
    [Flags]
    public enum CubismConstantFlags : byte
    {
        BlendNormal = 0x0,
        BlendAdditive = 0x1,
        BlendMultiplicative = 0x2,
        IsDoubleSided = 0x4,
        IsInvertedMask = 0x8,
    }
}
