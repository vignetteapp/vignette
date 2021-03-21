// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using osu.Framework.Platform;
using osu.Framework.Threading;

namespace Vignette.Application.IO
{
    public class BackgroundVideoStore : BackgroundStore<Stream>
    {
        protected override IEnumerable<string> Filters => new[] { "*.mp4" };

        public BackgroundVideoStore(Scheduler scheduler, Storage storage)
            : base(scheduler, storage)
        {
        }

        protected override Stream Load(Stream data) => data;
    }
}
