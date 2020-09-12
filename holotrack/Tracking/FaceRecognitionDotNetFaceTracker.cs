using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FaceRecognitionDotNet;
using osu.Framework;
using osuTK;
using Bitmap = System.Drawing.Bitmap;
using RectangleF = osu.Framework.Graphics.Primitives.RectangleF;

namespace holotrack.Tracking
{
    public class FaceRecognitionDotNetFaceTracker : FaceTracker
    {
        // Face Tracking models should be placed in the output directory in the meantime
        private static FaceRecognition face_recognition => FaceRecognition.Create($"{RuntimeInfo.StartupDirectory}/models");

        protected override void UpdateState(List<Face> faces)
        {
            var bitmap = new Bitmap(new MemoryStream(Camera.CaptureData));
            var image = FaceRecognition.LoadImage(bitmap);
            var locations = face_recognition.FaceLocations(image).ToArray();
            var landmarks = face_recognition.FaceLandmark(image, locations).ToArray();

            for (int i = 0; i < locations.Length; i++)
            {
                var location = locations[i];
                var landmark = landmarks[i];

                var landmarkDict = new Dictionary<FacePart, List<Vector2>>();
                foreach (var (part, points) in landmark)
                {
                    var pointsList = new List<Vector2>();
                    foreach (var p in points)
                        pointsList.Add(new Vector2(p.Point.X, p.Point.Y));

                    landmarkDict.Add(fromFaceRecognitionFacePart(part), pointsList);
                }

                faces.Add(new Face
                {
                    Landmarks = landmarkDict,
                    Bounds = new RectangleF((float)location.Left, (float)location.Top, (float)(location.Right - location.Left), (float)(location.Bottom - location.Top))
                });
            }
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
