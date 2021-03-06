// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;

namespace Vignette.Application.IO
{
    public class ObservedTextureStore : ObservedBackgroundStore<Texture>
    {
        protected override IEnumerable<string> Filters => new[] { "*.jpg", "*.jpeg", "*.png" };

        public ObservedTextureStore(Storage storage)
            : base(storage)
        {
        }

        protected override Texture Load(Stream stream) => Texture.FromStream(stream);
    }
}
