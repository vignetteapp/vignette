// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using osu.Framework;
using osu.Framework.Bindables;
using osu.Framework.Platform;

namespace Vignette.Application.Graphics.Themes
{
    public class ThemeStore : IDisposable
    {
        public IBindable<Theme> Current => current;

        public IBindableList<Theme> Loaded => loaded;

        private readonly Storage storage;

        private readonly FileSystemWatcher watcher;

        private readonly Bindable<Theme> current = new Bindable<Theme>();

        private readonly BindableList<Theme> loaded = new BindableList<Theme>();

        public ThemeStore(Storage storage = null)
        {
            this.storage = storage?.GetStorageForDirectory("themes");

            if (this.storage == null)
                return;

            watcher = new FileSystemWatcher
            {
                Path = this.storage?.GetFullPath("."),
                Filter = ".json",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            };

            watcher.Created += (_, e) => loadTheme(e.FullPath);
            watcher.Deleted += (_, e) => unloadTheme(e.FullPath);
            watcher.Changed += (_, e) => reloadTheme(e.FullPath);
            watcher.Renamed += (_, e) => reloadTheme(e.OldFullPath, e.FullPath);

            watcher.EnableRaisingEvents = true;

            loadSystemThemes();
        }

        public void Save()
        {
            
        }

        private void loadSystemThemes()
        {
            string[] files = Directory.GetFiles(Path.Combine(RuntimeInfo.StartupDirectory, "themes"));

            foreach (string path in files)
                loadTheme(path);
        }

        private void reloadTheme(string path)
        {
            unloadTheme(path);
            loadTheme(path);
        }

        private void reloadTheme(string oldPath, string newPath)
        {
            unloadTheme(oldPath);
            loadTheme(newPath);
        }

        private void unloadTheme(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);
            var theme = loaded.FirstOrDefault(t => t.Name == filename);

            if (theme != null)
                loaded.Remove(theme);
        }

        private void loadTheme(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);

            if (loaded.Any(t => t.Name == filename))
                return;

            using var file = File.OpenRead(path);
            using var reader = new StreamReader(file);

            var deserialized = JsonSerializer.Deserialize<Dictionary<string, string>>(reader.ReadToEnd());
            loaded.Add(new Theme(filename, deserialized));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            watcher.Dispose();
        }
    }
}
