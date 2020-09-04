using holotrack.Graphics.Sprites;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace holotrack.Graphics.Interface
{
    public class HoloTrackTextBox : BasicTextBox
    {
        public HoloTrackTextBox()
        {
            Height = 25;
            Masking = true;
            CornerRadius = 5;

            BackgroundUnfocused = HoloTrackColor.Darkest.Opacity(0.5f);
            BackgroundFocused = HoloTrackColor.Darkest;
        }

        protected override SpriteText CreatePlaceholder() => new HoloTrackSpriteText { Alpha = 0.5f };
    }
}