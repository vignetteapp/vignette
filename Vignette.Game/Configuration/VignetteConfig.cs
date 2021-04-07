// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.ComponentModel;
using Newtonsoft.Json;
using osu.Framework.Bindables;
using Vignette.Game.Configuration.Models;

namespace Vignette.Game.Configuration
{
    [Serializable]
    public class VignetteConfig
    {
        public Bindable<bool> WindowResizable = new Bindable<bool>();

        public Bindable<bool> ShowFpsOverlay = new Bindable<bool>();

        public Bindable<string> CameraDevice = new Bindable<string>();

        public BackgroundConfig Background = new BackgroundConfig();
    }
}
