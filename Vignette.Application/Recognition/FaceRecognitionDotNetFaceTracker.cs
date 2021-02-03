// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FaceRecognitionDotNet;
using FaceRecognitionDotNet.Extensions;
using osuTK;
using Bitmap = System.Drawing.Bitmap;
using RectangleF = osu.Framework.Graphics.Primitives.RectangleF;

namespace Vignette.Application.Recognition
{
    public class FaceRecognitionDotNetFaceTracker : FaceTracker
    {
        public int DetectionSize { get; set; } = 180;

        public int LandmarkSize { get; set; } = 520;

        private FaceRecognition instance;

        public FaceRecognitionDotNetFaceTracker()
        {
            string basePath = @"./recognition/models";
            instance = FaceRecognition.Create(basePath);
        }

        protected override IEnumerable<Face> Track()
        {
            using var bitmap = new Bitmap(Camera.Data);

            float detectionScale = DetectionSize / (float)Math.Max(DetectionSize, Math.Max(bitmap.Width, bitmap.Height));
            float landmarkScale = LandmarkSize / (float)Math.Max(LandmarkSize, Math.Max(bitmap.Width, bitmap.Height));

            using var detectionBitmap = new Bitmap(bitmap, bitmap.Size * (int)detectionScale);
            using var landmarkBitmap = new Bitmap(bitmap, bitmap.Size * (int)landmarkScale);
            using var detectionImage = FaceRecognition.LoadImage(detectionBitmap);
            using var landmarkImage = FaceRecognition.LoadImage(landmarkBitmap);

            var locations = instance.FaceLocations(detectionImage).ToArray();
            for (int i = 0; i < locations.Length; i++)
            {
                var location = locations[i];
                locations[i] = new Location(
                    (int)(location.Left / detectionScale * landmarkScale),
                    (int)(location.Top / detectionScale * landmarkScale),
                    (int)(location.Right / detectionScale * landmarkScale),
                    (int)(location.Bottom / detectionScale * landmarkScale)
                );
            }

            var landmarks = instance.FaceLandmark(landmarkImage, locations).ToArray();
            var faces = new List<Face>();
            for (int i = 0; i < locations.Length; i++)
            {
                var location = locations[i];
                var detectedFace = landmarks[i];

                var faceLandmarks = new List<FaceLandmark>();
                foreach (var part in detectedFace.Keys)
                {
                    foreach (var point in detectedFace[part])
                    {
                        faceLandmarks.Add(new FaceLandmark
                        {
                            Index = point.Index,
                            Region = facePartToRegion(part),
                            Coordinates = new Vector2(point.Point.X / landmarkScale, point.Point.Y / landmarkScale)
                        });
                    }
                }

                faces.Add(new Face
                {
                    Bounds = new RectangleF(location.Left / landmarkScale, location.Top / landmarkScale, (location.Right - location.Left) / landmarkScale, (location.Bottom - location.Top) / landmarkScale),
                    Landmarks = faceLandmarks,
                });
            }

            return faces;
        }

        private FaceRegion facePartToRegion(FacePart point)
        {
            switch (point)
            {
                case FacePart.Chin:
                    return FaceRegion.Chin;

                case FacePart.BottomLip:
                    return FaceRegion.BottomLip;

                case FacePart.LeftEye:
                    return FaceRegion.LeftEye;

                case FacePart.LeftEyebrow:
                    return FaceRegion.LeftEyebrow;

                case FacePart.Nose:
                    return FaceRegion.Nose;

                case FacePart.NoseBridge:
                    return FaceRegion.NoseBridge;

                case FacePart.NoseTip:
                    return FaceRegion.NoseTip;

                case FacePart.RightEye:
                    return FaceRegion.RightEye;

                case FacePart.RightEyebrow:
                    return FaceRegion.RightEyebrow;

                case FacePart.TopLip:
                    return FaceRegion.TopLip;

                default:
                    throw new ArgumentOutOfRangeException($@"""{nameof(point)}"" is not a FacePart.");
            }
        }
    }
}
