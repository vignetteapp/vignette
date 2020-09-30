using vignette.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace vignette.Graphics.Interface
{
    public class VignetteForm : Container
    {
        private const float TITLEBAR_HEIGHT = 64.0f;

        private SpriteText title;
        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }

        private Container titlebar;
        public bool TitleBarVisibility
        {
            get => titlebar.Alpha == 1;
            set
            {
                titlebar.Alpha = value ? 1 : 0;
                body.Padding = new MarginPadding { Top = value ? TITLEBAR_HEIGHT : 0 };
            }
        }

        private Container body;
        protected BufferedContainer Background;

        private Container<Drawable> content;
        protected override Container<Drawable> Content => content;

        public VignetteForm()
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
                            Colour = VignetteColor.Base,
                            RelativeSizeAxes = Axes.Both,
                        },
                        title = new VignetteSpriteText
                        {
                            Font = VignetteFont.Bold.With(size: 24),
                            Margin = new MarginPadding(20),
                        }
                    }
                },
                body = new Container
                {
                    Name = @"body",
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        Background = new BufferedContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            BlurSigma = new Vector2(20),
                        },
                        new Box
                        {
                            Colour = VignetteColor.Base,
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.9f,
                        },
                        content = new Container<Drawable>
                        {
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader(true)]
        private void load(VignetteGame game)
        {
            if (game != null)
                Background.Child = game.CreateView();
        }
    }
}