// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
using osu.Framework.IO.Stores;

namespace Vignette.Desktop
{
    public static class VignetteDesktopResources
    {
        public static Stream Icon => new DllResourceStore(typeof(VignetteDesktopResources).Assembly).GetStream(@"vignette.ico");
    }
}
