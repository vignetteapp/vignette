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
        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public Theme Current => CurrentBindable.Value;

        /// <summary>
        /// Gets the current theme as a bindable.
        /// </summary>
        public readonly Bindable<Theme> CurrentBindable = new Bindable<Theme>(Theme.Light);

        /// <summary>
        /// Gets the list of all loaded themes as a bindable.
        /// </summary>
        public readonly BindableList<Theme> UseableThemes = new BindableList<Theme>(new[] { Theme.Light, Theme.Dark });

        /// <summary>
        /// Invoked when the theme has been changed.
        /// </summary>
        public event Action ThemeChanged;

        private readonly MonitoredResourceStore store;
        private readonly Bindable<string> themeConfig;
        private readonly Scheduler scheduler;

        public ThemeManager(Scheduler scheduler, UserResources resources, VignetteConfigManager config)
        {
            this.scheduler = scheduler;

            store = resources.Themes;
            store.FileCreated += onFileCreated;
            store.FileDeleted += onFileDeleted;
            store.FileUpdated += onFileUpdated;
            store.FileRenamed += onFileRenamed;

            loadExistingThemes();

            themeConfig = config.GetBindable<string>(VignetteSetting.Theme);
            themeConfig.BindValueChanged(e =>
            {
                if (e.NewValue == CurrentBindable.Value.Name)
                    return;

                CurrentBindable.Value = UseableThemes.FirstOrDefault(t => t.Name == e.NewValue) ?? UseableThemes.FirstOrDefault();

                ThemeChanged?.Invoke();
            }, true);

            CurrentBindable.BindValueChanged(e =>
            {
                if (themeConfig.Value == e.NewValue.Name)
                    return;

                themeConfig.Value = e.NewValue.Name;
                ThemeChanged?.Invoke();
            }, true);
        }

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
