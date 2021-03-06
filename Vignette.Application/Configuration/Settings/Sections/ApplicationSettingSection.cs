// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using Vignette.Application.Configuration.Settings.Components;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.IO;
using Humanizer;
using osu.Framework.Bindables;
using System.Linq;
using osu.Framework.Platform;

namespace Vignette.Application.Configuration.Settings.Sections
{
    public class ApplicationSettingSection : SettingSection
    {
        public override string Label => "Application";

        public override IconUsage Icon => FontAwesome.Solid.Desktop;

        private Bindable<ObservableFile<Theme>> theme = new Bindable<ObservableFile<Theme>>();

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager appConfig, ThemeStore themes, Storage storage)
        {
            var themeConfig = appConfig.GetBindable<string>(ApplicationConfig.Theme);

            Children = new Drawable[]
            {
                new ThemeSettingDropdown
                {
                    Label = "Theme",
                    Current = theme,
                    ItemSource =  themes.Loaded,
                },
                new ButtonText
                {
                    Text = "Open Themes Folder",
                    Width = 200,
                    Action = () => storage.OpenPathInNativeExplorer("./themes"),
                },
            };

            theme.BindValueChanged((e) => themeConfig.Value = e.NewValue.Name, true);
        }

        private class ThemeSettingDropdown : LabelledDropdown<ObservableFile<Theme>>
        {
            protected override Drawable CreateControl() => new ThemeDropdownControl();

            private class ThemeDropdownControl : VignetteDropdown<ObservableFile<Theme>>
            {
                protected override string GenerateItemText(ObservableFile<Theme> item)
                {
                    return item.Name.Humanize(LetterCasing.Title);
                }
            }
        }
    }
}
