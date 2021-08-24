// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public class SettingsSlider<T> : SettingsControl<T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        protected override Drawable CreateControl() => new FluentSlider<T> { Width = 200 };
    }
}
