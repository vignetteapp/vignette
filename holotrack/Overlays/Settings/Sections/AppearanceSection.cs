using holotrack.Graphics.Interface;
using holotrack.Overlays.Settings.Sections.Appearance;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osuTK;

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