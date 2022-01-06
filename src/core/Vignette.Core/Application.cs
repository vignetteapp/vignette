// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using Stride.Engine;
using Vignette.Core.IO;

namespace Vignette.Core
{
    public class Application : Game
    {
        public AssemblyResourceProvider Resources { get; } = new AssemblyResourceProvider(typeof(Application).Assembly);
    }
}
