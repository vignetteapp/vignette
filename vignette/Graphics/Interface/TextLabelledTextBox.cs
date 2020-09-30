using vignette.Graphics.Sprites;
using osu.Framework.Graphics;

namespace vignette.Graphics.Interface
{
    public abstract class TextLabelledTextBox : LabelledTextBox
    {
        private VignetteSpriteText label;
        public string Label
        {
            get => label.Text;
            set => label.Text = value;
        }

        protected override Drawable CreateLabel() => label = new VignetteSpriteText
        {
            Font = VignetteFont.Bold.With(size: 14),
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Margin = new MarginPadding { Horizontal = 8 },
        };
    }
}