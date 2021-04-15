// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Threading;
using osuTK;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Backgrounds;

namespace Vignette.Game.Screens.Backgrounds
{
    public class BackgroundScreenUser : BackgroundScreen
    {
        private Bindable<BackgroundType> type;

        private Bindable<string> asset;

        private Drawable current;

        private ScheduledDelegate loadTask;

        private CancellationTokenSource cancellationTokenSource;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            type = config.GetBindable<BackgroundType>(VignetteSetting.BackgroundType);
            type.ValueChanged += e => handleVisualChange();

            asset = config.GetBindable<string>(VignetteSetting.BackgroundAsset);
            asset.ValueChanged += e => handleVisualChange();

            handleVisualChange();
        }

        private void handleVisualChange()
        {
            loadTask?.Cancel();
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            loadTask = Scheduler.AddDelayed(() => LoadComponentAsync(createBackground(), setupNextBackground, cancellationTokenSource.Token), 100);
        }

        private void setupNextBackground(Drawable background)
        {
            current?.FadeOut(200, Easing.OutQuint);
            current?.Expire();

            AddInternal(current = background);

            background.Size = new Vector2(1280, 720);
            background.Anchor = Anchor.Centre;
            background.Origin = Anchor.Centre;

            background.Alpha = 0;
            background.FadeIn(200, Easing.OutQuint);
        }

        private Drawable createBackground()
        {
            switch (type.Value)
            {
                case BackgroundType.Image:
                default:
                    return new UserBackgroundImage(asset.Value);

                case BackgroundType.Video:
                    return new UserBackgroundVideo(asset.Value);
            }
        }
    }
}
