using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using static osu.Framework.Graphics.UserInterface.BasicTextBox;

namespace holotrack.Core.Graphics.UserInterface.Control
{
    public class BasicInputBox : TextBox
    {
        protected override float LeftRightPadding => 10;

        public BasicInputBox()
        {
            Height = 40;
            BorderColour = Colour4.FromHex("7d7d7d");
            BorderThickness = 5;

            TextContainer.Height = 0.4f;

            Add(new Container
            {
                Depth = 1,
                RelativeSizeAxes = Axes.Both,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Colour4.FromHex("362f2d"),
                }
            });
        }

        protected override Caret CreateCaret() => new BasicCaret
        {
            CaretWidth = 2,
            SelectionColour = Colour4.White,
        };

        protected override SpriteText CreatePlaceholder() => new SpriteText
        {
            Colour = Colour4.White.Opacity(0.1f),
            Anchor = Anchor.CentreLeft,
            Origin = Anchor.CentreLeft,
            X = 2
        };

        protected override void NotifyInputError()
        {
        }
    }
}