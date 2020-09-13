using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FaceRecognitionDotNet;
using osu.Framework;
using osuTK;
using Size = System.Drawing.Size;
using Bitmap = System.Drawing.Bitmap;
using RectangleF = osu.Framework.Graphics.Primitives.RectangleF;

namespace holotrack.Tracking
{
    public class FaceRecognitionDotNetFaceTracker : FaceTracker
    {
        // Face Tracking models should be placed in the output directory in the meantime
        private static FaceRecognition face_recognition => FaceRecognition.Create($"{RuntimeInfo.StartupDirectory}/models");

        /// <summary>
        /// The scale factor of face detection. If this value is float.MaxValue, scale factor is automatically decide by maximum pixel size (MaxDetectionSize)
        /// </summary>
        public float DetectionScale = float.MaxValue;

        /// <summary>
        /// The scale factor of face landmark tracking. If this value is float.MaxValue, scale factor is automatically decide by maximum pixel size (MaxLandmarkSize)
        /// </summary>
        public float LandmarkScale = float.MaxValue;

        /// <summary>
        /// Maximum image size for detection, this value is applied first. Increasing this value to find more smaller face in far place. But it may loss performance.
        /// </summary>
        public int MaxDetectionSize = 200;

        /// <summary>
        /// Maximum image size for detection, this value is applied first.
        /// </summary>
        public int MaxLandmarkSize = 520;

        protected override void UpdateState(List<Face> faces)
        {
            var stream = new MemoryStream(Camera.CaptureData);
            var rawBmp = new Bitmap(stream);

            var dScale = DetectionScale == float.MaxValue
                ? (float)MaxDetectionSize / Math.Max(MaxDetectionSize, Math.Max(rawBmp.Width, rawBmp.Height))
                : DetectionScale;

            var lScale = LandmarkScale == float.MaxValue
                ? (float)MaxLandmarkSize / Math.Max(MaxLandmarkSize, Math.Max(rawBmp.Width, rawBmp.Height))
                : LandmarkScale;

            var detectionBitmap = new Bitmap(rawBmp, new Size((int)(rawBmp.Width * dScale), (int)(rawBmp.Height * dScale)));
            var detectionImage = FaceRecognition.LoadImage(detectionBitmap);

            var landmarkBitmap = new Bitmap(rawBmp, new Size((int)(rawBmp.Width * lScale), (int)(rawBmp.Height * lScale)));
            var landmarkImage = FaceRecognition.LoadImage(landmarkBitmap);

            var locations = face_recognition.FaceLocations(detectionImage).ToArray();
            var landmarks = face_recognition.FaceLandmark(landmarkImage, locations).ToArray();

            for (int i = 0; i < locations.Length; i ++)
            {
                var location = locations[i];
                var left = ((float)location.Left / DetectionScale * LandmarkScale);
                var top = ((float)location.Top / DetectionScale * LandmarkScale);
                var right = ((float)location.Right / DetectionScale * LandmarkScale);
                var bottom = ((float)location.Bottom / DetectionScale * LandmarkScale);

                var bounds = new RectangleF
                {
                    X = left,
                    Y = top,
                    Width = right - left,
                    Height = bottom - top,
                };

                var marks = landmarks[i];
                var newMarks = new Dictionary<FacePart, IEnumerable<Vector2>>();

                foreach (var key in marks.Keys)
                {
                    var pointList = new List<Vector2>();
                    foreach (var point in marks[key])
                        pointList.Add(new Vector2(point.Point.X, point.Point.Y) / lScale);

                    newMarks.Add(fromFaceRecognitionFacePart(key), pointList);
                }

                faces.Add(new Face
                {
                    Bounds = bounds,
                    Landmarks = newMarks,
                });
            }

            landmarkImage.Dispose();
            landmarkBitmap.Dispose();
            detectionImage.Dispose();
            detectionBitmap.Dispose();
            rawBmp.Dispose();
            stream.Dispose();
        }

        private FacePart fromFaceRecognitionFacePart(FaceRecognitionDotNet.FacePart part)
        {
            switch (part)
            {
                case FaceRecognitionDotNet.FacePart.Chin:
                    return FacePart.Chin;
                case FaceRecognitionDotNet.FacePart.LeftEyebrow:
                    return FacePart.LeftEyebrow;
                case FaceRecognitionDotNet.FacePart.RightEyebrow:
                    return FacePart.RightEyebrow;
                case FaceRecognitionDotNet.FacePart.LeftEye:
                    return FacePart.LeftEye;
                case FaceRecognitionDotNet.FacePart.RightEye:
                    return FacePart.RightEye;
                case FaceRecognitionDotNet.FacePart.TopLip:
                    return FacePart.TopLip;
                case FaceRecognitionDotNet.FacePart.BottomLip:
                    return FacePart.BottomLip;
                case FaceRecognitionDotNet.FacePart.Nose:
                    return FacePart.Nose;
                case FaceRecognitionDotNet.FacePart.NoseTip:
                    return FacePart.NoseTip;
                case FaceRecognitionDotNet.FacePart.NoseBridge:
                    return FacePart.NoseBridge;
                default:
                    throw new ArgumentException($"{nameof(part)} is not a valid FacePart.");
            }
        }
    }
}
