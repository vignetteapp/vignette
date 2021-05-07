// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Threading;
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

        private Scheduler scheduler;

        public event Action SourceChanged;

        public ThemeManager(Scheduler scheduler, UserResources resources, VignetteConfigManager config)
        {
            this.scheduler = scheduler;

            UseableThemes.Add(Theme.Light);
            UseableThemes.Add(Theme.Dark);

            store = resources.Themes;
            store.FileCreated += onFileCreated;
            store.FileDeleted += onFileDeleted;
            store.FileUpdated += onFileUpdated;
            store.FileRenamed += onFileRenamed;

            loadExistingThemes();

            themeConfig = config.GetBindable<string>(VignetteSetting.Theme);
            themeConfig.BindValueChanged(e =>
            {
                if (e.NewValue == Current.Value.Name)
                    return;

                Current.Value = UseableThemes.FirstOrDefault(t => t.Name == e.NewValue) ?? Theme.Light;

                SourceChanged?.Invoke();
            }, true);

            Current.BindValueChanged(e =>
            {
                if (themeConfig.Value == e.NewValue.Name)
                    return;

                themeConfig.Value = e.NewValue.Name;
                SourceChanged?.Invoke();
            }, true);
        }

        public Theme GetCurrent() => Current.Value;

        private void loadExistingThemes()
        {
            foreach (string filename in store.GetAvailableResources())
                onFileCreated(filename);
        }

        private void onFileCreated(string filename)
        {
            if (UseableThemes.Any(t => t.Name == filename))
                return;

            scheduler.Add(() =>
            {
                using var stream = store.GetStream(filename);
                UseableThemes.Add(new Theme(filename, stream));
            });
        }

        private void onFileDeleted(string filename)
        {
            if (!UseableThemes.Any(t => t.Name == filename))
                return;

            scheduler.Add(() => UseableThemes.Remove(UseableThemes.First(t => t.Name == filename)));
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
