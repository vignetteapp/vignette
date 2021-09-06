// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Configuration;

namespace Vignette.Game.Configuration
{
    public class InMemoryConfigManager<T> : ConfigManager<T>
        where T : struct, Enum
    {
        public InMemoryConfigManager()
        {
            InitialiseDefaults();
        }

        protected override void PerformLoad()
        {
        }

        protected override bool PerformSave() => true;
    }
}
