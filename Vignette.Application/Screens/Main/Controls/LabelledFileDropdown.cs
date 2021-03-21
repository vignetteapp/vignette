// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Localisation;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.IO.Monitors;

namespace Vignette.Application.Screens.Main.Controls
{
    public class LabelledFileDropdown<T> : LabelledDropdown<MonitoredFile<T>>
        where T : class
    {
        protected override Drawable CreateControl() => new LabelledFileDropdownControl();

        protected class LabelledFileDropdownControl : ThemedDropdown<MonitoredFile<T>>
        {
            protected override LocalisableString GenerateItemText(MonitoredFile<T> item)
            {
                return item.Name;
            }
        }
    }
}
