using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;

namespace holotrack.Configuration
{
    public class HoloTrackConfigManager : IniConfigManager<HoloTrackSetting>
    {
        internal const string FILENAME = @"holotrack.ini";
        protected override string Filename => FILENAME;

        public HoloTrackConfigManager(Storage storage, IDictionary<HoloTrackSetting, object> defaultOverrides = null)
            : base(storage, defaultOverrides)
        {
        }

        protected override void InitialiseDefaults()
        {
            Set(HoloTrackSetting.BackgroundMode, BackgroundMode.Color);
            Set(HoloTrackSetting.BackgroundColor, new Colour4(0.0f, 1.0f, 0.0f, 1.0f));
            Set(HoloTrackSetting.BackgroundImage, "");
            Set(HoloTrackSetting.BackgroundScale, 1.0f, 1.0f, 5.0f, 0.1f);
            Set(HoloTrackSetting.BackgroundPositionX, 0.0f);
            Set(HoloTrackSetting.BackgroundPositionY, 0.0f);

            Set(HoloTrackSetting.Model, "haru.haru.model3.json");
            Set(HoloTrackSetting.ModelScale, 1.0f, 0.5f, 10.0f, 0.1f);
            Set(HoloTrackSetting.ModelPositionX, 0.0f);
            Set(HoloTrackSetting.ModelPositionY, 0.0f);

            Set(HoloTrackSetting.MouseDrag, false);
            Set(HoloTrackSetting.MouseWheel, false);
        }
    }

    public enum HoloTrackSetting
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
    }

    public enum BackgroundMode
    {
        Image,
        Color,
    }
}