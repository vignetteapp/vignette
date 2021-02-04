// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Testing;

namespace Vignette.Application.Tests
{
    public class VignetteTestBrowser : VignetteApplicationBase
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();
            Add(new TestBrowser(@"Vignette"));
        }
    }
}
