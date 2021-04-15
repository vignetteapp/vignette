// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Platform;

namespace Vignette.Game.Tests
{
    public class TemporaryNativeStorage : NativeStorage, IDisposable
    {
        public TemporaryNativeStorage(string path, GameHost host = null)
            : base(path, host)
        {
            GetFullPath(string.Empty, true);
        }

        public void Dispose()
        {
            DeleteDirectory(string.Empty);
        }
    }
}
