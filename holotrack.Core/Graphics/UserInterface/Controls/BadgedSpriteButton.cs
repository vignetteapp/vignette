using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace holotrack.Core.Graphics.UserInterface.Control
{
    public class BadgedSpriteButton : SpriteButton
    {
        private SpriteText badgeLabel;

        public string BadgeText
        {
            get => badgeLabel?.Text;
            set
            {
                if (badgeLabel != null)
                    badgeLabel.Text = value;
            }
        }

        public BadgedSpriteButton()
        {
            Add(new Container
            {
                Size = new Vector2(25),
                Margin = new MarginPadding(0.75f),   // Needed to avoid that weird overdraw
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                Child = new Container
                {
                    Masking = true,
                    CornerRadius = 5,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = Colour4.Yellow,
                        },
                        badgeLabel = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = Colour4.Black,
                            Font = FontUsage.Default.With(size: 12),
                        },
                    } 
                }
            });
        }
    }
}