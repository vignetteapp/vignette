// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osu.Framework.Threading;
using Vignette.Application.IO.Monitors;

namespace Vignette.Application.Graphics.Themes
{
    public class ThemeStore : MonitoredDirectoryStore<Theme>
    {
        protected override string DirectoryName => @"themes";

        protected override IEnumerable<string> Filters => new[] { "*.json" };

        private IResourceStore<byte[]> store;

        public ThemeStore(Scheduler scheduler, Storage storage = null, IResourceStore<byte[]> store = null)
            : base(scheduler, storage)
        {
            this.store = store;
            loadSystemThemes();
        }

        private void loadSystemThemes()
        {
            foreach (var filename in store.GetAvailableResources())
                Add(filename.Replace(".json", string.Empty), Load(store.GetStream(filename)));
        }

        protected override Theme Load(Stream data)
        {
            using var reader = new StreamReader(data);
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, string>>(reader.ReadToEnd());
            return new Theme(deserialized);
        }
    }
}
