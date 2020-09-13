using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;

namespace holotrack.Tracking
{
    /// <summary>
    /// A component responsible for processing image data for Facial Recognition
    /// </summary>
    public abstract class FaceTracker : Component
    {
        protected CameraSprite Camera { get; private set; }
        
        private Task trackerTask;
        private readonly CancellationTokenSource trackerCancellationSource = new CancellationTokenSource();

        private List<Face> faces = new List<Face>();

        /// <summary>
        /// A collection of tracked faces
        /// </summary>
        public IReadOnlyList<Face> Faces => faces.ToArray();

        /// <summary>
        /// The number of faces being tracked
        /// </summary>
        public int Tracked => faces.Count;

        /// <summary>
        /// A check if there is at least one face is being tracked
        /// </summary>
        public bool IsTracking => faces.Count > 0;

        private double lastTrack;

        /// <summary>
        /// Time between <see cref="UpdateState"/> calls in milliseconds
        /// </summary>
        public double UpdateDelta { get; private set; }

        public virtual void StartTracking(CameraSprite camera) => Camera = camera;
        public virtual void StopTracking() => Camera = null;

        /// <summary>
        /// Face Tracking update loop. This loop starts after <see cref="LoadComplete()"/>
        /// </summary>
        protected abstract void UpdateState(List<Face> faces);

        protected override void LoadComplete()
        {
            base.LoadComplete();
            trackerTask = Task.Factory.StartNew(() => trackerLoop(trackerCancellationSource.Token), trackerCancellationSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void trackerLoop(CancellationToken cancellationToken)
        {
            while(true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (Camera?.CaptureData == null)
                    continue;
                    
                    
                var faceList = new List<Face>();
                UpdateState(faceList);
                faces = faceList;

                UpdateDelta = Time.Current - lastTrack;
                lastTrack = Time.Current;
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
    }
}