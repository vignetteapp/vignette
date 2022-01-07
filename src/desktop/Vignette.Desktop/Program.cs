// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using Stride.Engine;

namespace Vignette.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using var game = new Game();
            game.Run();
        }
    }
}
