// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using Vignette.Live2D.Graphics;

namespace Vignette.Live2D.Motion
{
    public struct CubismExpressionParameter
    {
        public CubismParameter Parameter { get; set; }

        public CubismExpressionBlending Blending { get; set; }

        public double Value { get; set; }
    }
}
