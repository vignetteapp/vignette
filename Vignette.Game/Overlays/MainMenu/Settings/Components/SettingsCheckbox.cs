// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public class SettingsCheckbox : SettingsControl<bool>
    {
        protected override Drawable CreateControl() => new FluentCheckbox();
    }
}
