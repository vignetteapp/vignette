// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Platform;

namespace Vignette.Application.IO
{
    public abstract class ObservedBackgroundStore<T> : ObservedDirectoryStore<T>
        where T : class
    {
        protected override string DirectoryName => @"backgrounds";

        public ObservedBackgroundStore(Storage storage)
            : base(storage)
        {
        }
    }
}
