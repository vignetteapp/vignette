// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Reflection;
using osu.Framework.Configuration;
using osu.Framework.Input;
using osu.Framework.Platform;
using Vignette.Game;

namespace Vignette.Desktop
{
    public class VignetteGameDesktop : VignetteGame
    {
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            if (host.Window is not SDL2DesktopWindow window)
                return;

            window.ConfineMouseMode.Value = ConfineMouseMode.Never;
            window.WindowMode.Value = WindowMode.Windowed;
            window.Resizable = false;
            window.Title = Name;
            window.SetIconFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "vignette.ico"));

            var resizable = LocalConfig.Config.WindowResizable.GetBoundCopy();
            resizable.BindValueChanged(e => window.Resizable = e.NewValue, true);
        }
    }
}
