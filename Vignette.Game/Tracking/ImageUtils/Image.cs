// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.IO;

namespace Vignette.Game.Tracking.ImageUtils
{
    public abstract class Image : IDisposable
    {
        public void Dispose()
        {
        }

        public void Save(Stream s, ImageFormat format)
        {
        }
    }
}
