// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu
{
    public class MainMenuNavigation : NavigationViewVertical<MenuScreen>
    {
        public Action OnSettings { get; set; }

        protected override NavigationViewTabControl CreateTabControl()
            => new MainMenuNavigationTabControl();

        protected override Drawable CreateEndControl()
            => new SettingsButton { Action = () => OnSettings?.Invoke() };

        protected class MainMenuNavigationTabControl : NavigationViewVerticalTabControl
        {
            protected override TabItem<MenuScreen> CreateTabItem(MenuScreen value)
                => new MainMenuNavigationTabItem(value);
        }

        protected class MainMenuNavigationTabItem : NavigationViewVerticalTabItem
        {
            protected override LocalisableString? Text => Value.Title;

            protected override IconUsage? Icon => Value.Icon;

            public MainMenuNavigationTabItem(MenuScreen value)
                : base(value)
            {
            }
        }

        private class SettingsButton : FluentButtonBase
        {
            private readonly ThemableBox background;

            public SettingsButton()
            {
                Height = 44;
                RelativeSizeAxes = Axes.X;

                BackgroundResting = ThemeSlot.Transparent;
                BackgroundHovered = ThemeSlot.Gray20;
                BackgroundPressed = ThemeSlot.Gray30;

                Children = new Drawable[]
                {
                    background = new ThemableBox
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    new ThemableSpriteIcon
                    {
                        Icon = FluentSystemIcons.Settings24,
                        Size = new Vector2(16),
                        Colour = ThemeSlot.Black,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Horizontal = 12 },
                    },
                    new ThemableSpriteText
                    {
                        Text = "Settings",
                        Font = SegoeUI.Regular.With(size: 18),
                        Colour = ThemeSlot.Black,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Left = 47 },
                    },
                };
            }

            protected override void UpdateBackground(ThemeSlot slot)
                => background.Colour = slot;
        }
    }
}
