// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using osu.Framework;
using osu.Framework.Bindables;
using osu.Framework.Logging;
using osu.Framework.Platform;
using Vignette.Application.IO.Stores;

namespace Vignette.Application.Graphics.Themes
{
    public class ThemeStore : ObservedDirectoryStore
    {
        public IBindable<Theme> Current => current;

        public IBindableList<Theme> Loaded => loaded;

        protected override string DirectoryName => @"themes";

        private readonly Bindable<Theme> current = new Bindable<Theme>();

        private readonly BindableList<Theme> loaded = new BindableList<Theme>();

        public ThemeStore(Storage storage = null)
            : base(storage)
        {
            loadSystemThemes();
            loadUserThemes();
        }

        private void loadUserThemes()
        {
            foreach (string path in Storage.GetFiles(string.Empty, "*.json"))
                FileCreated(Storage.GetFullPath(path));
        }

        private void loadSystemThemes()
        {
            foreach (string path in Directory.GetFiles(Path.Combine(RuntimeInfo.StartupDirectory, "themes")))
                FileCreated(path);
        }

        protected override void FileCreated(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);

            if (loaded.Any(t => t.Name == filename))
                return;

            using var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(file);

            try
            {
                var deserialized = JsonSerializer.Deserialize<Dictionary<string, string>>(reader.ReadToEnd());
                loaded.Add(new Theme(filename, deserialized));
            }
            catch (Exception e)
            {
                Logger.Log($"Failed to load theme to load theme {filename}.\n{e}");
            }
        }

        protected override void FileDeleted(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);
            var theme = loaded.FirstOrDefault(t => t.Name == filename);

            if (theme != null)
                loaded.Remove(theme);
        }
    }
}
