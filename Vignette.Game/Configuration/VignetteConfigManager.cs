// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using osu.Framework.IO.Serialization;
using osu.Framework.Platform;
using Vignette.Game.Configuration.Converters;

namespace Vignette.Game.Configuration
{
    public class VignetteConfigManager : JsonConfigManager<VignetteConfig>
    {
        protected override string Filename => "config.json";

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
