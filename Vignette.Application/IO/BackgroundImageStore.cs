// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu.Framework.Threading;

namespace Vignette.Application.IO
{
    public class BackgroundImageStore : BackgroundStore<Texture>
    {
        protected override IEnumerable<string> Filters => new[] { "*.jpg", "*.jpeg", "*.png" };

        public BackgroundImageStore(Scheduler scheduler, Storage storage)
            : base(scheduler, storage)
        {
        }

        protected override Texture Load(Stream stream) => Texture.FromStream(stream);
    }
}
