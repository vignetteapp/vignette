using vignette.Graphics.Interface;
using osu.Framework.Graphics;

namespace vignette.Overlays.Settings
{
    public class SettingsTextBox : SettingsItem<string>
    {
        protected override Drawable CreateControl() => new VignetteTextBox
        {
            RelativeSizeAxes = Axes.X,
            CommitOnFocusLost = true,
        };
    }
}