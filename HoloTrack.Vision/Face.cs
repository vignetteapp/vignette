using System;
using FaceRecognitionDotNet;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace HoloTrack.Vision
{
    /// <summary>
    /// A class that implements face targeting using DLib.
    /// </summary>
    public class Face
    {
        public static FaceRecognition faceRecognition;

        /// <summary>
        /// Perform Inference and get all valid targets. Note that you must execute this asynchronously otherwise this will block the main thread.
        /// </summary>
        public static Location[] GetTargets(int cameraID=0)
        {
            Bitmap imageFromCamera = Camera.CreateCameraImage(cameraID);
            FaceRecognitionDotNet.Image image = FaceRecognition.LoadImage(imageFromCamera);

            return faceRecognition.FaceLocations(image).ToArray();
        }

        /// <summary>
        /// Gets landmark data from a specific face data.
        /// </summary>
        /// <param name="faceIndex">the ID of the face to get landmarks. To get your face index, you might want to get a single OpenCV location and compare it against the output of Face.GetTargets()</param>
        /// <param name="cameraID">the ID of the camera you want to open a stream. Use Camera.EnumerateCameras() for this.</param>
        /// <returns></returns>
        public static Dictionary<FacePart, IEnumerable<FacePoint>> GetLandmark(int faceIndex, int cameraID=0)
        {
            Dictionary<FacePart, IEnumerable<FacePoint>> requiredLandmarks = new Dictionary<FacePart, IEnumerable<FacePoint>>();
            Bitmap image = Camera.CreateCameraImage(cameraID);
            using FaceRecognitionDotNet.Image faceLandmarks = FaceRecognition.LoadImage(image);
            var targetLandmarks = faceRecognition.FaceLandmark(faceLandmarks).ToArray();

            foreach (var landmark in targetLandmarks)
            {
                //  get all landmark points one by one.
                var leftEyes = landmark[FacePart.LeftEye].ToArray();
                var rightEyes = landmark[FacePart.RightEye].ToArray();
                var leftEyebrows = landmark[FacePart.LeftEyebrow].ToArray();
                var rightEyebrows = landmark[FacePart.RightEyebrow].ToArray();
                var noseBridges = landmark[FacePart.NoseBridge].ToArray();
                var noseTips = landmark[FacePart.NoseTip].ToArray();
                var topLips = landmark[FacePart.TopLip].ToArray();
                var bottomLips = landmark[FacePart.BottomLip].ToArray();
                var chins = landmark[FacePart.Chin].ToArray();

                // build the dictionary from what we got from the landmark, only this time we want only a specific face's landmark.
                // after that, we return it!
                requiredLandmarks.Add(FacePart.LeftEyebrow, (IEnumerable<FacePoint>)leftEyebrows[faceIndex]);
                requiredLandmarks.Add(FacePart.RightEyebrow, (IEnumerable<FacePoint>)rightEyebrows[faceIndex]);
                requiredLandmarks.Add(FacePart.LeftEye, (IEnumerable<FacePoint>)leftEyes[faceIndex]);
                requiredLandmarks.Add(FacePart.RightEye, (IEnumerable<FacePoint>)rightEyes[faceIndex]);
                requiredLandmarks.Add(FacePart.NoseBridge, (IEnumerable<FacePoint>)noseBridges[faceIndex]);
                requiredLandmarks.Add(FacePart.NoseTip, (IEnumerable<FacePoint>)noseTips[faceIndex]);
                requiredLandmarks.Add(FacePart.TopLip, (IEnumerable<FacePoint>)topLips[faceIndex]);
                requiredLandmarks.Add(FacePart.BottomLip, (IEnumerable<FacePoint>)bottomLips[faceIndex]);
                requiredLandmarks.Add(FacePart.Chin, (IEnumerable<FacePoint>)chins[faceIndex]);
            }

            return requiredLandmarks;

        }
    }
}