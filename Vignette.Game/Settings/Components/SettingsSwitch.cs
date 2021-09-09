// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsSwitch : SettingsControl<FluentSwitch, bool>
    {
        protected override FluentSwitch CreateControl() => new FluentSwitch();
    }
}
