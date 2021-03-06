// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.IO;
using osu.Framework.Platform;

namespace Vignette.Application.IO.Stores
{
    public abstract class ObservedDirectoryStore : IDisposable
    {
        private bool disposedValue;


        private readonly FileSystemWatcher watcher;

        protected readonly Storage Storage;

        protected abstract string DirectoryName { get; }

        public ObservedDirectoryStore(Storage storage = null)
        {
            Storage = storage?.GetStorageForDirectory(DirectoryName);

            watcher = new FileSystemWatcher
            {
                Path = Storage?.GetFullPath(string.Empty),
                Filter = "*.json",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            };

            watcher.Created += (_, e) => FileCreated(e.FullPath);
            watcher.Deleted += (_, e) => FileDeleted(e.FullPath);
            watcher.Changed += (_, e) => FileChanged(e.FullPath);
            watcher.Renamed += (_, e) => FileRenamed(e.OldFullPath, e.FullPath);

            watcher.EnableRaisingEvents = true;
        }

        protected abstract void FileCreated(string path);

        protected abstract void FileDeleted(string path);

        protected virtual void FileChanged(string path)
        {
            FileDeleted(path);
            FileCreated(path);
        }

        protected virtual void FileRenamed(string oldPath, string newPath)
        {
            FileDeleted(oldPath);
            FileCreated(newPath);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    watcher.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
