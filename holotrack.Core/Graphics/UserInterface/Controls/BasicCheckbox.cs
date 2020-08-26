using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace holotrack.Core.Graphics.UserInterface.Control
{
    public class BasicCheckbox : Checkbox
    {
        public string Text
        {
            get => label?.Text;
            set
            {
                if (label != null)
                    label.Text = value;
            }
        }

        private SpriteText label;
        private Box check;

        public BasicCheckbox()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            AddRange(new Drawable[]
            {
                new Container
                {
                    Size = new Vector2(30),
                    Margin = new MarginPadding(10),
                    Masking = true,
                    BorderColour = Colour4.FromHex("7d7d7d"),
                    BorderThickness = 4,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.FromHex("362f2d"),
                        },
                        check = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.FromHex("7d7d7d"),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                            Size = new Vector2(0.5f),
                        }
                    }
                },
                label = new SpriteText
                {
                    Margin = new MarginPadding { Left = 50 },
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                }
            });

            Current.ValueChanged += c => check.Alpha = c.NewValue ? 1 : 0;
        }
    }
}