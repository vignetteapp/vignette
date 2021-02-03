// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Platform;

namespace Vignette.Application
{
    public class VignetteApplication : VignetteApplicationBase
    {
        public VignetteApplication()
        {
            Name = @"Vignette";
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.Title = Name;
        }
    }
}
