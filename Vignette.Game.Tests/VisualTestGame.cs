// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
