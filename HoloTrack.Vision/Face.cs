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
        public static IDictionary<FacePart, IEnumerable<FacePoint>> GetLadmark(int faceIndex, int cameraID=0)
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
    }
}