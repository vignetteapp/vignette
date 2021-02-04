// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics.Primitives;

namespace Vignette.Application.Recognition
{
    public struct Face
    {
        public IEnumerable<FaceLandmark> Landmarks { get; set; }

        public RectangleF Bounds { get; set; }

        public RectangleF GetRegionBounds(FaceRegion region)
        {
            var points = Landmarks.Where(m => m.Region == region);
            var xValues = points.Select(p => p.Coordinates.X);
            var yValues = points.Select(p => p.Coordinates.Y);

            if (!xValues.Any() || !yValues.Any())
                return RectangleF.Empty;

            float xMin = xValues.Min();
            float xMax = xValues.Max();
            float yMin = yValues.Min();
            float yMax = yValues.Max();

            return new RectangleF(xMin, yMin, xMax - xMin, yMax - yMin);
        }
    }
}
