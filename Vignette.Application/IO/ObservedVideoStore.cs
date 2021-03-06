// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using osu.Framework.Graphics.Video;
using osu.Framework.Platform;

namespace Vignette.Application.IO
{
    public class ObservedVideoStore : ObservedBackgroundStore<Video>
    {
        protected override IEnumerable<string> Filters => new[] { "*.mp4" };

        public ObservedVideoStore(Storage storage)
            : base(storage)
        {
        }

        protected override Video Load(Stream data) => new Video(data);
    }
}
