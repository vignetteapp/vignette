// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework;
using osu.Framework.Platform;

namespace Vignette.Desktop.Windows
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using GameHost host = Host.GetSuitableHost(@"Vignette");
            host.Run(new VignetteWindowsApplication());
        }
    }
}
