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
        private NormalizedLandmark faceMiddle => landmarks[1];

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

        public Vector3 Position => new Vector3(faceMiddle.X, faceMiddle.Y, faceMiddle.Z);

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
                // Y and X assigns are inverted, because the logic is a bit different for Live2D parameters...
                angles.X = MathF.Acos((faceLeft.Z - faceRight.Z) / euclidian_distance_zx(faceLeft, faceRight)) * 2 - MathF.PI;
                angles.Y = MathF.Asin((faceTop.Z - faceBottom.Z) / euclidian_distance_yz(faceBottom, faceTop)) * 2;
                angles.Z = MathF.Asin((faceRight.Y - faceLeft.Y) / euclidian_distance_xy(faceLeft, faceRight)) * 2;
                return angles;
            }
        }

        public FaceData(NormalizedLandmarkList landmarkList)
        {
            landmarks = landmarkList.Landmark;
        }

        private static float norm(float x, float y) => MathF.Sqrt(x * x + y * y);
        private static float norm(float x, float y, float z) => MathF.Sqrt(x * x + y * y + z * z);

        private static float euclidian_distance(NormalizedLandmark a, NormalizedLandmark b) => norm(b.X - a.X, b.Y - a.Y, b.Z - a.Z);
        private static float euclidian_distance_xy(NormalizedLandmark a, NormalizedLandmark b) => norm(b.X - a.X, b.Y - a.Y);
        private static float euclidian_distance_yz(NormalizedLandmark a, NormalizedLandmark b) => norm(b.Y - a.Y, b.Z - a.Z);
        private static float euclidian_distance_zx(NormalizedLandmark a, NormalizedLandmark b) => norm(b.Z - a.Z, b.X - a.X);

        private static float aspect_ratio(float va, float vb, float h)
            => (va + vb) / (2.0f * h);
    }
}
