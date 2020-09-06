using holotrack.Graphics.Interface;
using osu.Framework.Graphics;

namespace holotrack.Overlays.Settings
{
    public class SettingsTextBox : SettingsItem<string>
    {
        protected override Drawable CreateControl() => new HoloTrackTextBox
        {
            RelativeSizeAxes = Axes.X,
            CommitOnFocusLost = true,
        };
    }
}