// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Localisation;
using Vignette.Game.Overlays.MainMenu.Settings.Components;

namespace Vignette.Game.Overlays.MainMenu.Settings.Sections
{
    public class GeneralSection : SettingsSection
    {
        public override LocalisableString Header => "General";

        public GeneralSection()
        {
            AddRange(new Drawable[]
            {
                new SettingsDropdown<string>
                {
                    Label = "Language",
                    Items = new[] { "English" },
                },
            });
        }
    }
}
