// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using osu.Framework;
using osu.Framework.Platform;
using Vignette.Application.IO;

namespace Vignette.Application.Graphics.Themes
{
    public class ThemeStore : ObservedDirectoryStore<Theme>
    {
        protected override string DirectoryName => @"themes";

        protected override IEnumerable<string> Filters => new[] { "*.json" };

        public ThemeStore(Storage storage = null)
            : base(storage)
        {
            loadSystemThemes();
        }

        private void loadSystemThemes()
        {
            foreach (string path in Directory.GetFiles(Path.Combine(RuntimeInfo.StartupDirectory, DirectoryName)))
                FileCreated(path);
        }

        protected override Theme Load(Stream data)
        {
            using var reader = new StreamReader(data);
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, string>>(reader.ReadToEnd());
            return new Theme(deserialized);
        }
    }
}
