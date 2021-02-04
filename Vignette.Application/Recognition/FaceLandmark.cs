// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osuTK;

namespace Vignette.Application.Recognition
{
    public struct FaceLandmark : IEquatable<FaceLandmark>
    {
        public int Index { get; set; }

        public Vector2 Coordinates { get; set; }

        public FaceRegion Region { get; set; }

        public bool Equals(FaceLandmark other)
        {
            return other.Index == Index && other.Coordinates == Coordinates && other.Region == Region;
        }

        public override bool Equals(object obj)
        {
            return obj is FaceLandmark landmark && Equals(landmark);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(FaceLandmark left, FaceLandmark right) => left.Equals(right);

        public static bool operator !=(FaceLandmark left, FaceLandmark right) => !(left == right);
    }
}
