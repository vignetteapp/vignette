// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    public class UserResources : IDisposable
    {
        protected Storage Storage { get; private set; }

        public MonitoredResourceStore Themes { get; private set; }

        public MonitoredResourceStore Avatars { get; private set; }

        public UserResources(GameHost host, Storage defaultStorage)
        {
            Storage = defaultStorage.GetStorageForDirectory("files");

            Themes = new MonitoredResourceStore(Storage.GetStorageForDirectory("themes"), new[] { ".json" });
            Avatars = new MonitoredResourceStore(Storage.GetStorageForDirectory("avatars"));
        }

        #region Disposal

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Themes?.Dispose();
                    Avatars?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
