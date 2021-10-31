// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using Akihabara.Framework.Protobuf;


namespace Vignette.Game.Screens.Stage
{
    class ImportantLandmarks
    {
        public NormalizedLandmark FaceTop { get; private set; }
        public NormalizedLandmark FaceLeft { get; private set; }
        public NormalizedLandmark FaceBottom { get; private set; }
        public NormalizedLandmark FaceRight { get; private set; }
        public NormalizedLandmark FaceMiddle { get; private set; }

        public NormalizedLandmark LeftEyeTop { get; private set; }
        public NormalizedLandmark LeftEyeLeft { get; private set; }
        public NormalizedLandmark LeftEyeBottom { get; private set; }
        public NormalizedLandmark LeftEyeRight { get; private set; }

        public NormalizedLandmark RightEyeTop { get; private set; }
        public NormalizedLandmark RightEyeLeft { get; private set; }
        public NormalizedLandmark RightEyeBottom { get; private set; }
        public NormalizedLandmark RightEyeRight { get; private set; }

        public NormalizedLandmark MouthTop { get; private set; }
        public NormalizedLandmark MouthLeft { get; private set; }
        public NormalizedLandmark MouthBottom { get; private set; }
        public NormalizedLandmark MouthRight { get; private set; }

        public ImportantLandmarks(NormalizedLandmarkList landmarks)
        {
            // See https://raw.githubusercontent.com/tensorflow/tfjs-models/master/face-landmarks-detection/mesh_map.jpg
            var mark = landmarks.Landmark;

            FaceTop = mark[10];
            FaceLeft = mark[234];
            FaceBottom = mark[152];
            FaceRight = mark[454];

            LeftEyeTop = mark[159];
            LeftEyeLeft = mark[33];
            LeftEyeBottom = mark[145];
            LeftEyeRight = mark[133];

            RightEyeTop = mark[386];
            RightEyeLeft = mark[263];
            RightEyeBottom = mark[374];
            RightEyeRight = mark[362];

            MouthTop = mark[13];
            MouthLeft = mark[61];
            MouthBottom = mark[14];
            MouthRight = mark[291];

            FaceMiddle = new NormalizedLandmark();
            FaceMiddle.X = (FaceTop.X + FaceRight.X + FaceBottom.X + FaceLeft.X) / 4;
            FaceMiddle.Y = (FaceTop.Y + FaceRight.Y + FaceBottom.Y + FaceLeft.Y) / 4;
            FaceMiddle.Z = (FaceTop.Z + FaceRight.Z + FaceBottom.Z + FaceLeft.Z) / 4;
        }
    }
}
