// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using osu.Framework.Bindables;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osu.Framework.Threading;

namespace Vignette.Application.IO.Monitors
{
    public abstract class MonitoredDirectoryStore<T> : IDisposable
        where T : class
    {
        public IBindableList<MonitoredFile<T>> Loaded => loaded;

        protected abstract string DirectoryName { get; }

        protected abstract IEnumerable<string> Filters { get; }

        protected virtual bool IncludeSubDirectories => false;

        private bool disposedValue;

        private const int retry_limit = 5;

        private readonly BindableList<MonitoredFile<T>> loaded = new BindableList<MonitoredFile<T>>();

        private readonly FileSystemWatcher watcher;

        private readonly Scheduler scheduler;

        protected readonly Storage Storage;

        public MonitoredDirectoryStore(Scheduler scheduler, Storage storage = null)
        {
            this.scheduler = scheduler;

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

        public void OpenInNativeExplorer() => Storage.OpenInNativeExplorer();

        public T Get(string name) => GetReference(name)?.Data;

        public MonitoredFile<T> GetReference(string name) => loaded.FirstOrDefault(f => f.Name == name);

        public bool Has(string name) => Get(name) != null;

        public void Add(string name, T data) => schedule(() => loaded.Add(new MonitoredFile<T>(name, data)));

        public void Remove(string name) => schedule(() => loaded.Remove(loaded.FirstOrDefault(f => f.Name == name)));

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

        private void schedule(Action action) => scheduler.Add(action);

        protected abstract T Load(Stream data);

        protected virtual void FileCreated(string path)
        {
            string filename = Path.GetFileNameWithoutExtension(path);

            if (Has(filename))
                return;

            int attempts = 0;

            while (true)
            {
                attempts++;
                bool wait;

                try
                {
                    using var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    var toLoad = Load(file);
                    if (toLoad != null)
                        Add(filename, toLoad);

                    break;
                }
                catch
                {
                    if (attempts > retry_limit)
                    {
                        Logger.Log($"Failed to load {path} after {retry_limit} tries.", LoggingTarget.Runtime, LogLevel.Error);
                        break;
                    }
                    else
                    {
                        Logger.Log($"Failed to load {path}. Attempts: {attempts} / {retry_limit}.");
                        wait = true;
                    }
                }

                if (wait)
                    Thread.Sleep(250);
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
