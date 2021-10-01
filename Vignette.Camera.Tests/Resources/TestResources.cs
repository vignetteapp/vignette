// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System.IO;
using osu.Framework.IO.Stores;

namespace Vignette.Camera.Tests.Resources
{
    public static class TestResources
    {
        public static DllResourceStore GetStore() => new DllResourceStore(typeof(TestResources).Assembly);

        public static Stream GetStream(string name) => GetStore().GetStream($"Resources/{name}");
    }
}
