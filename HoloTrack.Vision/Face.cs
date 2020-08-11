using System;
using FaceRecognitionDotNet;
using osu.Framework.IO.Stores;
using HoloTrack.Resources;
using System.Drawing;
using System.Linq;

namespace HoloTrack.Vision
{
    /// <summary>
    /// A class that implements face targeting using DLib.
    /// </summary>
    public class Face
    {
        private static readonly FaceRecognition faceRecognition;

        /// <summary>
        /// Loads the Appropriate DLib Model for inference.
        /// </summary>
        /// <param name="model">The name of the model, note this must exist inside HoloTrack.Resources.</param>
        /// <returns>The byte array for the model.</returns>
        internal static byte[] LoadModel(string model)
        {
            DllResourceStore storage = new DllResourceStore(typeof(HoloTrackResource).Assembly);

            return storage.Get($"Models/{model}.dat");
        }

        /// <summary>
        /// Perform Inference and get all valid targets. Note that you must execute this asynchronously otherwise this will block the main thread.
        /// </summary>
        /// <param name="model">the name of the model as defined in HoloTrack.Resources.</param>
        public static Location[] GetTargets()
        {
            Bitmap imageFromCamera = Camera.CreateCameraImage();
            FaceRecognitionDotNet.Image image = FaceRecognition.LoadImage(imageFromCamera);

            return faceRecognition.FaceLocations(image).ToArray();
        }

        /// <summary>
        /// Gets all the Landmark data from a target face location.
        /// </summary>
        /// <param name="faceTarget">the target face location.</param>
        public static System.Collections.Generic.IDictionary<FacePart, System.Collections.Generic.IEnumerable<FacePoint>>[] GetLandmarks(Location faceTarget)
        {
            // We'll need to get the index of our matching target. We'll use this later.
            Location[] faceTargets = GetTargets();
            int target = Array.BinarySearch(faceTargets, faceTarget);

            // A little sanity check so we don't encounter nasty stuff on the long run.
            if (faceTargets[target] !=  faceTarget)
            {
                throw new ArgumentOutOfRangeException("Error: FaceTarget value is not the same as target!");
            }

            // FIXME: Use provided FaceTarget only! We may crop the canvas to only use that face target. Not use the entirety of it.
            Bitmap image = Camera.CreateCameraImage();
            using (var faceLandmarks = FaceRecognition.LoadImage(image))
            {
                return faceRecognition.FaceLandmark(faceLandmarks).ToArray();
            }
        }
    }
}