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
        /// Gets landmark from a specific face. This is a simplified version of GetLandmark() and considered experimental. Use at your own risk.
        /// </summary>
        /// <param name="faceIndex">the ID of the face to get landmarks. To get your face index, you might want to get your face location from GetTargets() and get the index of the face you want to track.</param>
        /// <param name="cameraID">the ID of the camera to get input. Use Camera.EnumerateCameras() to find out which camera to get input to. Defaults to ID 0 (default camera).</param>
        /// <returns></returns>
        public static IDictionary<FacePart, IEnumerable<FacePoint>> GetLadmark2(int faceIndex, int cameraID=0)
        {
            // create a new list and only get the single target from our ID.
            List<Location> faceLocation = new List<Location>();
            Bitmap image = Camera.CreateCameraImage(cameraID);
            Location[] faceLocations = GetTargets(cameraID);

            faceLocation.Add(faceLocations[faceIndex]);
            using FaceRecognitionDotNet.Image cameraImage = FaceRecognition.LoadImage(image);

            // now targetLandmark will only listen to a specific location since there's only one location to listen to.
            var targetLandmarks = faceRecognition.FaceLandmark(cameraImage, faceLocations).ToArray();

            return targetLandmarks[0];
        }


        /// <summary>
        /// (Obsolete) Gets landmark data from a specific face.
        /// </summary>
        /// <param name="faceIndex">the ID of the face to get landmarks. To get your face index, you might want to get a single OpenCV location and compare it against the output of Face.GetTargets()</param>
        /// <param name="cameraID">the ID of the camera to get input. Use Camera.EnumerateCameras() to find out which camera to get input to. Defaults to ID 0 (default camera).</param>
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