using osu.Framework.Graphics;

namespace holotrack.Core.Graphics.UserInterface.Control
{
    public class SpriteButton : BasicButton
    {
        public SpriteButton()
        {
            Height = 75;
            Masking = true;
            CornerRadius = 5;
            BorderColour = Colour4.Yellow;
            BorderThickness = 5;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Background.Colour = Colour4.Transparent;
        }
    }
}