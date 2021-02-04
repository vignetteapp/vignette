// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
using osu.Framework.IO.Stores;

namespace Vignette.Application.Tests
{
    public static class VignetteTestResources
    {
        private static IResourceStore<byte[]> getStore() => new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(VignetteTestResources).Assembly), "Resources");

        public static Stream GetResource(string name) => getStore().GetStream(name);
    }
}
