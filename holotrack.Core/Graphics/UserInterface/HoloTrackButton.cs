using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;

namespace holotrack.Core.Graphics.UserInterface
{
    public class HoloTrackButton : Button
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

        private Colour4 backgroundColour;
        public Colour4 BackgroundColor
        {
            get => backgroundColour;
            set
            {
                if (backgroundColour != value)
                    backgroundColour = value;

                if (background != null)
                    background.Colour = backgroundColour;
            }
        }

        private readonly Box background;
        private readonly Box hover;
        private readonly SpriteText label;
        protected override Container<Drawable> Content { get; }

        public HoloTrackButton()
        {
            Height = 30;
            Masking = true;
            CornerRadius = 3;
            RelativeSizeAxes = Axes.X;
            BackgroundColor = HoloTrackColor.ControlBorder;

            AddInternal(Content = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = BackgroundColor,
                        RelativeSizeAxes = Axes.Both,
                    },
                    hover = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Black,
                        Alpha = 0,
                        Depth = -1,
                    },
                    label = new SpriteText
                    {
                        Depth = -2,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = HoloTrackFont.Control,
                    }
                }
            });

            Enabled.ValueChanged += enableChanged;
            Enabled.TriggerChange();
        }

        private void enableChanged(ValueChangedEvent<bool> e)
        {
            this.FadeColour(e.NewValue ? Colour4.White : Colour4.Gray, 200);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (Enabled.Value)
                hover.FadeTo(0.25f, 100);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (Enabled.Value)
                hover.FadeOut(100);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (Enabled.Value)
                background.FadeColour(BackgroundColor.Darken(0.25f), 100);

            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            base.OnMouseUp(e);

            if (Enabled.Value)
                background.FadeColour(BackgroundColor, 100);
        }
    }
}