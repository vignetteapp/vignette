// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Screens.Menu.Settings.Sections;

namespace Vignette.Game.Screens.Menu
{
    public class TrackerSettingScreen : SettingsScreen
    {
        public override LocalisableString Title => "Tracking";

        public override IconUsage Icon => FluentSystemIcons.Accessibility24;

        public TrackerSettingScreen()
        {
            AddRange(new Drawable[]
            {
                new CameraSection(),
                new RecognitionSection(),
            });
        }
    }
}
