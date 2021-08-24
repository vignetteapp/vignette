// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Overlays.MainMenu.Settings.Components
{
    public class SettingsLabel : SettingsItem
    {
        private readonly ThemableSpriteText text;

        public LocalisableString Text
        {
            get => text?.Text ?? string.Empty;
            set => text.Text = value;
        }

        public SettingsLabel()
        {
            Add(text = new ThemableSpriteText
            {
                Font = SegoeUI.Regular.With(size: 16),
                Colour = ThemeSlot.Gray190,
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
            });
        }
    }
}
