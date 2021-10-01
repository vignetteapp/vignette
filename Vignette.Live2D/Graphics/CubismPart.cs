// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;

namespace Vignette.Live2D.Graphics
{
    public class CubismPart : CubismId
    {
        public float TargetOpacity { get; set; }

        private float currentOpacity;

        public float CurrentOpacity
        {
            get => currentOpacity;
            set => currentOpacity = Math.Clamp(value, 0.0f, 1.0f);
        }

        public CubismPart(int id, string name)
        {
            Name = name;
            ID = id;
        }
    }
}
