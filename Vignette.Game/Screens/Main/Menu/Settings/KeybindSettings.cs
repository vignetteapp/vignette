// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Screens.Main.Menu.Settings.Sections;

namespace Vignette.Game.Screens.Main.Menu.Settings
{
    public class KeybindSettings : SettingsPage
    {
        public override LocalisableString Title => "Keybinds";

        public override IconUsage Icon => FluentSystemIcons.Keyboard24;

        public KeybindSettings()
        {
            Add(new GlobalActionsSection());
        }

        private class GlobalActionsSection : WorkInProgressSection
        {
            public override LocalisableString Header => "Global";
        }
    }
}
