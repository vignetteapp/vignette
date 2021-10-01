// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

namespace Vignette.Live2D
{
    public struct CubismVersion
    {
        public int Major { get; }

        public int Minor { get; }

        public int Patch { get; }

        public CubismVersion(uint number)
        {
            Major = (int)((number & 0xFF000000) >> 24);
            Minor = (int)((number & 0x00FF0000) >> 16);
            Patch = (int)(number & 0x0000FFFF);
        }
    }
}
