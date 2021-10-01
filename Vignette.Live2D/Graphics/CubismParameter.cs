// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;

namespace Vignette.Live2D.Graphics
{
    public class CubismParameter : CubismId
    {
        public readonly float Minimum;

        public readonly float Maximum;

        public readonly float Default;

        private float val;

        public float Value
        {
            get => val;
            set => val = Math.Clamp(value, Minimum, Maximum);
        }

        public CubismParameter(int id, string name, float min, float max, float def)
        {
            Name = name;
            ID = id;
            Minimum = min;
            Maximum = max;
            Default = def;
        }
    }
}
