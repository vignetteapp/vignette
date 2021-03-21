// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace Vignette.Application.Configuration
{
    public class ApplicationConfigManager : IniConfigManager<ApplicationSetting>
    {
        protected override string Filename => @"app.ini";

        public ApplicationConfigManager(Storage storage)
            : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            SetValue(ApplicationSetting.Theme, @"Default");
            SetValue(ApplicationSetting.WindowResizable, false);
            SetValue(ApplicationSetting.ShowFpsOverlay, false);
            SetValue(ApplicationSetting.Background, BackgroundType.Color);
            SetValue(ApplicationSetting.BackgroundColor, @"00FF00");
            SetValue(ApplicationSetting.BackgroundVideoFile, string.Empty);
            SetValue(ApplicationSetting.BackgroundImageFile, string.Empty);
            SetValue(ApplicationSetting.BackgroundOffsetX, 0.0f);
            SetValue(ApplicationSetting.BackgroundOffsetY, 0.0f);
            SetValue(ApplicationSetting.BackgroundScaleXY, 1.0f);
            SetValue(ApplicationSetting.CameraDevice, string.Empty);
        }
    }

    public enum ApplicationSetting
    {
        Theme,

        WindowResizable,

        ShowFpsOverlay,

        Background,

        BackgroundColor,

        BackgroundVideoFile,

        BackgroundImageFile,

        BackgroundOffsetX,

        BackgroundOffsetY,

        BackgroundScaleXY,

        CameraDevice,
    }
}
