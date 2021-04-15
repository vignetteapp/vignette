// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
using osu.Framework.IO.Stores;

namespace Vignette.Game.Tests.Resources
{
    public static class TestResources
    {
        public static ResourceStore<byte[]> GetStore() => new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(TestResources).Assembly), "Resources");

        public static Stream GetStream(string name) => GetStore().GetStream(name);
    }
}
