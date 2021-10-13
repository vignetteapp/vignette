// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
