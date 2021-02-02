// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using osu.Framework;
using osu.Framework.Platform;

namespace Vignette.Desktop
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using GameHost host = Host.GetSuitableHost(@"Vignette");
            host.Run(new VignetteDesktopApplication());
        }
    }
}
