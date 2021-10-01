// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Collections.Generic;

namespace Vignette.Live2D.IO.Serialization
{
    [Serializable]
    public class CubismMotionSetting : ICubismJsonSetting
    {
        public int Version { get; set; }

        public Metadata Meta { get; set; }

        public List<Curve> Curves { get; set; }

        public List<Userdata> UserData { get; set; }

        public class Metadata
        {
            public float Duration { get; set; } = -1.0f;

            public double Fps { get; set; } = 15.0;

            public bool Loop { get; set; }

            public bool AreBeziersRestricted { get; set; }

            public int CurveCount { get; set; }

            public int TotalSegmentCount { get; set; }

            public int TotalPointCount { get; set; }

            public int UserDataCount { get; set; }

            public int TotalUserDataSize { get; set; }

            public double FadeInTime { get; set; } = double.NaN;

            public double FadeOutTime { get; set; } = double.NaN;
        }

        public class Curve
        {
            public string Target { get; set; }

            public string Id { get; set; }

            public List<float> Segments { get; set; }

            public double FadeInTime { get; set; } = double.NaN;

            public double FadeOutTime { get; set; } = double.NaN;
        }

        public class Userdata
        {
            public float Time { get; set; }

            public string Value { get; set; }
        }
    }
}
