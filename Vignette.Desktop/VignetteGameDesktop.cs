// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Reflection;
using osu.Framework.Bindables;
using osu.Framework.Input;
using osu.Framework.Platform;
using Vignette.Game;
using Vignette.Game.Configuration;

namespace Vignette.Desktop
{
    public class VignetteGameDesktop : VignetteGame
    {
        private IBindable<bool> resizable;

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            if (host.Window is not SDL2DesktopWindow window)
                return;

            window.ConfineMouseMode.Value = ConfineMouseMode.Never;
            window.Title = Name;
            window.SetIconFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "vignette.ico"));

            resizable = LocalConfig.GetBindable<bool>(VignetteSetting.WindowResizable);
            resizable.BindValueChanged(e => window.Resizable = e.NewValue, true);
        }
    }
}
