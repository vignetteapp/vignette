// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Screens.Main.Menu.Settings
{
    public class SettingsCheckbox : SettingsItem<bool>
    {
        protected override Drawable CreateControl() => new FluentCheckbox();
    }
}
