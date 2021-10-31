// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using Akihabara.Framework.Protobuf;

namespace Vignette.Game.Screens.Stage
{
    public class FaceControlPoints
    {
        public NormalizedLandmark FaceTop { get; private set; }
        public NormalizedLandmark FaceLeft { get; private set; }
        public NormalizedLandmark FaceBottom { get; private set; }
        public NormalizedLandmark FaceRight { get; private set; }
        public NormalizedLandmark FaceMiddle { get; private set; }

        public NormalizedLandmark LeftEyeTop { get; private set; }
        public NormalizedLandmark LeftEyeBottom { get; private set; }
        // public NormalizedLandmark LeftEyeLeft { get; private set; }
        // public NormalizedLandmark LeftEyeRight { get; private set; }

        public NormalizedLandmark RightEyeTop { get; private set; }
        public NormalizedLandmark RightEyeBottom { get; private set; }
        // public NormalizedLandmark RightEyeLeft { get; private set; }
        // public NormalizedLandmark RightEyeRight { get; private set; }

        public NormalizedLandmark MouthTop { get; private set; }
        public NormalizedLandmark MouthLeft { get; private set; }
        public NormalizedLandmark MouthBottom { get; private set; }
        public NormalizedLandmark MouthRight { get; private set; }

        public float LeftEyeOpen { get; private set; }
        public float RightEyeOpen { get; private set; }
        public float MouthOpen { get; private set; }
        public float MouthStretch { get; private set; }

        public FaceControlPoints(NormalizedLandmarkList landmarks)
        {
            // See https://raw.githubusercontent.com/tensorflow/tfjs-models/master/face-landmarks-detection/mesh_map.jpg
            var mark = landmarks.Landmark;

            FaceTop = mark[10];
            FaceLeft = mark[234];
            FaceBottom = mark[152];
            FaceRight = mark[454];

            LeftEyeTop = mark[159];
            LeftEyeBottom = mark[145];
            // LeftEyeLeft = mark[33];
            // LeftEyeRight = mark[133];

            RightEyeTop = mark[386];
            RightEyeBottom = mark[374];
            // RightEyeLeft = mark[263];
            // RightEyeRight = mark[362];

            MouthTop = mark[13];
            MouthLeft = mark[61];
            MouthBottom = mark[14];
            MouthRight = mark[291];

            FaceMiddle = new NormalizedLandmark();
            FaceMiddle.X = (FaceTop.X + FaceRight.X + FaceBottom.X + FaceLeft.X) / 4;
            FaceMiddle.Y = (FaceTop.Y + FaceRight.Y + FaceBottom.Y + FaceLeft.Y) / 4;
            FaceMiddle.Z = (FaceTop.Z + FaceRight.Z + FaceBottom.Z + FaceLeft.Z) / 4;

            LeftEyeOpen = normalizedLandmarkDistance(LeftEyeBottom, LeftEyeTop);
            RightEyeOpen = normalizedLandmarkDistance(RightEyeBottom, RightEyeTop);
            MouthOpen = normalizedLandmarkDistance(MouthBottom, MouthTop);
            MouthStretch = normalizedLandmarkDistance(MouthLeft, MouthRight);
        }

        private float normalizedLandmarkDistance(NormalizedLandmark nla, NormalizedLandmark nlb)
        {
            var dx = nlb.X - nla.X;
            var dy = nlb.Y - nla.Y;
            var dz = nlb.Z - nla.Z;
            return MathF.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}
