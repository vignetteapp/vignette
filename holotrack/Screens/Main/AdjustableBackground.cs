using holotrack.Configuration;
using holotrack.IO;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace holotrack.Screens.Main
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
        private void load(HoloTrackConfigManager config)
        {
            userScale = config.GetBindable<float>(HoloTrackSetting.BackgroundScale);
            userXOffset = config.GetBindable<float>(HoloTrackSetting.BackgroundPositionX);
            userYOffset = config.GetBindable<float>(HoloTrackSetting.BackgroundPositionY);

            backgroundMode = config.GetBindable<BackgroundMode>(HoloTrackSetting.BackgroundMode);
            userTexture = config.GetBindable<string>(HoloTrackSetting.BackgroundImage);
            userColor = config.GetBindable<Colour4>(HoloTrackSetting.BackgroundColor);

            userScale.ValueChanged += _ => updateTranslation();
            userXOffset.ValueChanged += _ => updateTranslation();
            userYOffset.ValueChanged += _ => updateTranslation();

            backgroundMode.ValueChanged += _ => updateTexture();
            userTexture.ValueChanged += _ => updateTexture();
            userColor.ValueChanged += _ => updateTexture();

            updateTranslation();
            updateTexture();
        }

        private void updateTranslation()
        {
            sprite.Position = new Vector2(userXOffset.Value, userYOffset.Value);
            sprite.Size = new Vector2(userScale.Value);
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