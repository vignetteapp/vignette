// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using osu.Framework;
using osu.Framework.Platform;

namespace Vignette.Camera.Tests
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using GameHost host = Host.GetSuitableHost(@"vignette");
            host.Run(new VisualTestGame());
        }
    }
}
