// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using Newtonsoft.Json;
using osu.Framework.Graphics;

namespace Vignette.Game.Graphics.Themes
{
    [Serializable]
    public class Theme
    {
        [JsonProperty("displayName")]
        public string Name { get; set; }

        [JsonProperty("themeDarker")]
        public Colour4 AccentDarker { get; set; }

        [JsonProperty("themeDark")]
        public Colour4 AccentDark { get; set; }

        [JsonProperty("themeDarkAlt")]
        public Colour4 AccentDarkAlt { get; set; }

        [JsonProperty("themePrimary")]
        public Colour4 AccentPrimary { get; set; }

        [JsonProperty("themeSecondary")]
        public Colour4 AccentSecondary { get; set; }

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

        [JsonIgnore]
        public static Theme Light => new Theme
        {
            Name = "Light",
            AccentPrimary = Colour4.FromHex("#deab12"),
            AccentLighterAlt = Colour4.FromHex("#fefbf4"),
            AccentLighter = Colour4.FromHex("#faf1d5"),
            AccentLight = Colour4.FromHex("#f5e4b1"),
            AccentTertiary = Colour4.FromHex("#ebcb69"),
            AccentSecondary = Colour4.FromHex("#e2b42b"),
            AccentDarkAlt = Colour4.FromHex("#c89a10"),
            AccentDark = Colour4.FromHex("#a9820d"),
            AccentDarker = Colour4.FromHex("#7c600a"),
            NeutralLighterAlt = Colour4.FromHex("#e9e9e9"),
            NeutralLighter = Colour4.FromHex("#e5e5e5"),
            NeutralLight = Colour4.FromHex("#dcdcdc"),
            NeutralQuaternaryAlt = Colour4.FromHex("#cdcdcd"),
            NeutralQuaternary = Colour4.FromHex("#c4c4c4"),
            NeutralTertiaryAlt = Colour4.FromHex("#bcbcbc"),
            NeutralTertiary = Colour4.FromHex("#c3c3c3"),
            NeutralSecondary = Colour4.FromHex("#888888"),
            NeutralPrimaryAlt = Colour4.FromHex("#505050"),
            NeutralPrimary = Colour4.FromHex("#383838"),
            NeutralDark = Colour4.FromHex("#2b2b2b"),
            Black = Colour4.FromHex("#1f1f1f"),
            White = Colour4.FromHex("#f0f0f0"),
        };

        [JsonIgnore]
        public static Theme Dark => new Theme
        {
            Name = "Dark",
            AccentPrimary = Colour4.FromHex("#e0ab09"),
            AccentLighterAlt = Colour4.FromHex("#fefbf4"),
            AccentLighter = Colour4.FromHex("#faf1d4"),
            AccentLight = Colour4.FromHex("#f6e4af"),
            AccentTertiary = Colour4.FromHex("#edcb64"),
            AccentSecondary = Colour4.FromHex("#e4b423"),
            AccentDarkAlt = Colour4.FromHex("#ca9908"),
            AccentDark = Colour4.FromHex("#ab8207"),
            AccentDarker = Colour4.FromHex("#7e6005"),
            NeutralLighterAlt = Colour4.FromHex("#373737"),
            NeutralLighter = Colour4.FromHex("#3f3f3f"),
            NeutralLight = Colour4.FromHex("#4c4c4c"),
            NeutralQuaternaryAlt = Colour4.FromHex("#545454"),
            NeutralQuaternary = Colour4.FromHex("#5b5b5b"),
            NeutralTertiaryAlt = Colour4.FromHex("#777777"),
            NeutralTertiary = Colour4.FromHex("#f1f1f1"),
            NeutralSecondary = Colour4.FromHex("#f4f4f4"),
            NeutralPrimaryAlt = Colour4.FromHex("#f6f6f6"),
            NeutralPrimary = Colour4.FromHex("#ebebeb"),
            NeutralDark = Colour4.FromHex("#fafafa"),
            Black = Colour4.FromHex("#fdfdfd"),
            White = Colour4.FromHex("#2e2e2e"),
        };
    }
}
