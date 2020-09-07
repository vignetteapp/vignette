using holotrack.Overlays.Settings.Sections.Appearance;
using osu.Framework.Graphics;

namespace holotrack.Overlays.Settings.Sections
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