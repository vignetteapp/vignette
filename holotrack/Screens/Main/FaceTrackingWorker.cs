using FaceRecognitionDotNet;
using holotrack.Vision;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cubism;
using CubismFramework;
using System;
using System.Linq;
using System.Reflection;

namespace holotrack.Screens.Main
{

    internal enum StandardParameters
    {

    }

    class FaceTrackingWorker : Component
    {
        public byte[] cameraStream;
        public CubismSprite live2dContainer;

        protected override void Update()
        {
            FaceRecognition fr = FaceRecognition.Create(".models");
            // FIXME: for now we hardcode it to Face ID 0. Next time we need to define targets. Probably set it on ctor.
            var faceDict = FaceTracking.GetLandmark(0, cameraStream);

            // eyeblink detector for maximum i m m e r s i o n
            fr.EyeBlinkDetect(faceDict, out bool leftBlinked, out bool rightBlinked);

            // initialize the face parts
            // TODO: get rid of this unholy variable block. This thing is denser than a isekai MC
            var leftEye = faceDict[FacePart.LeftEye].ToArray();
            var leftEyebrow = faceDict[FacePart.LeftEyebrow].ToArray();
            var rightEye = faceDict[FacePart.RightEye].ToArray();
            var rightEyebrow = faceDict[FacePart.RightEyebrow].ToArray();
            var nose = faceDict[FacePart.Nose].ToArray();
            var upperLip = faceDict[FacePart.TopLip].ToArray();
            var lowerLip = faceDict[FacePart.BottomLip].ToArray();


            if (leftBlinked && rightBlinked)
            {
                // have our character blink here, then hand over the  parameter back to the individual parameters.
            }

            // set the parameters here if you have any.

        }

        [BackgroundDependencyLoader]
        private void load()
        {
            // in some nasty edge case we might end up with a empty model.
            // we do not want to keep tracking if there is no model.
            if (!live2dContainer.IsAlive)
            {
                throw new NotSupportedException("Error: Worker will not track on a non-existent container!");
            }

            return;
        }
    }
}
