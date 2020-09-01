using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;

namespace holotrack.Graphics.UserInterface
{
    public class FramedButton : Button
    {
        private Colour4 frameColor;
        public virtual Colour4 FrameColor
        {
            get => frameColor;
            set
            {
                if (frameColor == value)
                    return;

                frameColor = value;
                BorderColour = frameColor;

                if (badge != null)
                    badge.Colour = frameColor;
            }
        }

        private SpriteIcon icon;
        public IconUsage Icon
        {
            get => icon.Icon;
            set
            {
                icon.Icon = value;
                badge.Alpha = 1;
            }
        }

        private Box badge;
        private readonly Container content;
        protected override Container<Drawable> Content => content;

        public FramedButton()
        {
            Height = 75;
            Masking = true;
            FrameColor = HoloTrackColor.Notice;
            CornerRadius = 5;
            BorderThickness = 5;
            RelativeSizeAxes = Axes.X;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Transparent
                },
                content = new Container { RelativeSizeAxes = Axes.Both },
                new Container
                {
                    Size = new Vector2(25),
                    Margin = new MarginPadding(0.75f),
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Child = new Container
                    {
                        Masking = true,
                        CornerRadius = 5,
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            badge = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Colour = FrameColor,
                                Alpha = 0,
                            },
                            icon = new SpriteIcon
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Colour = Colour4.Black,
                                Size = new Vector2(8),
                            },
                        } 
                    }
                }
            };

            Enabled.ValueChanged += enableChanged;
            Enabled.TriggerChange();
        }

        private void enableChanged(ValueChangedEvent<bool> e)
        {
            this.FadeColour(e.NewValue ? Colour4.White : Colour4.Gray, 200);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (Enabled.Value)
                this.FadeColour(Colour4.White.Darken(0.25f), 100);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            if (Enabled.Value)
                this.FadeColour(Colour4.White, 100);
        }
    }
}