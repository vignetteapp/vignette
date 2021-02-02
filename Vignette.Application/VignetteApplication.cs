// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System.Drawing;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace Vignette.Application
{
    public class VignetteApplication : VignetteApplicationBase
    {
        private Bindable<Size> windowSize;

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
