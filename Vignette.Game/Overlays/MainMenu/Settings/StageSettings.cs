// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Overlays.MainMenu.Settings.Sections;

namespace Vignette.Game.Overlays.MainMenu.Settings
{
    public class StageSettings : SettingsView
    {
        public override LocalisableString Title => "Scene";

        public override IconUsage Icon => FluentSystemIcons.Wallpaper24;

        public StageSettings()
        {
            AddRange(new Drawable[]
            {
                new AvatarSection(),
                new BackgroundSection(),
            });
        }
    }
}
