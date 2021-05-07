// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    public class MonitoredResourceStore : MonitoredStorage, IResourceStore<byte[]>
    {
        protected IResourceStore<byte[]> UnderlyingStore;

        private string[] filters;

        public MonitoredResourceStore(Storage underlyingStorage, IEnumerable<string> filters = null)
            : base(underlyingStorage, filters)
        {
            this.filters = filters?.ToArray();
            UnderlyingStore = new StorageBackedResourceStore(underlyingStorage);
        }

        public byte[] Get(string name)
            => UnderlyingStore.Get(name);

        public Task<byte[]> GetAsync(string name)
            => UnderlyingStore.GetAsync(name);

        // UnderlyingStore would always yield no results from the root directory.
        // Might be a bug on the framework's end since it only checks for subdirectories.
        public IEnumerable<string> GetAvailableResources()
            => UnderlyingStorage
                .GetDirectories(string.Empty)
                .Append(string.Empty)
                .SelectMany(d => UnderlyingStorage.GetFiles(d))
                .Where(p => filters?.Contains(Path.GetExtension(p)) ?? false);

        public Stream GetStream(string name)
            => UnderlyingStore.GetStream(name);
    }
}
