// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Testing;
using Vignette.Game.IO;

namespace Vignette.Game.Tests
{
    public class VisualTestGame : VignetteGameBase
    {
        private TemporaryUserResources resources;

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));


        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.CacheAs<UserResources>(resources = new TemporaryUserResources(Host, Storage));
            Children = new Drawable[]
            {
                new TestBrowser("Vignette"),
                new CursorContainer(),
            };
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            resources?.Dispose();
        }
    }
}
