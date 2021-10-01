// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.IO;
using osu.Framework.IO.Stores;

namespace Vignette.Live2D.Tests.Resources
{
    public static class TestResources
    {
        public static IResourceStore<byte[]> GetResourceStore() => new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(TestResources).Assembly), "Resources");
        public static IResourceStore<byte[]> GetModelResourceStore() => new NamespacedResourceStore<byte[]>(GetResourceStore(), "Models");
    }
}
