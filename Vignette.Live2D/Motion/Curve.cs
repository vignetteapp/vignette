// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System.Collections.Generic;
using System.Linq;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Motion.Segments;

namespace Vignette.Live2D.Motion
{
    public class Curve
    {
        public IEnumerable<Segment> Segments { get; set; }

        public CubismMotionTarget TargetType { get; set; }

        public CubismId Effect { get; set; }

        public CubismPart Part { get; set; }

        public CubismParameter Parameter { get; set; }

        public double FadeInTime { get; set; }

        public double FadeOutTime { get; set; }

        public double ValueAt(double time)
        {
            foreach (var segment in Segments)
            {
                var points = segment.Points;
                if (time <= points.Last().Time)
                    return (points[0].Time <= time) ? segment.ValueAt(time) : points[0].Value;
            }

            return Segments.Last().Points.Last().Value;
        }
    }
}
