using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;

namespace holotrack.Core.Graphics.UserInterface
{
    public class HoloTrackPanel : Container
    {
        private readonly Box background;
        public Colour4 BackgroundColor
        {
            get => background.Colour;
            set => background.FadeColour(value);
        }

        private readonly Container content;
        protected override Container<Drawable> Content => content;

        private MarginPadding contentPadding;
        public MarginPadding ContentPadding
        {
            get => contentPadding;
            set
            {
                contentPadding = value;

                if (content != null)
                    content.Padding = contentPadding;
            }
        }

        public HoloTrackPanel()
        {
            Masking = true;
            CornerRadius = 3;

            EdgeEffect = new EdgeEffectParameters
            {
                Colour = Colour4.Black.Opacity(0.4f),
                Hollow = true,
                Radius = 12,
                Type = EdgeEffectType.Shadow,
            };

            InternalChildren = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = HoloTrackColor.ControlBackground
                },
                content = new Container
                {
                    Padding = contentPadding,
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }
    }
}