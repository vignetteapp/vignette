// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public class SettingsEnumDropdown<T> : SettingsItem<T>
        where T : struct, Enum
    {
        protected override Drawable CreateControl() => new FluentEnumDropdown<T> { Width = 200 };
    }
}
