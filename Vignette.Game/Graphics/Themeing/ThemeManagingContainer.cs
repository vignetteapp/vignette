// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.IO;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Platform;
using osu.Framework.Threading;
using Vignette.Game.Configuration;
using Vignette.Game.IO;

namespace Vignette.Game.Graphics.Themeing
{
    /// <summary>
    /// A container that manages themes at a configuration level.
    /// </summary>
    [Cached]
    public class ThemeManagingContainer : ThemeProvidingContainer
    {
        /// <summary>
        /// Gets the list of all loaded themes as a bindable.
        /// </summary>
        public readonly BindableList<Theme> UseableThemes = new BindableList<Theme>(new[] { Theme.Light, Theme.Dark });

        /// <summary>
        /// The store for this theme manager.
        /// </summary>
        public readonly MonitoredResourceStore Store;

        private Bindable<string> themeConfig;
        private static readonly string[] banned_names = new[] { "Light", "Dark" };

        public ThemeManagingContainer(Storage storage)
        {
            Store = new MonitoredResourceStore(storage.GetStorageForDirectory("themes"), new[] { ".json" });
            Store.FileCreated += onFileCreated;
            Store.FileDeleted += onFileDeleted;
            Store.FileUpdated += onFileUpdated;
            Store.FileRenamed += onFileRenamed;
        }

        public void Import(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);

            if (UseableThemes.Any(t => t.Name == name) || banned_names.Contains(name))
                return;
        }

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            loadExistingThemes();

            themeConfig = config.GetBindable<string>(VignetteSetting.Theme);
            themeConfig.BindValueChanged(e =>
            {
                if (e.NewValue == Current.Value.Name)
                    return;

                Schedule(() => Current.Value = UseableThemes.FirstOrDefault(t => t.Name == e.NewValue) ?? UseableThemes.FirstOrDefault());

                ThemeChanged?.Invoke();
            }, true);

            Current.BindValueChanged(e =>
            {
                if (themeConfig.Value == e.NewValue.Name)
                    return;

                themeConfig.Value = e.NewValue.Name;
            }, true);
        }

        private void loadExistingThemes()
        {
            foreach (string filename in Store.GetAvailableResources())
                onFileCreated(filename);
        }

        private void onFileCreated(string filename)
        {
            string name = Path.GetFileNameWithoutExtension(filename);

            Schedule(() =>
            {
                if (UseableThemes.Any(t => t.Name == name) || banned_names.Contains(name))
                    return;

                using var stream = Store.GetStream(filename);
                using var reader = new StreamReader(stream);

                var theme = JsonConvert.DeserializeObject<Theme>(reader.ReadToEnd());
                theme.Name = name;

                UseableThemes.Add(theme);
            });
        }

        private void onFileDeleted(string filename)
        {
            string name = Path.GetFileNameWithoutExtension(filename);

            Schedule(() =>
            {
                if (!UseableThemes.Any(t => t.Name == name) || banned_names.Contains(name))
                    return;

                UseableThemes.Remove(UseableThemes.First(t => t.Name == name));
            });
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

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            Store.Dispose();
        }
    }
}
