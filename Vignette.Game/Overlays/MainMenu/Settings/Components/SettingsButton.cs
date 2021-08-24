// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Themeing;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public class SettingsButton : SettingsItem
    {
        public Action Action
        {
            get => button.Action;
            set => button.Action = value;
        }

        public IconUsage Icon
        {
            get => icon.Icon;
            set => icon.Icon = value;
        }

        private readonly FluentButton button;
        private readonly ThemableSpriteIcon icon;

        public SettingsButton()
        {
            AddInternal(button = new FluentButton
            {
                Style = ButtonStyle.Text,
                Depth = 1,
                Height = 1,
                RelativeSizeAxes = Axes.Both,
            });

            Add(icon = new ThemableSpriteIcon
            {
                Size = new Vector2(16),
                Colour = ThemeSlot.Gray190,
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
            });
        }
    }
}
