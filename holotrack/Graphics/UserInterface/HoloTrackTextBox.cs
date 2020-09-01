using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using static osu.Framework.Graphics.UserInterface.BasicTextBox;

namespace holotrack.Graphics.UserInterface
{
    public class HoloTrackTextBox : TextBox
    {
        protected override float LeftRightPadding => 10;

        public HoloTrackTextBox()
        {
            Height = 40;
            CornerRadius = 3;
            BorderColour = HoloTrackColor.ControlBorder;
            BorderThickness = 3;

            TextContainer.Height = 0.5f;

            Add(new Container
            {
                Depth = 1,
                RelativeSizeAxes = Axes.Both,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Colour4.Transparent,
                }
            });
        }

        protected override Caret CreateCaret() => new BasicCaret
        {
            Scale = new Vector2(1, 1.5f),
            CaretWidth = 2,
            SelectionColour = Colour4.LightCoral,
        };

        protected override SpriteText CreatePlaceholder() => new SpriteText
        {
            Colour = Colour4.White.Opacity(0.1f),
            Anchor = Anchor.CentreLeft,
            Origin = Anchor.CentreLeft,
            Font = HoloTrackFont.Control,
            X = 2
        };

        protected override Drawable GetDrawableCharacter(char c) => new SpriteText
        {
            Text = c.ToString(),
            Font = HoloTrackFont.Control.With(size: CalculatedTextSize),
        };

        protected override void NotifyInputError()
        {
        }
    }
}