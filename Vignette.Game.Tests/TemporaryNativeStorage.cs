// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Platform;

namespace Vignette.Game.Tests
{
    public class TemporaryNativeStorage : NativeStorage, IDisposable
    {
        public TemporaryNativeStorage(string path, GameHost host = null)
            : base(path, host)
        {
            GetFullPath("./", true);
        }

        public void Dispose()
        {
            DeleteDirectory(string.Empty);
        }
    }
}
