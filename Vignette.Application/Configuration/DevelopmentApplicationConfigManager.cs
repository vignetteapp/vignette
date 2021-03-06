// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Platform;

namespace Vignette.Application.Configuration
{
    public class DevelopmentApplicationConfigManager : ApplicationConfigManager
    {
        protected override string Filename => @"app.dev.ini";

        public DevelopmentApplicationConfigManager(Storage storage)
            : base(storage)
        {
        }
    }
}
