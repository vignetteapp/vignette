using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Configuration.Tracking;
using osu.Framework.Graphics;
using osu.Framework.Platform;

namespace vignette.Configuration
{
    public class VignetteConfigManager : IniConfigManager<VignetteSetting>
    {
        internal const string FILENAME = @"vignette.ini";
        protected override string Filename => FILENAME;

        public VignetteConfigManager(Storage storage, IDictionary<VignetteSetting, object> defaultOverrides = null)
            : base(storage, defaultOverrides)
        {
        }

        protected override void InitialiseDefaults()
        {
            Set(VignetteSetting.BackgroundMode, BackgroundMode.Color);
            Set(VignetteSetting.BackgroundColor, new Colour4(0.0f, 1.0f, 0.0f, 1.0f));
            Set(VignetteSetting.BackgroundImage, string.Empty);
            Set(VignetteSetting.BackgroundScale, 1.0f, 1.0f, 5.0f, 0.1f);
            Set(VignetteSetting.BackgroundPositionX, 0.0f);
            Set(VignetteSetting.BackgroundPositionY, 0.0f);

            Set(VignetteSetting.Model, "haru.haru.model3.json");
            Set(VignetteSetting.ModelScale, 1.0f, 0.5f, 10.0f, 0.1f);
            Set(VignetteSetting.ModelPositionX, 0.0f);
            Set(VignetteSetting.ModelPositionY, 0.0f);

            Set(VignetteSetting.MouseDrag, false);
            Set(VignetteSetting.MouseWheel, false);

            Set<string>(VignetteSetting.CameraDevice, string.Empty);
        }

        public override TrackedSettings CreateTrackedSettings() => new TrackedSettings
        {
            new TrackedSetting<string>(VignetteSetting.CameraDevice, v => new SettingDescription(v, "Camera Device", string.IsNullOrEmpty(v) ? "Default" : v, v)),
        };
    }

    public enum VignetteSetting
    {
        BackgroundMode,
        BackgroundColor,
        BackgroundImage,
        BackgroundScale,
        BackgroundPositionX,
        BackgroundPositionY,

        Model,
        ModelScale,
        ModelPositionX,
        ModelPositionY,

        MouseDrag,
        MouseWheel,

        CameraDevice,
    }

    public enum BackgroundMode
    {
        Image,
        Color,
        Video,
    }
}