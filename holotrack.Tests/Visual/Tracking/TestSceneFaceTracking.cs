using System.Linq;
using holotrack.Tracking;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;
using osuTK;

namespace holotrack.Tests.Visual.Tracking
{
    public class TestSceneFaceTracking : TestScene
    {
        private readonly SpriteText status;
        private readonly FaceTracker tracker;
        private readonly Container<TrackingBox> faceLocationsContainer;
        private readonly CameraSprite camera;

        public TestSceneFaceTracking()
        {
            Children = new Drawable[]
            {
                tracker = new FaceRecognitionDotNetFaceTracker(),
                camera = new CameraSprite
                {
                    CameraID = 0,
                },
                faceLocationsContainer = new Container<TrackingBox>
                {
                    Name = @"face locations",
                    Size = new Vector2(640, 480),
                },
                new Container
                {
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Colour4.Black,
                            RelativeSizeAxes = Axes.Both,
                        },
                        status = new SpriteText
                        {
                            AlwaysPresent = true,
                            Margin = new MarginPadding(5)
                        },
                    }
                }
            };

            tracker.StartTracking(camera);
        }

        protected override void Update()
        {
            if (!status.IsLoaded && !tracker.IsLoaded)
                return;

            status.Text = $"Faces: {tracker.Tracked} | IsTracking: {tracker.IsTracking} | Delta: {(tracker.UpdateDelta / 1000).ToString("0.0000")} sec(s)";

            while (faceLocationsContainer.Children.Count < tracker.Tracked)
                faceLocationsContainer.Add(new TrackingBox());

            while (faceLocationsContainer.Children.Count > tracker.Tracked)
                faceLocationsContainer.Remove(faceLocationsContainer.First());

            faceLocationsContainer.ForEach(box =>
            {
                var index = faceLocationsContainer.IndexOf(box);
                var face = tracker.Faces[index];

                box.MoveTo(face.Bounds.Location, box.FirstTrack ? 0 : 200, Easing.OutQuint);
                box.ResizeTo(face.Bounds.Size, box.FirstTrack ? 0 : 200, Easing.OutQuint);

                box.FirstTrack = false;
            });
        }

        private class TrackingBox : Container
        {
            public bool FirstTrack = true;

            public TrackingBox()
            {
                Masking = true;
                BorderColour = Colour4.Red;
                BorderThickness = 3;
                Child = new Box
                {
                    Colour = Colour4.Transparent,
                    RelativeSizeAxes = Axes.Both,
                };
            }
        }
    }
}