// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Overlays.MainMenu;
using Vignette.Game.Overlays.MainMenu.Settings.Sections;

namespace Vignette.Game.Overlays.MainMenu.Settings
{
    public class TrackerSettings : SettingsView
    {
        public override LocalisableString Title => "Tracking";

        public override IconUsage Icon => FluentSystemIcons.Accessibility24;

        public TrackerSettings()
        {
            AddRange(new Drawable[]
            {
                new CameraSection(),
                new RecognitionSection(),
            });
        }
    }
}
