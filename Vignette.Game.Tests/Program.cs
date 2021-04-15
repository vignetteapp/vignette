// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework;

namespace Vignette.Game.Tests
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (var host = Host.GetSuitableHost("vignette"))
                host.Run(new VisualTestGame());
        }
    }
}
