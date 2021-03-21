// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Screens.Main
{
    public class NavigationButtonIcon : NavigationButton
    {
        public IconUsage Icon
        {
            get => SpriteIcon.Icon;
            set => SpriteIcon.Icon = value;
        }

        public LocalisableString Text
        {
            get => SpriteText.Text;
            set => SpriteText.Text = value.ToString().ToUpperInvariant();
        }

        protected new ThemedSpriteIcon SpriteIcon => (ThemedSpriteIcon)base.SpriteIcon;

        protected new ThemedSpriteText SpriteText => (ThemedSpriteText)base.SpriteText;

        protected override Drawable CreateIcon() => new ThemedSpriteIcon
        {
            Size = new Vector2(14),
            ThemeColour = ThemeColour.NeutralPrimary,
        };

        protected override Drawable CreateText() => new ThemedSpriteText
        {
            Font = SegoeUI.Bold.With(size: 16),
            ThemeColour = ThemeColour.NeutralPrimary,
        };
    }
}
