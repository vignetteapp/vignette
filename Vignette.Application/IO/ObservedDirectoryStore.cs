// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Platform;

namespace Vignette.Application.IO
{
    public abstract class ObservedDirectoryStore<T> : IDisposable
        where T : IFileInfo
    {
        public IBindable<T> Current => current;

        public IBindableList<T> Loaded => loaded;

        protected abstract string DirectoryName { get; }

        protected abstract IEnumerable<string> Filters { get; }

        protected virtual bool IncludeSubDirectories => false;

        private bool disposedValue;

        private readonly Bindable<T> current = new Bindable<T>();

        private readonly BindableList<T> loaded = new BindableList<T>();

        private readonly FileSystemWatcher watcher;

        protected readonly Storage Storage;

        public ObservedDirectoryStore(Storage storage = null)
        {
            Storage = storage?.GetStorageForDirectory(DirectoryName);

            watcher = new FileSystemWatcher
            {
                Path = Storage?.GetFullPath(string.Empty, true),
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                IncludeSubdirectories = IncludeSubDirectories,
            };

            foreach (string filter in Filters)
                watcher.Filters.Add(filter);

            watcher.Created += (_, e) => FileCreated(e.FullPath);
            watcher.Deleted += (_, e) => FileDeleted(e.FullPath);
            watcher.Changed += (_, e) => FileChanged(e.FullPath);
            watcher.Renamed += (_, e) => FileRenamed(e.OldFullPath, e.FullPath);

            watcher.EnableRaisingEvents = true;

            loadFiles();
        }

        private void loadFiles(string directory = null)
        {
            foreach (string filter in Filters)
            {
                foreach (string path in Storage.GetFiles(directory ?? string.Empty, filter))
                    FileCreated(Storage.GetFullPath(path));

                if (IncludeSubDirectories)
                {
                    foreach (string path in Storage.GetDirectories(directory ?? string.Empty))
                        loadFiles(path);
                }
            }
        }

        protected abstract T Load(string path);

        protected virtual void FileCreated(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);

            if (loaded.Any(t => t.Name == filename))
                return;

            var toLoad = Load(path);
            if (toLoad != null)
                loaded.Add(toLoad);
        }

        protected virtual void FileDeleted(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);
            var asset = loaded.FirstOrDefault(t => t.Name == filename);

            if (asset != null)
                loaded.Remove(asset);
        }

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
