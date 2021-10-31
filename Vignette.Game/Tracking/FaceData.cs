// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Numerics;
using System.Collections.Generic;
using Akihabara.Framework.Protobuf;

namespace Vignette.Game.Tracking
{
    public class FaceData
    {
        private readonly IReadOnlyList<NormalizedLandmark> landmarks;

        private NormalizedLandmark faceTop => landmarks[10];
        private NormalizedLandmark faceLeft => landmarks[234];
        private NormalizedLandmark faceBottom => landmarks[152];
        private NormalizedLandmark faceRight => landmarks[454];

        private NormalizedLandmark leftEyeTopRight => landmarks[158];
        private NormalizedLandmark leftEyeTopLeft => landmarks[160];
        private NormalizedLandmark leftEyeLeft => landmarks[33];
        private NormalizedLandmark leftEyeBottomLeft => landmarks[144];
        private NormalizedLandmark leftEyeBottomRight => landmarks[153];
        private NormalizedLandmark leftEyeRight => landmarks[133];

        private NormalizedLandmark rightEyeTopRight => landmarks[387];
        private NormalizedLandmark rightEyeTopLeft => landmarks[385];
        private NormalizedLandmark rightEyeLeft => landmarks[362];
        private NormalizedLandmark rightEyeBottomLeft => landmarks[380];
        private NormalizedLandmark rightEyeBottomRight => landmarks[373];
        private NormalizedLandmark rightEyeRight => landmarks[263];

        private NormalizedLandmark mouthTopRight => landmarks[312];
        private NormalizedLandmark mouthTopLeft => landmarks[82];
        private NormalizedLandmark mouthLeft => landmarks[61];
        private NormalizedLandmark mouthBottomLeft => landmarks[87];
        private NormalizedLandmark mouthBottomRight => landmarks[317];
        private NormalizedLandmark mouthRight => landmarks[291];

        public float LeftEyeOpen
        {
            get
            {
                float va = euclidian_distance(leftEyeTopLeft, leftEyeBottomLeft);
                float vb = euclidian_distance(leftEyeTopRight, leftEyeBottomRight);
                float h = euclidian_distance(leftEyeLeft, leftEyeRight);
                return aspect_ratio(va, vb, h);
            }
        }

        public float RightEyeOpen
        {
            get
            {
                float va = euclidian_distance(rightEyeTopLeft, rightEyeBottomLeft);
                float vb = euclidian_distance(rightEyeTopRight, rightEyeBottomRight);
                float h = euclidian_distance(rightEyeLeft, rightEyeRight);
                return aspect_ratio(va, vb, h);
            }
        }

        public float MouthOpen
        {
            get
            {
                float va = euclidian_distance(mouthTopLeft, mouthBottomLeft);
                float vb = euclidian_distance(mouthTopRight, mouthBottomRight);
                float h = euclidian_distance(mouthLeft, mouthRight);
                return aspect_ratio(va, vb, h);
            }
        }

        public Vector3 Angles
        {
            get
            {
                var angles = new Vector3();
                angles.X = MathF.Acos((faceRight.Y - faceLeft.Y) / euclidian_distance_yz(faceLeft, faceRight));
                angles.Y = MathF.Acos((faceRight.X - faceLeft.X) / euclidian_distance_xz(faceLeft, faceRight));
                angles.Z = MathF.Acos((faceRight.X - faceLeft.X) / euclidian_distance_xy(faceLeft, faceRight));
                return angles;
            }
        }

        public FaceData(NormalizedLandmarkList landmarkList)
        {
            landmarks = landmarkList.Landmark;
        }

        private static float euclidian_distance(NormalizedLandmark a, NormalizedLandmark b)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            float dz = b.Z - a.Z;
            return MathF.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        private static float distance(float x, float y) => MathF.Sqrt(x * x + y * y);

        private static float euclidian_distance_xy(NormalizedLandmark a, NormalizedLandmark b) => distance(b.X - a.X, b.Y - a.Y);
        private static float euclidian_distance_xz(NormalizedLandmark a, NormalizedLandmark b) => distance(b.X - a.X, b.Z - a.Z);
        private static float euclidian_distance_yz(NormalizedLandmark a, NormalizedLandmark b) => distance(b.Y - a.Y, b.Z - a.Z);

        private static float aspect_ratio(float va, float vb, float h)
            => (va + vb) / (2.0f * h);
    }
}
