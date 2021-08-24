// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public class SettingsButton : SettingsItem
    {
        public Action Action
        {
            get => Control.Action;
            set => Control.Action = value;
        }

        protected new FluentButton Control => (FluentButton)base.Control;

        protected override Drawable CreateControl() => new FluentButton
        {
            Icon = FluentSystemIcons.WindowNew24,
            Style = ButtonStyle.Text,
            Width = 32,
        };
    }
}
