// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Testing;

namespace Vignette.Game.Tests
{
    public class VisualTestGame : VignetteGameBase
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();
            Add(new TestBrowser("Vignette"));
        }
    }
}
