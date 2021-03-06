// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace Vignette.Application.Configuration
{
    public class StorageConfigManager : IniConfigManager<StorageConfig>
    {
        protected override string Filename => @"storage.ini";

        public StorageConfigManager(Storage storage)
            : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            Set(StorageConfig.FullPath, string.Empty);
        }
    }

    public enum StorageConfig
    {
        FullPath,
    }
}
