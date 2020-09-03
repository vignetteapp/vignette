using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace holotrack.Graphics.Interface
{
    public class HoloTrackForm : Container
    {
        private const float TITLEBAR_HEIGHT = 64.0f;

        private readonly SpriteText title;
        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }

        private readonly Container titlebar;
        public bool TitleBarVisible
        {
            get => titlebar.Alpha == 1;
            set => titlebar.Alpha = value ? 1 : 0;
        }

        private readonly Container<Drawable> content;
        protected override Container<Drawable> Content => content;

        public HoloTrackForm()
        {
            InternalChildren = new Drawable[]
            {
                titlebar = new Container
                {
                    Name = @"titlebar",
                    Height = TITLEBAR_HEIGHT,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = HoloTrackColor.Base,
                            RelativeSizeAxes = Axes.Both,
                        },
                        title = new SpriteText
                        {
                            Font = HoloTrackFont.Bold.With(size: 24),
                            Margin = new MarginPadding(20),
                        }
                    }
                },
                new Container
                {
                    Name = @"body",
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Top = TITLEBAR_HEIGHT },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = HoloTrackColor.Base,
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.9f,
                        },
                        content = new Container<Drawable>
                        {
                            Padding = new MarginPadding(15),
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                }
            };
        }
    }
}