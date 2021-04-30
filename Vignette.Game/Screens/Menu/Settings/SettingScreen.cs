// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Screens.Menu.Settings
{
    public class SettingScreen : MenuScreen
    {
        public override LocalisableString Title => "Settings";

        public override IconUsage Icon => FluentSystemIcons.Settings24;
    }
}
