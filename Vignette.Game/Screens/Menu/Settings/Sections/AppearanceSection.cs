// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.IO;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu.Settings.Sections
{
    public class AppearanceSection : SettingsSection
    {
        public override LocalisableString Header => "Appearance";

        private Bindable<Theme> theme;

        [BackgroundDependencyLoader]
        private void load(GameHost host, VignetteConfigManager config, UserResources userResources, ThemeManager manager)
        {
            theme = manager.Current.GetBoundCopy();

            AddRange(new Drawable[]
            {
                new SettingsDropdown<Theme>
                {
                    Label = "Theme",
                    Current = theme,
                    ItemSource = manager.UseableThemes,
                },
                new SettingsButton
                {
                    Label = "Open themes folder",
                    Action = () => userResources.Themes.OpenInNativeExplorer(),
                },
                new SettingsButton
                {
                    Label = "Theme Designer",
                    Description = "Opens Microsoft's Fluent Theme Designer in a browser. Export the theme as JSON and save it in your themes folder.",
                    Action = () => host.OpenUrlExternally("https://aka.ms/themedesigner"),
                },
            });
        }
    }
}
