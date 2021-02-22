// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Logging;
using osu.Framework.Platform;

namespace Vignette.Application.IO
{
    public abstract class ObservedDirectoryStore<T> : IDisposable
        where T : class
    {
        public IBindableList<ObservableFile<T>> Loaded => loaded;

        protected abstract string DirectoryName { get; }

        protected abstract IEnumerable<string> Filters { get; }

        protected virtual bool IncludeSubDirectories => false;

        private bool disposedValue;

        private readonly BindableList<ObservableFile<T>> loaded = new BindableList<ObservableFile<T>>();

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

        public T Get(string name) => loaded.FirstOrDefault(f => f.Name == name);

        public bool Has(string name) => Get(name) != null;

        public void Add(string name, T data) => loaded.Add(new ObservableFile<T>(name, data));

        public void Remove(string name) => loaded.Remove(loaded.FirstOrDefault(f => f.Name == name));

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

        protected abstract T Load(Stream data);

        protected virtual void FileCreated(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);

            if (Has(filename))
                return;

            try
            {
                using var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                var toLoad = Load(file);
                if (toLoad != null)
                    Add(filename, toLoad);
            }
            catch
            {
                Logger.Log($"Failed to load theme to load {path}.");
            }
        }

        protected virtual void FileDeleted(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);

            if (Has(filename))
                Remove(filename);
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
