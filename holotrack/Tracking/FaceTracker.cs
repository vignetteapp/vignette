using FaceRecognitionDotNet;
using FaceRecognitionDotNet.Extensions;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace holotrack.Tracking
{
    /// <summary>
    /// A component responsible for processing image data for Facial Recognition
    /// </summary>
    public class FaceTracker : Component
    {
        private CameraSprite camera { get; set; }
        private FaceRecognition faceRecognition { get; set; }
        private Task trackerTask;
        private readonly CancellationTokenSource trackerCancellationSource = new CancellationTokenSource();

        private List<Face> faces;

        /// <summary>
        /// A collection of tracked faces
        /// </summary>
        public IReadOnlyList<Face> Faces => faces;

        /// <summary>
        /// The number of faces being tracked
        /// </summary>
        public int Tracked => Faces?.Count ?? 0;
 
        public bool IsTracking => Tracked > 0;

        public event Action<IReadOnlyList<Face>> OnTrackerUpdate;


        [BackgroundDependencyLoader]
        private void load(FaceRecognition faceRecognition)
        {
            this.faceRecognition = faceRecognition;
            this.faceRecognition.CustomEyeBlinkDetector = new EyeAspectRatioLargeEyeBlinkDetector(0.2, 0.2);

            trackerTask = Task.Factory.StartNew(() => trackerLoop(trackerCancellationSource.Token), trackerCancellationSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void StartTracking(CameraSprite camera) => this.camera = camera;

        public void StopTracking() => camera = null;

        private void trackerLoop(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (camera?.CaptureData == null)
                    continue;

                var bitmap = new Bitmap(new MemoryStream(camera.CaptureData));
                var image = FaceRecognition.LoadImage(bitmap);
                var locations = faceRecognition.FaceLocations(image).ToArray();
                var landmarks = faceRecognition.FaceLandmark(image, locations).ToArray();

                faces = new List<Face>();

                for (int i = 0; i < locations.Length; i++)
                {
                    var loc = locations[i];
                    faces.Add(new Face
                    {
                        Landmarks = landmarks[i],
                        BoundingBox = new RectangleF((float)loc.Left, (float)loc.Top, (float)(loc.Right - loc.Left), (float)(loc.Bottom - loc.Top))
                    });
                }

                OnTrackerUpdate?.Invoke(faces);
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            trackerCancellationSource.Cancel();
            trackerTask.Wait();
            trackerTask.Dispose();
            trackerCancellationSource.Dispose();
        }

        public struct Face
        {
            public RectangleF BoundingBox;
            public IDictionary<FacePart, IEnumerable<FacePoint>> Landmarks;
        }
    }
}
