// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using osu.Framework.Bindables;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    /// <summary>
    /// A storage that monitors and notifies for file changes via the <see cref="FileSystemWatcher"/>
    /// </summary>
    public class MonitoredStorage : WrappedStorage, IDisposable
    {
        /// <summary>
        /// Sets whether this instance should listen for file changes in its target storage.
        /// If disabled, all future file changes will be ignored until it has been re-enabled.
        /// </summary>
        public readonly BindableBool Enabled = new BindableBool(true);

        /// <summary>
        /// A list of files currently monitored by this store.
        /// </summary>
        public readonly BindableList<string> Files = new BindableList<string>();

        /// <summary>
        /// Occurs when a file has been created
        /// </summary>
        public event Action<string> FileCreated;

        /// <summary>
        /// Occurs when a file has been deleted
        /// </summary>
        public event Action<string> FileDeleted;

        /// <summary>
        /// Occurs when a file has been updated
        /// </summary>
        public event Action<string> FileUpdated;

        /// <summary>
        /// Occurs when a file has been renamed
        /// </summary>
        public event Action<string, string> FileRenamed;

        protected virtual IEnumerable<string> Filters => Array.Empty<string>();

        private readonly FileSystemWatcher watcher;

        public MonitoredStorage(Storage underlyingStorage)
            : base(underlyingStorage)
        {
            watcher = new FileSystemWatcher
            {
                Path = UnderlyingStorage?.GetFullPath(string.Empty, true),
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size
            };

            foreach (string filter in Filters)
                watcher.Filters.Add(filter);

            watcher.Created += (_, e) => OnFileCreated(e.Name);
            watcher.Deleted += (_, e) => OnFileDeleted(e.Name);
            watcher.Changed += (_, e) => OnFileUpdated(e.Name);
            watcher.Renamed += (_, e) => OnFileRenamed(e.OldName, e.Name);

            Enabled.BindValueChanged(e => watcher.EnableRaisingEvents = e.NewValue, true);
        }

        protected void OnFileCreated(string name, bool invoke = true)
        {
            if (Files.Contains(name))
                return;

            Files.Add(name);

            if (invoke)
                FileCreated?.Invoke(name);
        }

        protected void OnFileDeleted(string name, bool invoke = true)
        {
            if (!Files.Contains(name))
                return;

            Files.Remove(name);

            if (invoke)
                FileDeleted?.Invoke(name);
        }

        protected void OnFileUpdated(string name)
        {
            OnFileDeleted(name, false);
            OnFileCreated(name, false);
            FileUpdated?.Invoke(name);
        }

        protected void OnFileRenamed(string oldName, string newName)
        {
            OnFileDeleted(oldName, false);
            OnFileCreated(newName, false);
            FileRenamed?.Invoke(oldName, newName);
        }

        private bool isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    watcher.Dispose();

                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
