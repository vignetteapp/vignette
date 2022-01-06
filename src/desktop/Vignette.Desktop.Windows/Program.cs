// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using Vignette.Core;

namespace Vignette.Desktop.Windows
{
    public static class Program
    {
        public static void Main()
        {
            using var app = new Application();
            app.Run();
        }
    }
}
