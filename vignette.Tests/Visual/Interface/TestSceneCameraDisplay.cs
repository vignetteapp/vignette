using vignette.Overlays.SidePanel;
using osu.Framework.Testing;

namespace vignette.Tests.Visual.Interface
{
    public class TestSceneCameraDisplay : TestScene
    {
        public TestSceneCameraDisplay()
        {
            Add(new CameraDisplay());
        }
    }
}