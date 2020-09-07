using holotrack.Overlays.SidePanel;
using osu.Framework.Testing;

namespace holotrack.Tests.Visual.Interface
{
    public class TestSceneCameraDisplay : TestScene
    {
        public TestSceneCameraDisplay()
        {
            Add(new CameraDisplay());
        }
    }
}