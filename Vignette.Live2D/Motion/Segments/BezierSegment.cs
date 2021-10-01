// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;

namespace Vignette.Live2D.Motion.Segments
{
    public class BezierSegment : Segment
    {
        public BezierSegment()
            : base(4)
        {
        }

        public override double ValueAt(double time)
        {
            double t = (time - Points[0].Time) / (Points[3].Time - Points[0].Time);
            t = Math.Max(t, 0);
            var p01 = lerp(Points[0], Points[1], t);
            var p12 = lerp(Points[1], Points[2], t);
            var p23 = lerp(Points[2], Points[3], t);
            var p012 = lerp(p01, p12, t);
            var p123 = lerp(p12, p23, t);
            return lerp(p012, p123, t).Value;
        }

        private static ControlPoint lerp(ControlPoint a, ControlPoint b, double t)
        {
            return new ControlPoint
            {
                Time = a.Time + ((b.Time - a.Time) * t),
                Value = a.Value + ((b.Value - a.Value) * t)
            };
        }
    }
}
