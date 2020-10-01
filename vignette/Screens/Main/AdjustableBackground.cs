using vignette.Configuration;
using vignette.IO;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace vignette.Screens.Main
{
    public class AdjustableBackground : Container
    {
        private readonly Sprite sprite;
        private Bindable<float> userScale;
        private Bindable<float> userXOffset;
        private Bindable<float> userYOffset;
        private Bindable<BackgroundMode> backgroundMode;
        private Bindable<string> userTexture;
        private Bindable<Colour4> userColor;

        [Resolved]
        private FileStore files { get; set; }

        [Resolved]
        private TextureStore textures { get; set; }

        public AdjustableBackground()
        {
            RelativeSizeAxes = Axes.Both;
            Child = sprite = new Sprite
            {
                FillMode = FillMode.Fill,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
            };
        }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            userScale = config.GetBindable<float>(VignetteSetting.BackgroundScale);
            userXOffset = config.GetBindable<float>(VignetteSetting.BackgroundPositionX);
            userYOffset = config.GetBindable<float>(VignetteSetting.BackgroundPositionY);

            backgroundMode = config.GetBindable<BackgroundMode>(VignetteSetting.BackgroundMode);
            userTexture = config.GetBindable<string>(VignetteSetting.BackgroundImage);
            userColor = config.GetBindable<Colour4>(VignetteSetting.BackgroundColor);

            userScale.ValueChanged += _ => updateTranslation();
            userXOffset.ValueChanged += _ => updateTranslation();
            userYOffset.ValueChanged += _ => updateTranslation();

            backgroundMode.ValueChanged += _ => updateTexture();
            userColor.ValueChanged += _ => updateTexture();
            userTexture.ValueChanged += v => updateMode(v.NewValue);

            updateTranslation();
            updateTexture();
        }

        private void updateTranslation()
        {
            sprite.Position = new Vector2(userXOffset.Value, userYOffset.Value);
            sprite.Size = new Vector2(userScale.Value);
        }

        private void updateMode(string newMode)
        {
            backgroundMode.Value = newMode == "Color" ? BackgroundMode.Color : BackgroundMode.Image;
            updateTexture();
        }

        private void updateTexture()
        {
            switch (backgroundMode.Value)
            {
                case BackgroundMode.Color:
                    sprite.Texture = Texture.WhitePixel;
                    sprite.Colour = userColor.Value;
                    break;

                case BackgroundMode.Image:
                    sprite.Texture = textures.Get(files.Retrieve(userTexture.Value));
                    sprite.Colour = Colour4.White;
                    break;
            }
        }
    }
}