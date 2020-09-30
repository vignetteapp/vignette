using vignette.Overlays.Settings.Sections.Appearance;
using osu.Framework.Graphics;

namespace vignette.Overlays.Settings.Sections
{
    public class AppearanceSection : SettingsSection
    {
        public AppearanceSection()
        {
            Children = new Drawable[]
            {
                new BackgroundSettings(),
                new ModelSettings(),
            };
        }
    }
}