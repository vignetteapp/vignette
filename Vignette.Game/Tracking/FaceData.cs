// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using Akihabara.Framework.Protobuf;

namespace Vignette.Game.Tracking
{
    public class FaceData
    {
        private readonly IReadOnlyList<NormalizedLandmark> landmarks;

        public float LeftEyeOpen
        {
            get
            {
                float va = get_euclidean_dist(landmarks[160], landmarks[144]);
                float vb = get_euclidean_dist(landmarks[158], landmarks[153]);
                float h = get_euclidean_dist(landmarks[33], landmarks[133]);
                return get_aspect_ratio(va, vb, h);
            }
        }

        public float RightEyeOpen
        {
            get
            {
                float va = get_euclidean_dist(landmarks[385], landmarks[380]);
                float vb = get_euclidean_dist(landmarks[387], landmarks[373]);
                float h = get_euclidean_dist(landmarks[362], landmarks[263]);
                return get_aspect_ratio(va, vb, h);
            }
        }

        public float MouthOpen
        {
            get
            {
                float va = get_euclidean_dist(landmarks[82], landmarks[87]);
                float vb = get_euclidean_dist(landmarks[312], landmarks[317]);
                float h = get_euclidean_dist(landmarks[76], landmarks[293]);
                return get_aspect_ratio(va, vb, h);
            }
        }

        public FaceData(NormalizedLandmarkList landmarkList)
        {
            landmarks = landmarkList.Landmark;
        }

        private static float get_euclidean_dist(NormalizedLandmark a, NormalizedLandmark b)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        private static float get_aspect_ratio(float va, float vb, float h)
            => (va + vb) / (2.0f * h);
    }
}
