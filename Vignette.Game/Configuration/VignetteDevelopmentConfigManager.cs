// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Platform;

namespace Vignette.Game.Configuration
{
    public class VignetteDevelopmentConfigManager : VignetteConfigManager
    {
        protected override string Filename => "config.dev.ini";

        public VignetteDevelopmentConfigManager(Storage storage, IDictionary<VignetteSetting, object> defaultOverrides = null)
            : base(storage, defaultOverrides)
        {
        }
    }
}
