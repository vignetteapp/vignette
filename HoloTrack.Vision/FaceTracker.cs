using System;
using System.Resources;
using FaceRecognitionDotNet;

namespace HoloTrack.Vision
{
    public class FaceTracker
    {
        private static FaceRecognition faceRecognition;
        private readonly Model model;

        public static Location GetFacialTargets(string webcamBuffer, Model model)
        {
            // we want to start with faceID 0 since we want to detect multiple faces.
            int faceId = 0;
            var faceLocations = faceRecognition.FaceLocations(webcamBuffer, 0, model);

            foreach (var locations in faceLocations)
            {
                return locations;
            }
        }
    }
}
