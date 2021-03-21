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
        protected override void LoadComplete()
        {
            base.LoadComplete();

            var resizableBindable = LocalConfig.GetBindable<bool>(ApplicationSetting.WindowResizable);
            resizableBindable.ValueChanged += e =>
            {

            };

            resizableBindable.TriggerChange();
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            var icon = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "app.ico");

            switch (host.Window)
            {
                case SDL2DesktopWindow sdlWindow:
                    sdlWindow.Title = Name;
                    sdlWindow.CursorStateBindable.Value |= CursorState.Default;
                    sdlWindow.SetIconFromStream(icon);
                    break;
            }
        }
    }
}
