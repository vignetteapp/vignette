// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsTextBox : SettingsExpandedControl<FluentTextBox, string>
    {
        protected override FluentTextBox CreateControl() => new FluentTextBox { RelativeSizeAxes = Axes.X };
    }
}
