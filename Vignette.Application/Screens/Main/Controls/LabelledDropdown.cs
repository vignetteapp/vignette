// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Screens.Main.Controls
{
    public class LabelledDropdown<T> : LabelledControl<T>
    {
        protected new ThemedDropdown<T> Control => (ThemedDropdown<T>)base.Control;

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

        protected override Drawable CreateControl() => new ThemedDropdown<T>();
    }
}
