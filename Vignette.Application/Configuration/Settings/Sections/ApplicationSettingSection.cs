// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Vignette.Application.Configuration.Settings.Components;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Graphics.Themes;
using Humanizer;
using System.Linq;
using osu.Framework.Platform;

namespace Vignette.Application.Configuration.Settings.Sections
{
    public class ApplicationSettingSection : SettingSection
    {
        public override string Label => "Application";

        public override IconUsage Icon => FontAwesome.Solid.Desktop;

        private ThemeSettingDropdown themesDropdown;

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager appConfig, ThemeStore themes, Storage storage)
        {
            Children = new Drawable[]
            {
                themesDropdown = new ThemeSettingDropdown
                {
                    Label = "Theme",
                    Current = appConfig.GetBindable<string>(ApplicationConfig.Theme),
                },
                new ButtonText
                {
                    Text = "Open Themes Folder",
                    Width = 200,
                    Action = () => storage.OpenPathInNativeExplorer("./themes"),
                },
            };

            themes.Loaded.BindCollectionChanged((s, e) => Schedule(() => { themesDropdown.Items = themes.Loaded.Select(t => t.Name); }), true);
        }

        private class ThemeSettingDropdown : LabelledDropdown<string>
        {
            protected override Drawable CreateControl() => new ThemeDropdownControl();

            private class ThemeDropdownControl : VignetteDropdown<string>
            {
                protected override string GenerateItemText(string item)
                {
                    return item.Humanize(LetterCasing.Title);
                }
            }
        }
    }
}
