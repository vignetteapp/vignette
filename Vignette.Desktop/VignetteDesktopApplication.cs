// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Reflection;
using osu.Framework.Platform;
using Vignette.Application;
using Vignette.Application.Configuration;

namespace Vignette.Desktop
{
    public class VignetteDesktopApplication : VignetteApplication
    {
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            var icon = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "app.ico");
            var resizable = LocalConfig.GetBindable<bool>(ApplicationSetting.WindowResizable);

            switch (host.Window)
            {
                case SDL2DesktopWindow sdlWindow:
                    sdlWindow.Title = Name;
                    sdlWindow.Resizable = resizable.Value;
                    sdlWindow.CursorStateBindable.Value |= CursorState.Default;
                    sdlWindow.SetIconFromStream(icon);
                    break;
            }
        }
    }
}
