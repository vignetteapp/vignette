using System;
using System.Drawing;
using System.IO;
using OpenCvSharp;

namespace HoloTrack.Vision
{
    /// <summary>
    /// A Class that manages camera input from OpenCV, and feeds it to DLib for classification.
    /// </summary>
    /// TODO: handle multiple cameras!
    public class Camera : IDisposable
    {
        private static readonly VideoCapture capture;

        /// <summary>
        /// Gets the camera stream from the camera.
        /// </summary>
        /// <returns>Video Stream in a Mat - you will need to convert this.</returns>
        public static Mat GetRawCameraStream()
        {
            VideoCapture capture = new VideoCapture();

            capture.Open(0, VideoCaptureAPIs.ANY);

            if (!capture.IsOpened())
            {
                throw new Exception("Not opening a new Video Capture. There's one open already!");
            }

            return capture.RetrieveMat();

        }

        /// <summary>
        /// Returns a camera stream into a byte array.
        /// </summary>
        public static byte[] CreateCameraVideoByte()
        {
            return GetRawCameraStream().ToBytes();
        }

        /// <summary>
        /// Creates a Bitmap from the Camera.
        /// </summary>
        /// <returns>Bitmap from the camera input.</returns>
        public static Bitmap CreateCameraImage()
        {
            byte[] cameraStream = CreateCameraVideoByte();

            using (var ms = new MemoryStream(cameraStream))
            {
                return (Bitmap)System.Drawing.Image.FromStream(ms);
            }
        }

        /// <summary>
        /// Disposes the camera stream. Usually you wouldn't need to use this unless you're going to terminate capture to switch to a new camera.
        /// </summary>
        public void Dispose()
        {
            capture.Dispose();
        }
    }
}
