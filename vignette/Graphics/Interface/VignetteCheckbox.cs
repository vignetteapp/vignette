using vignette.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace vignette.Graphics.Interface
{
    public class VignetteCheckbox : Checkbox
    {
        private readonly Sprite checkmark;
        private readonly VignetteSpriteText label;
        public string Text
        {
            get => label.Text;
            set => label.Text = value;
        }

        public VignetteCheckbox()
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
                            Colour = VignetteColor.Darkest,
                        },
                        checkmark = new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0,
                        }
                    }
                },
                label = new VignetteSpriteText
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