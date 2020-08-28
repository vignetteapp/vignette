using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace holotrack.Core.Graphics.UserInterface
{
    public class HoloTrackCheckbox : Checkbox
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
        private SpriteIcon check;

        public HoloTrackCheckbox()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            AddRange(new Drawable[]
            {
                new Container
                {
                    Size = new Vector2(20),
                    Masking = true,
                    CornerRadius = 3,
                    BorderColour = HoloTrackColor.ControlBorder,
                    BorderThickness = 3,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Transparent,
                        },
                        check = new SpriteIcon
                        {
                            Size = new Vector2(0.4f),
                            Icon = FontAwesome.Solid.Check,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                },
                label = new SpriteText
                {
                    Margin = new MarginPadding { Left = 30 },
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Font = HoloTrackFont.Control,
                }
            });

            Current.ValueChanged += c => check.Alpha = c.NewValue ? 1 : 0;
            Current.TriggerChange();
        }
    }
}