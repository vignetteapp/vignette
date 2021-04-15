// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.IO;
using osu.Framework.Platform;
using Vignette.Game.IO;
using Vignette.Game.Tests.Resources;

namespace Vignette.Game.Tests
{
    public class TemporaryUserResources : UserResources
    {
        protected new TemporaryNativeStorage Storage => base.Storage as TemporaryNativeStorage;

        public TemporaryUserResources(GameHost host, Storage defaultStorage)
            : base(host, new TemporaryNativeStorage(defaultStorage.GetFullPath("dev", true), host))
        {
            copyTestResource("test-wallpaper.jpg", "images");
        }

        private void copyTestResource(string filename, string subPath = @"")
        {
            using var i = TestResources.GetStream(filename);
            using var o = Storage.GetStream(Path.Combine(subPath, filename), FileAccess.Write);
            i.CopyTo(o);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Storage?.Dispose();
        }
    }
}
