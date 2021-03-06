// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Video;
using Vignette.Application.Configuration;
using Vignette.Application.IO;

namespace Vignette.Application.Graphics.Interface
{
    public class Background : Container
    {
        private IBindable<BackgroundTypes> backgroundType;

        private IBindable<Colour4> backgroundColour;

        private IBindable<string> backgroundImage;

        private IBindable<string> backgroundVideo;

        [Resolved]
        private ObservedTextureStore textures { get; set; }

        [Resolved]
        private ObservedVideoStore videos { get; set; }

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager appConfig)
        {
            backgroundType = appConfig.GetBindable<BackgroundTypes>(ApplicationConfig.BackgroundType);
            backgroundImage = appConfig.GetBindable<string>(ApplicationConfig.BackgroundImage);
            backgroundVideo = appConfig.GetBindable<string>(ApplicationConfig.BackgroundVideo);
            backgroundColour = appConfig.GetBindable<Colour4>(ApplicationConfig.BackgroundColour);

            backgroundType.ValueChanged += _ =>updateBackground();
            backgroundImage.ValueChanged += _ => updateBackground();
            backgroundVideo.ValueChanged += _ => updateBackground();
            backgroundColour.ValueChanged += _ => updateBackground();

            updateBackground();
        }

        private void updateBackground()
        {
            if (Children.Count > 0)
            {
                switch (Child)
                {
                    case Box box:
                        if (backgroundType.Value != BackgroundTypes.Colour && box != null)
                            Remove(box);
                        break;

                    case Sprite sprite:
                        if (backgroundType.Value != BackgroundTypes.Image && sprite != null)
                            Remove(sprite);
                        break;

                    case Video video:
                        if (backgroundType.Value != BackgroundTypes.Video && video != null)
                            Remove(video);
                        break;
                }
            }

            switch (backgroundType.Value)
            {
                case BackgroundTypes.Colour:
                    Child = new Box { Colour = backgroundColour.Value };
                    break;

                case BackgroundTypes.Image:
                    Child = new Sprite { Texture = textures.Get(backgroundImage.Value) };
                    break;

                case BackgroundTypes.Video:
                    Child = videos.Get(backgroundVideo.Value);
                    break;
            }

            Child.RelativeSizeAxes = Axes.Both;
            Child.Anchor = Anchor.Centre;
            Child.Origin = Anchor.Centre;
        }
    }
}
