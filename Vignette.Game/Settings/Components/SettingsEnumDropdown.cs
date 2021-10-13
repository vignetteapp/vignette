// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsEnumDropdown<T> : SettingsDropdown<T>
        where T : struct, Enum
    {
        protected override FluentDropdown<T> CreateControl() => new FluentEnumDropdown<T> { Width = 125 };
    }
}
