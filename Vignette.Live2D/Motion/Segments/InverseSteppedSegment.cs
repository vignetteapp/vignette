// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

namespace Vignette.Live2D.Motion.Segments
{
    public class InverseSteppedSegment : Segment
    {
        public InverseSteppedSegment()
            : base(2)
        {
        }

        public override double ValueAt(double time)
        {
            return Points[1].Value;
        }
    }
}
