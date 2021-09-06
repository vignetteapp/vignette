// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using System;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Settings
{
    public class SettingsInteractableItem : SettingsItem
    {
        protected Action Action
        {
            get => button.Action;
            set => button.Action = value;
        }

        private SettingsInteractableItemBackground button;

        protected override Drawable CreateBackground() => button = new SettingsInteractableItemBackground();

        private class SettingsInteractableItemBackground : FluentButtonBase
        {
            private readonly ThemableBox background;

            public SettingsInteractableItemBackground()
            {
                RelativeSizeAxes = Axes.Both;

                InternalChild = background = new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                };

                BackgroundResting = ThemeSlot.Gray30;
                BackgroundPressed = ThemeSlot.Gray60;
                BackgroundHovered = ThemeSlot.Gray50;
                BackgroundDisabled = ThemeSlot.Gray30;
            }

            protected override void UpdateBackground(ThemeSlot slot) => background.Colour = slot;
        }
    }
}
