using System.Collections.Generic;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace holotrack.Tracking
{
    public struct Face
    {
        public Dictionary<FacePart, IEnumerable<Vector2>> Landmarks;
        public RectangleF Bounds;
    }
}