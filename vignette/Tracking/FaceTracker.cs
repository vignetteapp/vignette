using FaceRecognitionDotNet;
using FaceRecognitionDotNet.Extensions;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace vignette.Tracking
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

        // head pose estimation files here
        private string yawModelFileName;
        private string pitchModelFileName;
        private string rollModelFileName;

        /// <summary>
        /// A collection of tracked faces
        /// </summary>
        public IReadOnlyList<Face> Faces => faces;

        /// <summary>
        /// The number of faces being tracked
        /// </summary>
        public int Tracked => Faces?.Count ?? 0;

        public bool IsTracking => Tracked > 0;

        float _detectionScale = float.MaxValue;
        /// <summary>
        /// The scale factor of face detection. If this value is float.MaxValue, scale factor is automatically decide by maximum pixel size (MaxDetectionSize)
        /// </summary>
        public float FaceDetectionScale
        {
            get => _detectionScale;
            set => _detectionScale = value;
        }

        float _landmarkScale = float.MaxValue;
        /// <summary>
        /// The scale factor of face landmark tracking. If this value is float.MaxValue, scale factor is automatically decide by maximum pixel size (MaxLandmarkSize)
        /// </summary>
        public float FaceLandmarkScale
        {
            get => _landmarkScale;
            set => _landmarkScale = value;
        }

        int _maxDetectionSize = 180;
        /// <summary>
        /// Maximum image size for detection, this value is applied first. Increasing this value to find more smaller face in far place. But it may loss performance.
        /// </summary>
        public int MaxDetectionSize
        {
            get => _maxDetectionSize;
            set => _maxDetectionSize = value;
        }

        int _maxLandmarkSize = 520;
        /// <summary>
        /// Maximum image size for detection, this value is applied first.
        /// </summary>
        public int MaxLandmarkSize
        {
            get => _maxLandmarkSize;
            set => _maxLandmarkSize = value;
        }

        public event Action<IReadOnlyList<Face>> OnTrackerUpdate;


        [BackgroundDependencyLoader]
        private void load(FaceRecognition faceRecognition)
        {
            this.faceRecognition = faceRecognition;
            //TODO <sr229>: I made some stub references here. Change this to a proper assembly location. 
            this.faceRecognition.CustomHeadPoseEstimator = new SimpleHeadPoseEstimator(rollModelFileName, pitchModelFileName, yawModelFileName);
            this.faceRecognition.CustomEyeBlinkDetector = new EyeAspectRatioLargeEyeBlinkDetector(0.2, 0.2);

            trackerTask = Task.Factory.StartNew(() => trackerLoop(trackerCancellationSource.Token), trackerCancellationSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void StartTracking(CameraSprite camera) => this.camera = camera;

        public void StopTracking() => camera = null;

        public TimeSpan TimeCopy = TimeSpan.MinValue;
        public TimeSpan TimeDetect = TimeSpan.MinValue;
        public TimeSpan TimeLandmark = TimeSpan.MinValue;
        public TimeSpan TimeHeadPose = TimeSpan.MinValue;
        public TimeSpan TimePost = TimeSpan.MinValue;


        private void trackerLoop(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (camera?.CaptureData == null)
                    continue;

                // init stopwatch
#if DEBUG
                var stopwatch = new Stopwatch();
                stopwatch.Start();
#endif

                // copy buffers
#if DEBUG
                TimeCopy = stopwatch.Elapsed;
#endif
                var mstream = new MemoryStream(camera.CaptureData);
                var raw_bitmap = new Bitmap(mstream);

                var detectionScale = _detectionScale == float.MaxValue ?
                    (float)_maxDetectionSize / Math.Max(_maxDetectionSize, Math.Max(raw_bitmap.Width, raw_bitmap.Height)) : _detectionScale;
                var landmarkScale = _landmarkScale == float.MaxValue ?
                    (float)_maxLandmarkSize / Math.Max(_maxLandmarkSize, Math.Max(raw_bitmap.Width, raw_bitmap.Height)) : _landmarkScale;

                var detectionBitmap = new Bitmap(raw_bitmap,
                    new System.Drawing.Size((int)(raw_bitmap.Width * detectionScale), (int)(raw_bitmap.Height * detectionScale)));
                var landmarkBitmap = new Bitmap(raw_bitmap,
                    new System.Drawing.Size((int)(raw_bitmap.Width * landmarkScale), (int)(raw_bitmap.Height * landmarkScale)));

                var detectionImage = FaceRecognition.LoadImage(detectionBitmap);
                var landmarkImage = FaceRecognition.LoadImage(landmarkBitmap);
#if DEBUG
                TimeCopy = stopwatch.Elapsed - TimeCopy;
#endif

                // face detection
#if DEBUG
                TimeDetect = stopwatch.Elapsed;
#endif
                var locations = faceRecognition.FaceLocations(detectionImage).ToArray();
#if DEBUG
                TimeDetect = stopwatch.Elapsed - TimeDetect;
#endif

                // face landmark tracking
#if DEBUG
                TimeLandmark = stopwatch.Elapsed;
#endif
                for (int i = 0; i < locations.Length; i++)
                {
                    var item = locations[i];
                    locations[i] = new Location(
                        (int)((float)item.Left / detectionScale * landmarkScale),
                        (int)((float)item.Top / detectionScale * landmarkScale),
                        (int)((float)item.Right / detectionScale * landmarkScale),
                        (int)((float)item.Bottom / detectionScale * landmarkScale)
                        );
                }
                var landmarks = faceRecognition.FaceLandmark(landmarkImage, locations).ToArray();
#if DEBUG
                TimeLandmark = stopwatch.Elapsed - TimeLandmark;
#endif

                // post processing
#if DEBUG
                TimePost = stopwatch.Elapsed;
#endif
                var scale = landmarkScale;
                faces = new List<Face>();

                for (int i = 0; i < locations.Length; i++)
                {
                    var loc = locations[i];

                    var marks = landmarks[i];
                    var new_marks = new Dictionary<FacePart, IEnumerable<FacePoint>>();
                    foreach (var key in marks.Keys)
                    {
                        var point_list = new List<FacePoint>();
                        foreach (var point in marks[key])
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
                        Pose = faceRecognition.PredictHeadPose(new_marks),
                        Landmarks = new_marks,
                        BoundingBox = new RectangleF((float)loc.Left / scale, (float)loc.Top / scale,
                                                     (float)(loc.Right - loc.Left) / scale, (float)(loc.Bottom - loc.Top) / scale)
                    });
                }

                landmarkImage.Dispose();
                detectionImage.Dispose();

                landmarkBitmap.Dispose();
                detectionBitmap.Dispose();

                raw_bitmap.Dispose();
                mstream.Dispose();
#if DEBUG
                TimePost = stopwatch.Elapsed - TimePost;
                TimeHeadPose = stopwatch.Elapsed - TimeHeadPose;
#endif

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
            public HeadPose Pose;
        }
    }
}