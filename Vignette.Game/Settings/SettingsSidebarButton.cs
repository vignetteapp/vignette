// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Settings
{
    public class SettingsSidebarButton : FluentButtonBase
    {
        public readonly BindableBool Active = new BindableBool();

        public readonly SettingsSection Section;

        public Action<SettingsSection> SelectionRequested;

        private readonly ThemableBox background;
        private readonly ThemableCircle highlight;
        private readonly ThemableSpriteIcon icon;

        public SettingsSidebarButton(SettingsSection section)
        {
            Section = section;

            Size = new Vector2(40);
            Masking = true;
            CornerRadius = 5;

            Children = new Drawable[]
            {
                background = new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                highlight = new ThemableCircle
                {
                    RelativeSizeAxes = Axes.Y,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Size = new Vector2(4, 0),
                },
                icon = new ThemableSpriteIcon
                {
                    Icon = section.Icon,
                    Size = new Vector2(16),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            };

            BackgroundResting = ThemeSlot.Transparent;
            BackgroundHovered = ThemeSlot.Gray30;
            BackgroundPressed = ThemeSlot.Gray40;
            BackgroundDisabled = ThemeSlot.Transparent;

            LabelResting = ThemeSlot.Black;
            LabelDisabled = ThemeSlot.Gray60;

            Active.BindValueChanged(_ => handleActiveChange(), true);
            Action = () => SelectionRequested?.Invoke(Section);
        }

        protected override void UpdateBackground(ThemeSlot slot)
        {
            highlight.Colour = Enabled.Value ? ThemeSlot.AccentPrimary : ThemeSlot.AccentDarker;

            if (!Active.Value)
                background.Colour = slot;
        }

        protected override void UpdateLabel(ThemeSlot slot) => icon.Colour = slot;

        private void handleActiveChange()
        {
            highlight.ResizeHeightTo(Active.Value ? 0.4f : 0, 200, Easing.OutQuint);
            background.Colour = Active.Value
                ? Enabled.Value ? ThemeSlot.Gray40 : ThemeSlot.Gray10
                : BackgroundResting;
        }
    }
}
