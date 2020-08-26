using holotrack.Core.Graphics.UserInterface.Control;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;

namespace holotrack.Core.Graphics.UserInterface
{
    public class CameraDisplay : BasicModal
    {
        private CameraSprite camera;

        public CameraDisplay()
        {
            Height = 150;
            RelativeSizeAxes = Axes.X;

            Add(camera = new CameraSprite
            {
                RelativeSizeAxes = Axes.Both,
            });
        }
    }
}