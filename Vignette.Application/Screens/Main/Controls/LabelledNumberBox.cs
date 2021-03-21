// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Screens.Main.Controls
{
    public class LabelledNumberBox : LabelledControl<string>
    {
        protected new NumberBox Control => (NumberBox)base.Control;

        protected override Drawable CreateControl() => new NumberBox();
    }
}
