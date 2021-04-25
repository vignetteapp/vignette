// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Linq;
using osu.Framework.Bindables;
using Vignette.Game.Configuration;
using Vignette.Game.IO;

namespace Vignette.Game.Themeing
{
    public class ThemeManager : IThemeSource
    {
        public readonly Bindable<Theme> Current = new Bindable<Theme>(Theme.Light);

        public readonly BindableList<Theme> UseableThemes = new BindableList<Theme>();

        private readonly MonitoredResourceStore store;

        private Bindable<string> themeConfig;

        public event Action SourceChanged;

        public ThemeManager(UserResources resources, VignetteConfigManager config)
        {
            UseableThemes.Add(Theme.Light);
            UseableThemes.Add(Theme.Dark);

            store = resources.Themes;
            store.FileCreated += onFileCreated;
            store.FileDeleted += onFileDeleted;
            store.FileUpdated += onFileUpdated;
            store.FileRenamed += onFileRenamed;

            themeConfig = config.GetBindable<string>(VignetteSetting.Theme);
            themeConfig.BindValueChanged(e =>
            {
                Current.Value = UseableThemes.First(t => t.Name == e.NewValue);
                SourceChanged?.Invoke();
            }, true);
        }

        public Theme GetCurrent() => Current.Value;

        private void onFileCreated(string filename)
        {
            if (UseableThemes.Any(t => t.Name == filename))
                return;

            using var stream = store.GetStream(filename);
            UseableThemes.Add(new Theme(filename, stream));
        }

        private void onFileDeleted(string filename)
        {
            if (!UseableThemes.Any(t => t.Name == filename))
                return;

            UseableThemes.Remove(UseableThemes.First(t => t.Name == filename));
        }

        private void onFileUpdated(string filename)
        {
            onFileDeleted(filename);
            onFileCreated(filename);
        }

        private void onFileRenamed(string oldName, string newName)
        {
            onFileDeleted(oldName);
            onFileCreated(newName);
        }
    }
}
