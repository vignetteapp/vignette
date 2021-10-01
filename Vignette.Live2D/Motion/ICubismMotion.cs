// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System.Collections.Generic;

namespace Vignette.Live2D.Motion
{
    public interface ICubismMotion
    {
        public double GlobalFadeInSeconds { get; set; }

        public double GlobalFadeOutSeconds { get; set; }

        public double Weight { get; set; }

        public void Update(double time, bool loop = false);
    }
}
