// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework;

namespace Vignette.Application.Tests
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using var host = Host.GetSuitableHost(@"Vignette");
            host.Run(new VignetteTestBrowser());
        }
    }
}
