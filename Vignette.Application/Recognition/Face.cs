// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Graphics.Primitives;

namespace Vignette.Application.Recognition
{
    public struct Face
    {
        public IEnumerable<FaceLandmark> Landmarks { get; set; }

        public RectangleF Bounds { get; set; }
    }
}
