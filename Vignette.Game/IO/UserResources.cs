// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    public class UserResources : IDisposable
    {
        protected Storage Storage { get; private set; }

        public MonitoredResourceStore Videos { get; private set; }

        public MonitoredResourceStore Themes { get; private set; }

        public MonitoredResourceStore Avatars { get; private set; }

        public MonitoredLargeTextureStore Images { get; private set; }

        public UserResources(GameHost host, Storage defaultStorage)
        {
            Storage = defaultStorage.GetStorageForDirectory("files");

            Images = new MonitoredLargeTextureStore(host, Storage.GetStorageForDirectory("images"), new[] { "png", "jpg", "jpeg" });
            Videos = new MonitoredResourceStore(Storage.GetStorageForDirectory("videos"), new[] { "mp4" });
            Themes = new MonitoredResourceStore(Storage.GetStorageForDirectory("themes"), new[] { "json" });
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
                    Videos?.Dispose();
                    Themes?.Dispose();
                    Avatars?.Dispose();
                    Images?.Dispose();
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
