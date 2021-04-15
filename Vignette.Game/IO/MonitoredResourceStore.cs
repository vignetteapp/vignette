// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    public class MonitoredResourceStore : MonitoredStorage, IResourceStore<byte[]>
    {
        protected IResourceStore<byte[]> UnderlyingStore;

        public MonitoredResourceStore(Storage underlyingStorage, IEnumerable<string> filters = null)
            : base(underlyingStorage, filters)
        {
            UnderlyingStore = new StorageBackedResourceStore(underlyingStorage);
        }

        public byte[] Get(string name)
            => UnderlyingStore.Get(name);

        public Task<byte[]> GetAsync(string name)
            => UnderlyingStore.GetAsync(name);

        public IEnumerable<string> GetAvailableResources()
            => UnderlyingStore.GetAvailableResources();

        public Stream GetStream(string name)
            => UnderlyingStore.GetStream(name);
    }
}
