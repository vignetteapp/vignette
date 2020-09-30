using vignette.Overlays.SidePanel;
using osu.Framework.Testing;

namespace vignette.Tests.Visual.Overlays
{
    public class TestSceneSidePanel : TestScene
    {
        public TestSceneSidePanel()
        {
            SidePanelOverlay sidePanel;
            Add(sidePanel = new SidePanelOverlay());

            AddStep("toggle", () => sidePanel.ToggleVisibility());
        }
    }
}