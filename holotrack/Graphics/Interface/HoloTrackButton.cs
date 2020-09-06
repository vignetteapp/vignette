using holotrack.Graphics.Sprites;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;

namespace holotrack.Graphics.Interface
{
    public class HoloTrackButton : Button
    {
        private Box flash;
        private Box background;
        private HoloTrackSpriteText label;

        public string Text
        {
            get => label.Text;
            set => label.Text = value;
        }

        public Colour4 BackgroundColor
        {
            get => background.Colour;
            set => background.Colour = value;
        }

        public HoloTrackButton()
        {
            Height = 25;
            Masking = true;
            CornerRadius = 5;

            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = HoloTrackColor.Light,
                },
                label = new HoloTrackSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
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