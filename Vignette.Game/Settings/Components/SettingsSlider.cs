// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsSlider<T> : SettingsExpandedControl<FluentSlider<T>, T>
        where T : struct, IComparable<T>, IConvertible, IEquatable<T>
    {
        protected override FluentSlider<T> CreateControl() => new FluentSlider<T> { RelativeSizeAxes = Axes.X };
    }
}
