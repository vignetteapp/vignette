// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.IO;
using Newtonsoft.Json;
using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace Vignette.Game.Configuration
{
    /// <summary>
    /// A config manager backed by a JSON file.
    /// </summary>
    public abstract class JsonConfigManager<T> : IConfigManager, IDisposable
        where T : class, new()
    {
        /// <summary>
        /// The parsed config.
        /// </summary>
        public T Config { get; private set; }

        /// <summary>
        /// The backing file used to store the config. Null means no persistent storage.
        /// </summary>
        protected abstract string Filename { get; }

        private readonly Storage storage;

        public JsonConfigManager(Storage storage)
        {
            this.storage = storage;
            Load();
        }

        public void Load()
        {
            if (storage.Exists(Filename))
            {
                using (var stream = storage.GetStream(Filename, FileAccess.Read, FileMode.Open))
                using (var reader = new StreamReader(stream))
                    Config = JsonConvert.DeserializeObject<T>(reader.ReadToEnd(), CreateSerializerSettings());
            }

            Config ??= Activator.CreateInstance<T>();
        }

        public bool Save()
        {
            if (Config == null || string.IsNullOrEmpty(Filename))
                return false;

            try
            {
                using (var stream = storage.GetStream(Filename, FileAccess.Write, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                    writer.Write(JsonConvert.SerializeObject(Config, CreateSerializerSettings()));
            }
            catch
            {
                return false;
            }

            return true;
        }

        protected virtual JsonSerializerSettings CreateSerializerSettings() => new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
        };

        #region Disposal

        private bool isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                Save();
                isDisposed = true;
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
