// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
