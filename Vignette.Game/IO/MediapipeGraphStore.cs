// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.IO.Stores;

namespace Vignette.Game.IO
{
    public class MediapipeGraphStore : IResourceStore<string>, IDisposable
    {
        private readonly IResourceStore<byte[]> store;

        public MediapipeGraphStore(IResourceStore<byte[]> store)
        {
            this.store = store;
        }

        public string Get(string name)
        {
            byte[] buffer = store.Get(name);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        public async Task<string> GetAsync(string name)
        {
            byte[] buffer = await store.GetAsync(name);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        public Stream GetStream(string name) => store.GetStream(name);

        public IEnumerable<string> GetAvailableResources() => store.GetAvailableResources();

        public void Dispose() => store.Dispose();
    }
}
