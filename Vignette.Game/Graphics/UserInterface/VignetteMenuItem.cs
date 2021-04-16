// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;

namespace Vignette.Game.Graphics.UserInterface
{
    public class VignetteMenuItem : MenuItem
    {
        public VignetteMenuItem(LocalisableString text)
            : base(text)
        {
        }

        public VignetteMenuItem(LocalisableString text, Action action)
            : base(text, action)
        {
        }
    }
}
