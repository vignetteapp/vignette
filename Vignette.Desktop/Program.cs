// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework;

namespace Vignette.Desktop
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using var host = Host.GetSuitableHost("vignette");
            host.Run(new VignetteGameDesktop());
        }
    }
}
