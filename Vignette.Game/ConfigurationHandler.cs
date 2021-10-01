// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Drawing;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using Vignette.Camera.Platform;
using Vignette.Game.Configuration;

namespace Vignette.Game
{
    public class ConfigurationHandler : Component
    {
        private Bindable<bool> resizable;
        private Bindable<Size> windowSize;
        private Bindable<WindowMode> windowMode;

        private Bindable<Size> frameworkResolution;
        private Bindable<Size> gameResolution;
        private readonly BindableSize resolution = new BindableSize();

        private Bindable<bool> isMuted;
        private readonly BindableDouble muteAdjustment = new BindableDouble();

        private Bindable<string> cameraDevice;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager gameConfig, FrameworkConfigManager frameworkConfig, AudioManager audio, CameraManager camera, GameHost host)
        {
            // We cannot disable the bindable obtained from framework config as it is internally modified.
            windowSize = frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize);
            windowMode = frameworkConfig.GetBindable<WindowMode>(FrameworkSetting.WindowMode);

            frameworkResolution = frameworkConfig.GetBindable<Size>(FrameworkSetting.SizeFullscreen);
            gameResolution = gameConfig.GetBindable<Size>(VignetteSetting.WindowSize);

            resizable = gameConfig.GetBindable<bool>(VignetteSetting.WindowResizable);
            resizable.BindValueChanged(e =>
            {
                if (host.Window is SDL2DesktopWindow window)
                    window.Resizable = e.NewValue;

                resolution.TriggerChange();
            }, true);

            resolution.BindValueChanged(e =>
            {
                if (!resizable.Value && windowMode.Value == WindowMode.Windowed)
                    windowSize.Value = e.NewValue;
            });

            windowMode.BindValueChanged(e =>
            {
                resizable.Disabled = e.NewValue != WindowMode.Windowed;

                resolution.UnbindBindings();
                resolution.BindTo(e.NewValue != WindowMode.Windowed ? frameworkResolution : gameResolution);
            }, true);

            isMuted = gameConfig.GetBindable<bool>(VignetteSetting.SoundMuted);
            isMuted.BindValueChanged(e =>
            {
                if (isMuted.Value)
                    audio.AddAdjustment(AdjustableProperty.Volume, muteAdjustment);
                else
                    audio.RemoveAdjustment(AdjustableProperty.Volume, muteAdjustment);
            }, true);

            cameraDevice = gameConfig.GetBindable<string>(VignetteSetting.CameraDevice);
            camera.Current.BindTo(cameraDevice);
        }
    }
}
