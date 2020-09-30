using vignette.Graphics.Interface;
using osu.Framework.Graphics;

namespace vignette.Overlays.Settings
{
    public class SettingsCheckbox : SettingsItem<bool>
    {
        private VignetteCheckbox checkbox;
        public override string Label
        {
            get => checkbox.Text;
            set => checkbox.Text = value;
        }

        protected override Drawable CreateControl() => checkbox = new VignetteCheckbox();
    }
}