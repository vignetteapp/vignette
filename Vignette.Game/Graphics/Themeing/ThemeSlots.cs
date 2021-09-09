// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.ComponentModel;

namespace Vignette.Game.Graphics.Themeing
{
    public enum ThemeSlot
    {
        [Description("themeDarker")]
        AccentDarker,
        [Description("themeDark")]
        AccentDark,
        [Description("themeDarkAlt")]
        AccentDarkAlt,
        [Description("themePrimary")]
        AccentPrimary,
        [Description("themeSecondary")]
        AccentSecondary,
        [Description("themeTertiary")]
        AccentTertiary,
        [Description("themeLight")]
        AccentLight,
        [Description("themeLighter")]
        AccentLighter,
        [Description("themeLighterAlt")]
        AccentLighterAlt,
        [Description("black")]
        Black,
        [Description("neutralDark")]
        Gray190,
        [Description("neutralPrimary")]
        Gray160,
        [Description("neutralPrimaryAlt")]
        Gray150,
        [Description("neutralSecondary")]
        Gray130,
        [Description("neutralSecondaryAlt")]
        Gray110,
        [Description("neutralTertiary")]
        Gray90,
        [Description("neutralTertiaryAlt")]
        Gray60,
        [Description("neutralQuaternary")]
        Gray50,
        [Description("neutralQuaternaryAlt")]
        Gray40,
        [Description("neutralLight")]
        Gray30,
        [Description("neutralLighter")]
        Gray20,
        [Description("neutralLighterAlt")]
        Gray10,
        [Description("white")]
        White,
        [Description("transparent")]
        Transparent,
        [Description("error")]
        Error,
        [Description("errorBackground")]
        ErrorBackground,
        [Description("success")]
        Success,
        [Description("successBackground")]
        SuccessBackground,
        [Description("severeWarning")]
        SevereWarning,
        [Description("severeWarningBackground")]
        SevereWarningBackground,
        [Description("warning")]
        Warning,
        [Description("warningBackground")]
        WarningBackground,
    }
}
