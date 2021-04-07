// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Platform;

namespace Vignette.Game.Configuration
{
    public class VignetteDevelopmentConfigManager : VignetteConfigManager
    {
        protected override string Filename => "config.dev.json";

        public VignetteDevelopmentConfigManager(Storage storage)
            : base(storage)
        {
        }
    }
}
