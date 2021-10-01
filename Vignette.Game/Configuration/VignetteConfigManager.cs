// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.Drawing;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Bindables;
using Vignette.Game.Screens.Stage;

namespace Vignette.Game.Configuration
{
    public class VignetteConfigManager : IniConfigManager<VignetteSetting>
    {
        protected override string Filename => "config.ini";

        public VignetteConfigManager(Storage storage, IDictionary<VignetteSetting, object> defaultOverrides = null)
            : base(storage, defaultOverrides)
        {
        }

        protected override void InitialiseDefaults()
        {
            SetDefault(VignetteSetting.WindowResizable, false);
            SetDefault(VignetteSetting.WindowSize, new Size(1366, 768));
            SetDefault(VignetteSetting.ShowFpsOverlay, false);
            SetDefault(VignetteSetting.CameraDevice, string.Empty);
            SetDefault(VignetteSetting.BackgroundPosition, Vector2.Zero);
            SetDefault(VignetteSetting.BackgroundRotation, 0.0f, 0.0f, 360.0f, 1.0f);
            SetDefault(VignetteSetting.BackgroundScale, 1.0f, 0.1f, 10.0f, 0.1f);
            SetDefault(VignetteSetting.BackgroundColour, Colour4.Green);
            SetDefault(VignetteSetting.BackgroundPath, string.Empty);
            SetDefault(VignetteSetting.BackgroundType, BackgroundType.Colour);
            SetDefault(VignetteSetting.AvatarPath, string.Empty);
            SetDefault(VignetteSetting.AvatarOffset, Vector2.Zero);
            SetDefault(VignetteSetting.AvatarScale, 1.0f, 0.1f, 10.0f, 0.1f);
            SetDefault(VignetteSetting.AvatarRotation, 0.0f, 0.0f, 360.0f, 1.0f);
            SetDefault(VignetteSetting.KeyboardEnabled, true);
            SetDefault(VignetteSetting.TrackingEnabled, true);
            SetDefault(VignetteSetting.SoundMuted, false);
            SetDefault(VignetteSetting.Theme, "Light");
        }

        protected override void AddBindable<TBindable>(VignetteSetting lookup, Bindable<TBindable> bindable)
        {
            switch (lookup)
            {
                case VignetteSetting.BackgroundColour:
                    base.AddBindable(lookup, new BindableColour4(Colour4.Green));
                    break;

                case VignetteSetting.WindowSize:
                    base.AddBindable(lookup, new BindableSize(new Size(1366, 768)));
                    break;

                case VignetteSetting.BackgroundPosition:
                    base.AddBindable(lookup, new BindableVector2());
                    break;

                case VignetteSetting.AvatarOffset:
                    base.AddBindable(lookup, new BindableVector2());
                    break;

                default:
                    base.AddBindable(lookup, bindable);
                    break;
            }
        }
    }

    public enum VignetteSetting
    {
        WindowResizable,
        WindowSize,
        ShowFpsOverlay,
        CameraDevice,
        CameraSaturation,
        CameraContrast,
        CameraGain,
        CameraHue,
        CameraFocus,
        CameraExposure,
        CameraAutoFocus,
        CameraAutoExposure,
        BackgroundPosition,
        BackgroundRotation,
        BackgroundColour,
        BackgroundScale,
        BackgroundType,
        BackgroundPath,
        AvatarPath,
        AvatarScale,
        AvatarOffset,
        AvatarRotation,
        KeyboardEnabled,
        TrackingEnabled,
        SoundMuted,
        Theme,
    }
}
