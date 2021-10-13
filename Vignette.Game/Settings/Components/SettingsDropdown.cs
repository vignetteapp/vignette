// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Bindables;
using System.Collections.Generic;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsDropdown<T> : SettingsControl<FluentDropdown<T>, T>
    {
        public IEnumerable<T> Items
        {
            get => Control.Items;
            set => Control.Items = value;
        }

        public IBindableList<T> ItemSource
        {
            get => Control.ItemSource;
            set => Control.ItemSource = value;
        }

        protected override FluentDropdown<T> CreateControl() => new FluentDropdown<T> { Width = 125 };
    }
}
