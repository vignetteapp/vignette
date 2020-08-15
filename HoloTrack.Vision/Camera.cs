using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using HoloTrack.DirectShow;
using OpenCvSharp;

namespace HoloTrack.Vision
{
    /// <summary>
    /// A Class that manages camera input from OpenCV, and feeds it to DLib for classification.
    /// </summary>
    /// TODO: handle multiple cameras!
    public class Camera : IDisposable
    {
        internal static VideoCapture capture = new VideoCapture();
        public static List<VideoDevice> videoDevices = new List<VideoDevice>();

        public struct VideoDevice
        {
            public string deviceName;
            public string deviceLocation;
        }

        /// <summary>
        /// Enumerates and lists all available Cameras in the system
        /// </summary>
        /// <returns>A List containing the camera's device location and name. Keep in mind in Linux, name and location have the same value for now.</returns>
        public static List<VideoDevice> EnumerateCameras()
        {
            // we want to be cross-platform as possible, and as a curse, we need a giant ass switch case to handle platform-specific code.
            // .NET, please don't give me 5 year old examples, it's really fucking infuriating.
            OperatingSystem os = Environment.OSVersion;
            PlatformID platform = os.Platform;
            VideoDevice device;

            switch (platform)
            {
                case PlatformID.Win32NT:
                    // we use DirectShow here to get device name and device path.
                    // I wish we had the same bindings for Linux, but reality is fickle.
                    DsDevice[] systemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

                    for (int i = 0;  i < systemCameras.Length;)
                    {

                        device.deviceName = systemCameras[i].Name;
                        device.deviceLocation = systemCameras[i].DevicePath;

                        videoDevices.Add(device);
                    }

                    break;

                case PlatformID.Unix:
                    // FIXME: this is a basic form of enumerating how many devices are there. There's no basic info yet.
                    // FIXME: I only tested this on Linux, no idea if this will work on Darwin. Screw macOS anyways.
                    List<string> devDir = new List<string>(Directory.EnumerateDirectories("/dev/"));
                    Regex videoRegex = new Regex(@"\/dev\/video[0-9]*");
                    
                    foreach (string dirName in devDir)
                    {
                        MatchCollection matches = videoRegex.Matches(dirName);

                        foreach (Match match in matches)
                        {
                            //FIXME: get metadata from /dev/videoN, for now we'll duplicate the value from each field.
                            device.deviceName = match.Value;
                            device.deviceLocation = match.Value;

                            videoDevices.Add(device);
                        }
                    }

                    break;

                default:
                    throw new NotImplementedException("OS not supported!");
            }

            return videoDevices;
        }

        /// <summary>
        /// Gets the camera stream from the camera.
        /// </summary>
        /// <param name="cameraID">the zero-index ID of the camera. Refer to EnumerateCameras() for the IDs.</param>
        /// <returns>Video Stream in a Mat - you will need to convert this.</returns>
        public static Mat GetRawCameraStream(int cameraID = 0)
        {
            capture.Open(cameraID, VideoCaptureAPIs.ANY);

            if (!capture.IsOpened())
            {
                throw new Exception("Not opening a new Video Capture. There's one open already!");
            }

            return capture.RetrieveMat();

        }

        /// <summary>
        /// Returns a camera stream into a byte array.
        /// </summary>
        /// <param name="cameraID">the zero-index ID of the camera. Refer to EnumerateCameras() for the IDs.</param>
        public static byte[] CreateCameraVideoByte(int cameraID = 0)
        {
            return GetRawCameraStream(cameraID).ToBytes();
        }

        /// <summary>
        /// Creates a Bitmap from the Camera.
        /// </summary>
        /// <param name="cameraID">the zero-index ID of the camera. Refer to EnumerateCameras() for the IDs.</param>
        /// <returns>Bitmap from the camera input.</returns>
        public static Bitmap CreateCameraImage(int cameraID = 0)
        {
            byte[] cameraStream = CreateCameraVideoByte(cameraID);

            using var ms = new MemoryStream(cameraStream);
            return (Bitmap)Image.FromStream(ms);
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
