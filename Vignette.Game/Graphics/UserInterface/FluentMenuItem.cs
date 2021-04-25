// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentMenuItem : MenuItem
    {
        public IconUsage Icon { get; private set; }

        public FluentMenuItem(LocalisableString text, IconUsage icon = default)
            : base(text)
        {
            Icon = icon;
        }

        public FluentMenuItem(LocalisableString text, Action action, IconUsage icon = default)
            : base(text, action)
        {
            Icon = icon;
        }
    }

    public class DrawableFluentMenuItem : FluentMenu.DrawableMenuItem
    {
        private MenuItemContent content;

        private ThemableBox background;

        public DrawableFluentMenuItem(FluentMenuItem item)
            : base(item)
        {
            Masking = true;
            content.Icon = item.Icon;
            BackgroundColour = Colour4.White;
            BackgroundColourHover = Colour4.White;
        }

        protected override Drawable CreateBackground()
            => background = new ThemableBox { RelativeSizeAxes = Axes.Both };

        protected override Drawable CreateContent()
            => content = new MenuItemContent();

        protected override void UpdateBackgroundColour()
        {
            if (isPressed)
                background.Colour = ThemeSlot.Gray30;
            else if (IsHovered)
                background.Colour = ThemeSlot.Gray20;
            else
                background.Colour = ThemeSlot.Transparent;
        }

        protected override void UpdateForegroundColour()
        {
        }

        private bool isPressed;

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            isPressed = true;
            UpdateBackgroundColour();
            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            isPressed = false;
            UpdateBackgroundColour();
            base.OnMouseUp(e);
        }

        protected class MenuItemContent : FillFlowContainer, IHasText, IHasIcon
        {

            private readonly ThemableSpriteIcon icon;

            private readonly ThemableSpriteText text;

            public LocalisableString Text
            {
                get => text.Text;
                set => text.Text = value;
            }

            public IconUsage Icon
            {
                get => icon.Icon;
                set => icon.Icon = value;
            }

            public MenuItemContent()
            {
                Height = 32;
                AutoSizeAxes = Axes.X;
                Direction = FillDirection.Horizontal;
                Children = new Drawable[]
                {
                    icon = new ThemableSpriteIcon
                    {
                        Size = new Vector2(16),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Colour = ThemeSlot.Gray190,
                        Margin = new MarginPadding(8),
                    },
                    text = new ThemableSpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Colour = ThemeSlot.Gray190,
                        Margin = new MarginPadding { Right = 16 },
                    },
                };
            }
        }
    }
}
