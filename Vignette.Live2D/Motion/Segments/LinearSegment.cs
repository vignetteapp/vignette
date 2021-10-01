// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;

namespace Vignette.Live2D.Motion.Segments
{
    public class LinearSegment : Segment
    {
        public LinearSegment()
            : base(2)
        {
        }

        public override double ValueAt(double time)
        {
            double t = (time - Points[0].Time) / (Points[1].Time - Points[0].Time);
            t = Math.Max(t, 0);
            return Points[0].Value + ((Points[1].Value - Points[0].Value) * t);
        }
    }
}
