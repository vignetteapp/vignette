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
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            if (host.Window is not SDL2DesktopWindow window)
                return;

            window.ConfineMouseMode.Value = ConfineMouseMode.Never;
            window.DragDrop += f => FileDropped(new[] { f });
            window.Title = Name;

            string icon = IsInsidersBuild ? "vignette-insiders.ico" : "vignette.ico";
            window.SetIconFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), icon));
        }
    }
}
