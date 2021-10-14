// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsSwitch : SettingsControl<FluentSwitch, bool>
    {
        protected override FluentSwitch CreateControl() => new FluentSwitch();
    }
}
