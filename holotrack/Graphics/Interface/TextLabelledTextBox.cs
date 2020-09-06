using holotrack.Graphics.Sprites;
using osu.Framework.Graphics;

namespace holotrack.Graphics.Interface
{
    public abstract class TextLabelledTextBox : LabelledTextBox
    {
        private HoloTrackSpriteText label;
        public string Label
        {
            get => label.Text;
            set => label.Text = value;
        }

        protected override Drawable CreateLabel() => label = new HoloTrackSpriteText
        {
            Font = HoloTrackFont.Bold.With(size: 14),
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Margin = new MarginPadding { Horizontal = 8 },
        };
    }
}