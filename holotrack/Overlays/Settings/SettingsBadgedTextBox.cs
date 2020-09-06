using holotrack.Graphics.Interface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace holotrack.Overlays.Settings
{
    public class SettingsBadgedTextBox : SettingsItem<string>
    {
        protected override Drawable CreateControl() => new TextLabelledBasicTextBox
        {
            RelativeSizeAxes = Axes.X,
            CommitOnFocusLost = true,
        };

        public string BadgeText
        {
            get => ((TextLabelledTextBox)Control).Label;
            set => ((TextLabelledTextBox)Control).Label = value;
        }

        public string Text
        {
            get => ((TextLabelledTextBox)Control).Text;
            set => ((TextLabelledTextBox)Control).Text = value;
        }

        public string PlaceholderText
        {
            get => ((TextLabelledTextBox)Control).PlaceholderText;
            set => ((TextLabelledTextBox)Control).PlaceholderText = value;
        }

        private class TextLabelledBasicTextBox : TextLabelledTextBox
        {
            protected override TextBox CreateTextBox() => new HoloTrackTextBox
            {
                RelativeSizeAxes = Axes.X,
            };
        }
    }
}