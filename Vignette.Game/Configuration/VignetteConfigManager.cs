// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using osu.Framework.Bindables;
using osu.Framework.IO.Serialization;
using osu.Framework.Platform;
using Vignette.Game.Configuration.Converters;
using Vignette.Game.Configuration.Models;

namespace Vignette.Game.Configuration
{
    public class VignetteConfigManager : JsonConfigManager
    {
        protected override string Filename => "config.json";

        public Bindable<bool> WindowResizable = new Bindable<bool>();

        public Bindable<bool> ShowFpsOverlay = new Bindable<bool>();

        public Bindable<string> CameraDevice = new Bindable<string>();

        public BackgroundConfig Background = new BackgroundConfig();

        public VignetteConfigManager(Storage storage)
            : base(storage)
        {
        }

        protected override JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = base.CreateSerializerSettings();
            settings.Converters = new JsonConverter[]
            {
                new Vector2Converter(),
                new Colour4Converter(),
                new StringEnumConverter(),
            };

            return settings;
        }
    }
}
