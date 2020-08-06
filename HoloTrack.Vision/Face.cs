using System;
using FaceRecognitionDotNet;
using osu.Framework.IO.Stores;
using HoloTrack.Resources;
using System.IO;
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
            var storage = new DllResourceStore(typeof(HoloTrackResource).Assembly);

            return storage.Get($"Models/{model}.dat");
        }

        /// <summary>
        /// Perform Inference and get all valid targets. Note that you must execute this asynchronously otherwise this will block the main thread.
        /// </summary>
        /// <param name="model">the name of the model as defined in HoloTrack.Resources.</param>
        public static FaceEncoding[] GetTargets()
        {
            var cameraStream = Camera.CreateCameraVideoByte();

            using (var ms = new MemoryStream(cameraStream))
            {
                // load our camera stream into a Bitmap, then load it for inference.
                var imageFromByte = new Bitmap(System.Drawing.Image.FromStream(ms));
                var image = FaceRecognition.LoadImage(imageFromByte);

                // now we have the stream loaded from the camera, now let's return the amount of faces we detected!
                return faceRecognition.FaceEncodings(image).ToArray();
            }
        }
    }
}
