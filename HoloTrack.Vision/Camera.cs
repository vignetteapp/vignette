using System;
using OpenCvSharp;

namespace HoloTrack.Vision
{
    /// <summary>
    /// A Class that manages camera input from OpenCV, and feeds it to DLib for classification.
    /// </summary>
    /// TODO: handle multiple cameras!
    class Camera
    {
        private static VideoCapture capture;

        public Camera() => capture = new VideoCapture();

        /// <summary>
        /// Gets the camera stream from the camera.
        /// </summary>
        /// <returns>Video Stream in a Mat - you will need to convert this.</returns>
        public static Mat GetCameraStream ()
        {
            capture.Open(0, VideoCaptureAPIs.ANY);

            if (!capture.IsOpened())
            {
                throw new Exception("Not opening a new Video Capture. There's one open already!");
            }

            return capture.RetrieveMat();

        }

        /// <summary>
        /// Disposes the camera stream. Usually you wouldn't need to use this unless you're going to terminate capture to switch to a new camera.
        /// </summary>
        public static void DestroyCameraStream ()
        {
            capture.Dispose();
        }
    }
}
