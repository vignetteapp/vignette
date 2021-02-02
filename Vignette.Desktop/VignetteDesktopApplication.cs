// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Platform;
using Vignette.Application;

namespace Vignette.Desktop
{
    public class VignetteDesktopApplication : VignetteApplication
    {
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            switch (host.Window)
            {
                case OsuTKDesktopWindow osuTKWindow:
                    osuTKWindow.SetIconFromStream(VignetteDesktopResources.Icon);
                    break;

                case SDL2DesktopWindow sdlWindow:
                    sdlWindow.SetIconFromStream(VignetteDesktopResources.Icon);
                    break;
            }
        }
    }
}
