// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Testing;
using Vignette.Game.Tests.Resources;

namespace Vignette.Game.Tests
{
    public class VisualTestGame : VignetteGameBase
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(TestResources.GetStore());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Add(new TestBrowser("Vignette"));
        }
    }
}
