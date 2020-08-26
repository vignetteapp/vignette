using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace holotrack.Core.Graphics.UserInterface.Control
{
    public class BasicButton : Button
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

        protected Box Background;
        private SpriteText label;
        protected override Container<Drawable> Content { get; }

        public BasicButton()
        {
            Height = 40;
            RelativeSizeAxes = Axes.X;

            AddInternal(Content = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    Background = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Colour4.FromHex("7d7d7d"),
                        RelativeSizeAxes = Axes.Both
                    },
                    label = new SpriteText
                    {
                        Depth = -1,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = FontUsage.Default.With(size: 16)
                    }
                }
            });
        }
    }
}