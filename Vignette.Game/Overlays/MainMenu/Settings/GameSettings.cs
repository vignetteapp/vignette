// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Overlays.MainMenu.Settings.Sections;

namespace Vignette.Game.Overlays.MainMenu.Settings
{
    public class GameSettings : SettingsView
    {
        public override LocalisableString Title => "Settings";

        public override IconUsage Icon => FluentSystemIcons.Settings24;

        public GameSettings()
        {
            AddRange(new Drawable[]
            {
                new GeneralSection(),
                new AppearanceSection(),
                new GraphicsSection(),
                new WindowSection(),
                new DebugSection(),
            });
        }
    }
}
