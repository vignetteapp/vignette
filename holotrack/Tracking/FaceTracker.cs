using DlibDotNet;
using FaceRecognitionDotNet;
using FaceRecognitionDotNet.Extensions;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;
using osuTK.Graphics.ES20;
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

        float _scale = float.MaxValue;
        public float DetectScale
        {
            get => _scale;
            set =>_scale = value;
        }

        int _maxSize = 350;
        public int MaxSize
        {
            get => _maxSize;
            set => _maxSize = value;
        }

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

                var mstream = new MemoryStream(camera.CaptureData);
                var raw_bitmap = new Bitmap(mstream);

                var scale = _scale;
                if (scale == float.MaxValue)
                {
                    scale = (float)_maxSize / Math.Max(_maxSize, Math.Max(raw_bitmap.Width, raw_bitmap.Height));
                }

                var bitmap = new Bitmap(raw_bitmap, new System.Drawing.Size((int)(raw_bitmap.Width * scale), (int)(raw_bitmap.Height * scale)));

                var image = FaceRecognition.LoadImage(bitmap);
                var locations = faceRecognition.FaceLocations(image).ToArray();
                var landmarks = faceRecognition.FaceLandmark(image, locations).ToArray();

                faces = new List<Face>();

                for (int i = 0; i < locations.Length; i++)
                {
                    var loc = locations[i];

                    var marks = landmarks[i];
                    var new_marks = new Dictionary<FacePart, IEnumerable<FacePoint>>();
                    foreach(var key in marks.Keys)
                    {
                        var point_list = new List<FacePoint>();
                        foreach(var point in marks[key])
                        {
                            point_list.Add(new FacePoint(
                                new FaceRecognitionDotNet.Point((int)(point.Point.X / scale), (int)(point.Point.Y / scale)),
                                point.Index
                            ));
                        }
                        new_marks.Add(key, point_list);
                    }

                    faces.Add(new Face
                    {
                        Landmarks = new_marks,
                        BoundingBox = new RectangleF((float)loc.Left / scale, (float)loc.Top / scale, 
                                                     (float)(loc.Right - loc.Left) / scale, (float)(loc.Bottom - loc.Top) / scale)
                    });
                }

                image.Dispose();
                bitmap.Dispose();
                raw_bitmap.Dispose();
                mstream.Dispose();

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
