// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Configuration.Settings.Components
{
    public class LabelledDropdown<T> : LabelledControl<T>
    {
        protected new VignetteDropdown<T> Control => (VignetteDropdown<T>)base.Control;

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

        protected override Drawable CreateControl() => new VignetteDropdown<T>();
    }
}
