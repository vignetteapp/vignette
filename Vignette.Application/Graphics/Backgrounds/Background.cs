// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics.Containers;

namespace Vignette.Application.Graphics.Backgrounds
{
    public class Background : Presentation<BackgroundScreen>
    {
        private Bindable<BackgroundType> type;

        private Bindable<string> video;

        private Bindable<string> image;

        private readonly bool adjust;

        public Background(bool adjust = true)
        {
            this.adjust = adjust;
        }

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager config)
        {
            type = config.GetBindable<BackgroundType>(ApplicationSetting.Background);
            type.ValueChanged += _ => updateDisplay();

            image = config.GetBindable<string>(ApplicationSetting.BackgroundImageFile);
            image.ValueChanged += _ => updateDisplay();

            video = config.GetBindable<string>(ApplicationSetting.BackgroundVideoFile);
            video.ValueChanged += _ => updateDisplay();

            updateDisplay();
        }

        private void updateDisplay()
        {
            string asset = type.Value switch
            {
                BackgroundType.Color => string.Empty,
                BackgroundType.Image => image.Value,
                BackgroundType.Video => video.Value,
                _ => null,
            };

            if (Current.Value != null)
                Remove(Current.Value);

            Add(new BackgroundScreen(type.Value, asset, adjust));
        }
    }
}
