// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using Newtonsoft.Json;
using osu.Framework.Graphics;

namespace Vignette.Game.Configuration
{
    [Serializable]
    public class Theme
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("themeDarker")]
        public Colour4 AccentDarker { get; set; }

        [JsonProperty("themeDark")]
        public Colour4 AccentDark { get; set; }

        [JsonProperty("themeDarkAlt")]
        public Colour4 AccentDarkAlt { get; set; }

        [JsonProperty("themePrimary")]
        public Colour4 AccentPrimary { get; set; }

        [JsonProperty("themeTertiary")]
        public Colour4 AccentTertiary { get; set; }

        [JsonProperty("themeLight")]
        public Colour4 AccentLight { get; set; }

        [JsonProperty("themeLighter")]
        public Colour4 AccentLighter { get; set; }

        [JsonProperty("themeLighterAlt")]
        public Colour4 AccentLighterAlt { get; set; }

        [JsonProperty("black")]
        public Colour4 Black { get; set; }

        [JsonProperty("neutralDark")]
        public Colour4 NeutralDark { get; set; }

        [JsonProperty("neutralPrimary")]
        public Colour4 NeutralPrimary { get; set; }

        [JsonProperty("neutralPrimaryAlt")]
        public Colour4 NeutralPrimaryAlt { get; set; }

        [JsonProperty("neutralSecondary")]
        public Colour4 NeutralSecondary { get; set; }

        [JsonProperty("neutralSecondaryAlt")]
        public Colour4 NeutralSecondaryAlt { get; set; }

        [JsonProperty("neutralTertiary")]
        public Colour4 NeutralTertiary { get; set; }

        [JsonProperty("neutralTertiaryAlt")]
        public Colour4 NeutralTertiaryAlt { get; set; }

        [JsonProperty("neutralQuaternary")]
        public Colour4 NeutralQuaternary { get; set; }

        [JsonProperty("neutralQuaternaryAlt")]
        public Colour4 NeutralQuaternaryAlt { get; set; }

        [JsonProperty("neutralLight")]
        public Colour4 NeutralLight { get; set; }

        [JsonProperty("neutralLighter")]
        public Colour4 NeutralLighter { get; set; }

        [JsonProperty("neutralLighterAlt")]
        public Colour4 NeutralLighterAlt { get; set; }

        [JsonProperty("white")]
        public Colour4 White { get; set; }
    }
}
