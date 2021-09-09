// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings.Components
{
    public class SettingsButton : SettingsItem
    {
        public Action Action
        {
            get => button.Action;
            set => button.Action = value;
        }

        public LocalisableString ButtonText
        {
            get => button.Text;
            set => button.Text = value;
        }

        private readonly FluentButton button;

        public SettingsButton()
        {
            Foreground.Add(button = new FluentButton
            {
                Width = 150,
                Style = ButtonStyle.Primary,
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
            });
        }
    }
}
