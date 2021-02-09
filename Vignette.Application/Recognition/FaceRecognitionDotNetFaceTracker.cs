// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FaceRecognitionDotNet;
using OpenCvSharp;
using osu.Framework.Graphics.Textures;
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

        private static string getAssemblyDirectory
        {
            get
            {
                var assemblyName = Assembly.GetExecutingAssembly().Location;
                var UriBuilder = new UriBuilder(assemblyName);
                var path = Uri.UnescapeDataString(UriBuilder.Path);

                return Path.GetDirectoryName(path);
            }
        }

        public FaceRecognitionDotNetFaceTracker()
        {
            string basePath = $@"{getAssemblyDirectory}/recognition/models";
            instance = FaceRecognition.Create(basePath);
        }

        protected override IEnumerable<Face> Track()
        {
            using var memory = new MemoryStream(Camera.Data);
            using var bitmap = new Bitmap(memory);

            float detectionScale = DetectionSize / (float)Math.Max(DetectionSize, Math.Max(bitmap.Width, bitmap.Height));
            float landmarkScale = LandmarkSize / (float)Math.Max(LandmarkSize, Math.Max(bitmap.Width, bitmap.Height));

            using var detectionBitmap = new Bitmap(bitmap, System.Drawing.Size.Round(bitmap.Size * detectionScale));
            using var landmarkBitmap = new Bitmap(bitmap, System.Drawing.Size.Round(bitmap.Size * landmarkScale));
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

        public override Vector3? GetHeadAngles(int index = 0)
        {
            throw new NotImplementedException();
        }

        public override Vector2? GetHeadPosition(int index = 0)
        {
            throw new NotImplementedException();
        }

        public override Vector2? GetEyePupilPosition(FaceRegion eye, int headIndex = 0)
        {
            if (!(eye == FaceRegion.LeftEye || eye == FaceRegion.RightEye))
                throw new ArgumentOutOfRangeException($@"{eye} is not an eye.");

            if (Faces == null || Camera.Data == null)
                return null;

            var faces = Faces.ToArray();

            if (headIndex >= faces.Length)
                return null;

            var data = Camera.Data.ToArray();
            var mat = Mat.FromImageData(data);
            var eyeRect = faces[headIndex].GetRegionBounds(eye);

            // Apply filters to matrix to filter out possible noise from the region of interest
            var eyeMat = mat[new Rect((int)eyeRect.X, (int)eyeRect.Y, (int)eyeRect.Width, (int)eyeRect.Height)]
                .CvtColor(ColorConversionCodes.BGR2GRAY)
                .GaussianBlur(new Size(7, 7), 0);

            var contours = Cv2.FindContoursAsArray(eyeMat.Threshold(3, 255, ThresholdTypes.BinaryInv), RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            if (contours.Length > 0)
            {
                // Find the biggest contour as that is usually the pupil
                var sorted = contours.OrderBy(c => Cv2.ContourArea(c));
                var contourRect = Cv2.BoundingRect(sorted.First());

                var position = new Vector2(contourRect.Width / eyeRect.Width, contourRect.Height / eyeRect.Height);
                position.Normalize();

                return position;
            }
            else
                return null;
        }

        public override float? GetEyeLidOpen(FaceRegion eye, int headIndex = 0)
        {
            if (eye != FaceRegion.LeftEye || eye != FaceRegion.RightEye)
                throw new ArgumentOutOfRangeException($@"Face Region {eye} is not an eye.");

            throw new NotImplementedException();
        }

        public override float? GetMouthOpen(int index = 0)
        {
            throw new NotImplementedException();
        }

        private static FaceRegion facePartToRegion(FacePart point)
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
