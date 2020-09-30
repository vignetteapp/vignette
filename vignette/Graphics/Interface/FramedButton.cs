using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;

namespace vignette.Graphics.Interface
{
    public class FramedButton : Button
    {
        private Box flash;
        private readonly Box badgeBox;
        private readonly Sprite icon;
        private readonly Container badge;

        public Texture Icon
        {
            get => icon.Texture;
            set => icon.Texture = value;
        }

        private Colour4 frameColor;
        public Colour4 FrameColor
        {
            get => frameColor;
            set
            {
                frameColor = value;
                badgeBox.Colour = value;
                BorderColour = value;
            }
        }

        private bool showBadge;
        public bool ShowBadge
        {
            get => showBadge;
            set
            {
                showBadge = value;
                badge.Alpha = showBadge ? 1 : 0;
            }
        }

        private readonly Container<Drawable> content;
        protected override Container<Drawable> Content => content;

        public FramedButton()
        {
            Masking = true;
            CornerRadius = 5;
            BorderColour = Colour4.White;
            BorderThickness = 5;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Transparent,
                },
                content = new Container<Drawable>
                {
                    RelativeSizeAxes = Axes.Both,
                },
                badge = new Container
                {
                    Size = new Vector2(25),
                    Alpha = 0,
                    Margin = new MarginPadding(1),
                    Masking = true,
                    CornerRadius = 3,
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    Children = new Drawable[]
                    {
                        badgeBox = new Box
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                        icon = new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        }
                    }
                },
                flash = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                    Alpha = 0,
                }
            };

            Enabled.BindValueChanged(enableChanged, true);
        }

        private void enableChanged(ValueChangedEvent<bool> e) => this.FadeColour(e.NewValue ? Colour4.White : Colour4.Gray, 200, Easing.OutQuint);

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (Enabled.Value)
                flash.FadeTo(0.25f, 200, Easing.OutQuint);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            if (Enabled.Value)
                flash.FadeOut(200, Easing.OutQuint);
        }
    }
}