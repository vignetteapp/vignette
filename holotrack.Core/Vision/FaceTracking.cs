using FaceRecognitionDotNet;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace holotrack.Core.Vision
{
    /// <summary>
    /// A class that implements face targeting using DLib.
    /// </summary>
    public class FaceTracking
    {
        public static FaceRecognition faceRecognition;

        /// <summary>
        /// Perform Inference and get all valid targets. Note that you must execute this asynchronously otherwise this will block the main thread.
        /// </summary>
        public static Location[] GetTargets(byte[] cameraData)
        {
            Bitmap imageFromCamera = new Bitmap(new MemoryStream(cameraData));
            FaceRecognitionDotNet.Image image = FaceRecognition.LoadImage(imageFromCamera);

            return faceRecognition.FaceLocations(image).ToArray();
        }


        /// <summary>
        /// Gets landmark from a specific face. This is a simplified version of GetLandmark() and considered experimental. Use at your own risk.
        /// </summary>
        /// <param name="faceIndex">the ID of the face to get landmarks. To get your face index, you might want to get your face location from GetTargets() and get the index of the face you want to track.</param>
        /// <param name="cameraData">the image data of the camera.</param>
        /// <returns></returns>
        public static IDictionary<FacePart, IEnumerable<FacePoint>> GetLandmark(int faceIndex, byte[] cameraData)
        {
            // create a new list and only get the single target from our ID.
            List<Location> faceLocation = new List<Location>();
            Bitmap image = new Bitmap(new MemoryStream(cameraData));
            Location[] faceLocations = GetTargets(cameraData);

            faceLocation.Add(faceLocations[faceIndex]);
            using FaceRecognitionDotNet.Image cameraImage = FaceRecognition.LoadImage(image);

            // now targetLandmark will only listen to a specific location since there's only one location to listen to.
            var targetLandmarks = faceRecognition.FaceLandmark(cameraImage, faceLocations).ToArray();

            return targetLandmarks[0];
        }
    }
}