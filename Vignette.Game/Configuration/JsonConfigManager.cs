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
    [Serializable]
    public abstract class JsonConfigManager : ConfigManager, IDisposable
    {
        /// <summary>
        /// The backing file used to store the config. Null means no persistent storage.
        /// </summary>
        protected abstract string Filename { get; }

        /// <summary>
        /// The absolute path to the backing file for this config.
        /// </summary>
        [JsonIgnore]
        public string FilePath => storage.GetFullPath(Filename);

        private readonly Storage storage;

        public JsonConfigManager(Storage storage)
        {
            this.storage = storage;
            Load();
        }

        protected override void PerformLoad()
        {
            if (storage.Exists(Filename))
            {
                using (var stream = storage.GetStream(Filename, FileAccess.Read, FileMode.Open))
                using (var reader = new StreamReader(stream))
                    JsonConvert.PopulateObject(reader.ReadToEnd(), this, CreateSerializerSettings());
            }
        }

        protected override bool PerformSave()
        {
            if (string.IsNullOrEmpty(Filename))
                return false;

            using (var stream = storage.GetStream(Filename, FileAccess.Write, FileMode.Create))
            using (var writer = new StreamWriter(stream))
                writer.Write(JsonConvert.SerializeObject(this, CreateSerializerSettings()));

            return true;
        }

        protected virtual JsonSerializerSettings CreateSerializerSettings() => new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
    }
}
