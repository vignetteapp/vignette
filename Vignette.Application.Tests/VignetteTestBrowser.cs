// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Testing;
using Vignette.Application.Graphics;

namespace Vignette.Application.Tests
{
    public class VignetteTestBrowser : VignetteApplicationBase
    {
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        private DependencyContainer dependencies;

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.Cache(new VignetteColour());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Add(new TestBrowser(@"Vignette"));
        }
    }
}
