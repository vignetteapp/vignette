// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsTextBox : SettingsExpandedControl<FluentTextBox, string>
    {
        protected override FluentTextBox CreateControl() => new FluentTextBox { RelativeSizeAxes = Axes.X };
    }
}
