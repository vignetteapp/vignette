using System;
using System.Drawing;
using System.IO;
using OpenCvSharp;
using osu.Framework.Graphics.Textures;

namespace HoloTrack.Vision
{
    /// <summary>
    /// A Class that manages camera input from OpenCV, and feeds it to DLib for classification.
    /// </summary>
    /// TODO: handle multiple cameras!
    internal class Camera : IDisposable
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
        /// Returns a Camera Stream in a osu! OpenGL Texture.
        /// </summary>
        public static CameraTexture CreateCameraTexture()
        {
            byte[] cameraStream = CreateCameraVideoByte();
            TextureUpload upload = new TextureUpload(new MemoryStream(cameraStream));
            Texture cameraTexture = new Texture(upload.Width, upload.Height);

            //set the texture then return it. We'll let the entire stuff do the rest.
            cameraTexture.SetData(upload);

            return new CameraTexture(cameraTexture);
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
