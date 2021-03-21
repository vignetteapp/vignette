// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Platform;
using osu.Framework.Threading;
using Vignette.Application.IO.Monitors;

namespace Vignette.Application.IO
{
    public abstract class BackgroundStore<T> : MonitoredDirectoryStore<T>
        where T : class
    {
        protected override string DirectoryName => @"backgrounds";

        public BackgroundStore(Scheduler scheduler, Storage storage)
            : base(scheduler, storage)
        {
        }
    }
}
