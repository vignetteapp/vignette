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
        private ThemableEffectBox background;

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
            Size = new Vector2(320, 80);
            BackgroundResting = ThemeSlot.White;
            BackgroundHovered = ThemeSlot.Gray20;
            BackgroundPressed = ThemeSlot.Gray30;
            BackgroundDisabled = ThemeSlot.Transparent;
            Children = new Drawable[]
            {
                background = new ThemableEffectBox
                {
                    Shadow = true,
                    RelativeSizeAxes = Axes.Both,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(15),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Size = new Vector2(80),
                            Child = icon = new ThemableSpriteIcon
                            {
                                Colour = ThemeSlot.AccentPrimary,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Size = new Vector2(28),
                            },
                        },
                        new FillFlowContainer
                        {
                            Margin = new MarginPadding { Left = 100 },
                            AutoSizeAxes = Axes.X,
                            RelativeSizeAxes = Axes.Y,
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
            => background.Colour = slot;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            if (!string.IsNullOrEmpty(Url) && Uri.IsWellFormedUriString(Url, UriKind.Absolute))
                Action = () => host.OpenUrlExternally(Url);
        }
    }
}
