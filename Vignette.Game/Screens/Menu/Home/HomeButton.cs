// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu.Home
{
    public class HomeButton : FluentButtonBase
    {
        private ThemableMaskedBox border;

        private ThemableSpriteIcon icon;

        private ThemableSpriteText text;

        private ThemableSpriteText subText;

        public string Url { get; set; }

        public IconUsage Icon
        {
            get => icon.Icon;
            set => icon.Icon = value;
        }

        public LocalisableString Text
        {
            get => text.Text;
            set => text.Text = value;
        }

        public LocalisableString SubText
        {
            get => subText.Text;
            set => subText.Text = value;
        }

        public HomeButton()
        {
            Size = new Vector2(240, 80);
            BackgroundResting = ThemeSlot.Transparent;
            BackgroundHovered = ThemeSlot.Gray20;
            BackgroundPressed = ThemeSlot.Gray30;
            BackgroundDisabled = ThemeSlot.Transparent;
            Children = new Drawable[]
            {
                border = new ThemableMaskedBox
                {
                    BorderThickness = 3.0f,
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.Transparent,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(15),
                    Children = new Drawable[]
                    {
                        icon = new ThemableSpriteIcon
                        {
                            Colour = ThemeSlot.AccentPrimary,
                            Size = new Vector2(28),
                        },
                        new FillFlowContainer
                        {
                            Margin = new MarginPadding { Left = 40 },
                            Direction = FillDirection.Vertical,
                            Children = new Drawable[]
                            {
                                text = new ThemableSpriteText
                                {
                                    Font = SegoeUI.Regular.With(size: 24),
                                    Colour = ThemeSlot.Black,
                                },
                                subText = new ThemableSpriteText
                                {
                                    Font = SegoeUI.Regular.With(size: 16),
                                    Colour = ThemeSlot.Gray190,
                                }
                            },
                        }
                    }
                }
            };
        }

        protected override void UpdateBackground(ThemeSlot slot)
            => border.BorderColour = slot;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            if (!string.IsNullOrEmpty(Url) && Uri.IsWellFormedUriString(Url, UriKind.Absolute))
                Action = () => host.OpenUrlExternally(Url);
        }
    }
}
