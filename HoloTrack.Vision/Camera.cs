using System;
using System.IO;
using OpenCvSharp;
using osu.Framework.Graphics.Textures;

namespace HoloTrack.Vision
{
    /// <summary>
    /// A Class that manages camera input from OpenCV, and feeds it to DLib for classification.
    /// </summary>
    /// TODO: handle multiple cameras!
    internal class Camera
    {
        private static VideoCapture capture;

        public Camera() => capture = new VideoCapture();

        /// <summary>
        /// Gets the camera stream from the camera.
        /// </summary>
        /// <returns>Video Stream in a Mat - you will need to convert this.</returns>
        internal static Mat GetRawCameraStream ()
        {
            capture.Open(0, VideoCaptureAPIs.ANY);

            if (!capture.IsOpened())
            {
                throw new Exception("Not opening a new Video Capture. There's one open already!");
            }

            return capture.RetrieveMat();

        }

        /// <summary>
        /// Returns a Camera Stream in a osu! OpenGL Texture.
        /// </summary>
        public static CameraTexture CreateCameraTexture ()
        {
            Mat rawStream = GetRawCameraStream();

            // we need to convert this to a byte array first.
            var byteStream = rawStream.ToBytes();
            var upload = new TextureUpload(new MemoryStream(byteStream));
            var cameraTexture = new Texture(upload.Width, upload.Height);

            //set the texture then we return it. We'll let the entire stuff do the rest.
            cameraTexture.SetData(upload);


            return new CameraTexture(cameraTexture);

        }

        /// <summary>
        /// Returns a camera stream into a byte array.
        /// </summary>
        public static byte[] CreateCameraVideoByte ()
        {
            return GetRawCameraStream().ToBytes();
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
