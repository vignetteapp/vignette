using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace holotrack.Graphics.UserInterface
{
    public class KeybindButton : FillFlowContainer
    {
        private SpriteText label;
        public string Text
        {
            get => label?.Text;
            set
            {
                if (label != null)
                    label.Text = value;
            }
        }

        private HoloTrackButton button;
        public string ButtonText
        {
            get => button?.Text;
            set
            {
                if (button != null)
                    button.Text = value;
            }
        }

        public Action Action
        {
            get => button?.Action;
            set
            {
                if (button != null)
                    button.Action = value;
            }
        }

        public KeybindButton()
        {
            Direction = FillDirection.Horizontal;
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
            Spacing = new Vector2(10, 0);

            AddRange(new Drawable[]
            {
                button = new HoloTrackButton
                {
                    RelativeSizeAxes = Axes.X,
                    Width = 0.4f,
                },
                label = new SpriteText
                {
                    Font = HoloTrackFont.Control,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                }
            });
        }
    }
}