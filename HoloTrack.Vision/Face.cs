using System;
using FaceRecognitionDotNet;
using System.Drawing;
using System.Linq;

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
        public static Location[] GetTargets()
        {
            Bitmap imageFromCamera = Camera.CreateCameraImage();
            FaceRecognitionDotNet.Image image = FaceRecognition.LoadImage(imageFromCamera);

            return faceRecognition.FaceLocations(image).ToArray();
        }

        /// <summary>
        /// Gets all the Landmark data from a target face location.
        /// </summary>
        public static System.Collections.Generic.IDictionary<FacePart, System.Collections.Generic.IEnumerable<FacePoint>>[] GetLandmarks()
        {
            Bitmap image = Camera.CreateCameraImage();
            using (var faceLandmarks = FaceRecognition.LoadImage(image))
            {
                return faceRecognition.FaceLandmark(faceLandmarks).ToArray();
            }
        }
    }
}