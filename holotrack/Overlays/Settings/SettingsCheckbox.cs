using holotrack.Graphics.Interface;
using osu.Framework.Graphics;

namespace holotrack.Overlays.Settings
{
    public class SettingsCheckbox : SettingsItem<bool>
    {
        private HoloTrackCheckbox checkbox;
        public override string Label
        {
            get => checkbox.Text;
            set => checkbox.Text = value;
        }

        protected override Drawable CreateControl() => checkbox = new HoloTrackCheckbox();
    }
}