using holotrack.Screens.Main;
using osu.Framework.Graphics;

namespace holotrack.Tests.Visual.UserInterface
{
    public class TestSceneCameraControl : TestSceneUserInterface
    {
        public TestSceneCameraControl()
        {
            Elements.Add(new CameraControl
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
            });
        }
    }
}