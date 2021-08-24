// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public class SettingsDropdown<T> : SettingsItem<T>
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

        protected new FluentDropdown<T> Control => (FluentDropdown<T>)base.Control;

        protected override Drawable CreateControl() => new FluentDropdown<T> { Width = 200 };
    }
}
