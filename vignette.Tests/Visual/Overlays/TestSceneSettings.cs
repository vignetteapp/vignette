using vignette.Overlays.Settings;
using osu.Framework.Testing;

namespace vignette.Tests.Visual.Overlays
{
    public class TestSceneSettings : TestScene
    {
        public TestSceneSettings()
        {
            SettingsOverlay settings;
            Add(settings = new SettingsOverlay());

            AddStep("toggle", () => settings.ToggleVisibility());
        }
    }
}