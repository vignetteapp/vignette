using holotrack.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace holotrack.Graphics.Interface
{
    public class HoloTrackCheckbox : Checkbox
    {
        private readonly Sprite checkmark;
        private readonly HoloTrackSpriteText label;
        public string Text
        {
            get => label.Text;
            set => label.Text = value;
        }

        public HoloTrackCheckbox()
        {
            AutoSizeAxes = Axes.Both;

            Children = new Drawable[]
            {
                new Container
                {
                    Size = new Vector2(25),
                    Masking = true,
                    CornerRadius = 5,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = HoloTrackColor.Darkest,
                        },
                        checkmark = new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        }
                    }
                },
                label = new HoloTrackSpriteText
                {
                    Text = "test",
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding { Left = 30 },
                }
            };

            Current.BindValueChanged(currentChanged, true);
        }

        private void currentChanged(ValueChangedEvent<bool> e)
        {
            checkmark.Alpha = e.NewValue ? 1 : 0;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            checkmark.Texture = textures.Get("checkmark");
        }
    }
}