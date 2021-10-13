// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

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
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentMenuItem : MenuItem
    {
        public IconUsage? Icon { get; private set; }

        public FluentMenuItem(LocalisableString text, IconUsage? icon = null)
            : base(text)
        {
            Icon = icon;
        }

        public FluentMenuItem(LocalisableString text, Action action, IconUsage? icon = null)
            : base(text, action)
        {
            Icon = icon;
        }
    }

    public class DrawableFluentMenuItem : Menu.DrawableMenuItem
    {
        public new FluentMenuItem Item => (FluentMenuItem)base.Item;

        public bool ShowIcon
        {
            get => Content.ShowIcon;
            set => Content.ShowIcon = value;
        }

        protected bool IsPressed { get; private set; }

        protected ThemableEffectBox BackgroundBox { get; private set; }

        protected new Container Background => (Container)base.Background;

        protected new MenuItemContent Content => (MenuItemContent)base.Content;

        public DrawableFluentMenuItem(FluentMenuItem item)
            : base(item)
        {
            Masking = true;
            BackgroundColour = Colour4.White;
            BackgroundColourHover = Colour4.White;
        }

        protected override Drawable CreateBackground() => new Container
        {
            RelativeSizeAxes = Axes.Both,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Size = new Vector2(0.95f),
            Child = BackgroundBox = new ThemableEffectBox
            {
                RelativeSizeAxes = Axes.Both,
                CornerRadius = 5.0f,
            },
        };

        protected override Drawable CreateContent() => new MenuItemContent
        {
            Icon = Item.Icon,
        };

        protected override void UpdateBackgroundColour()
        {
            if (IsPressed)
                BackgroundBox.Colour = ThemeSlot.Gray30;
            else if (IsHovered)
                BackgroundBox.Colour = ThemeSlot.Gray20;
            else
                BackgroundBox.Colour = ThemeSlot.Transparent;
        }

        protected override void UpdateForegroundColour()
        {
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            IsPressed = true;
            UpdateBackgroundColour();
            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            IsPressed = false;
            UpdateBackgroundColour();
            base.OnMouseUp(e);
        }

        protected class MenuItemContent : GridContainer, IHasText
        {
            private readonly ThemableSpriteIcon icon;
            private readonly ThemableSpriteText text;
            private IconUsage? iconUsage;
            private bool showIcon;

            public LocalisableString Text
            {
                get => text.Text;
                set => text.Text = value;
            }

            public IconUsage? Icon
            {
                get => iconUsage;
                set
                {
                    if (value.Equals(iconUsage))
                        return;

                    if (value.HasValue)
                        icon.Icon = value.Value;

                    iconUsage = value;
                }
            }

            public bool ShowIcon
            {
                get => showIcon;
                set
                {
                    if (showIcon == value)
                        return;

                    showIcon = value;

                    icon.Alpha = showIcon ? 1 : 0;
                    text.Margin = showIcon ? new MarginPadding { Right = 18 } : new MarginPadding { Left = 12, Right = 18 };

                    ColumnDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Absolute, showIcon ? 36 : 0),
                        new Dimension(GridSizeMode.AutoSize),
                    };
                }
            }

            public MenuItemContent()
            {
                Height = 32;
                Padding = new MarginPadding { Horizontal = 5 };
                AutoSizeAxes = Axes.X;
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.Absolute, 0),
                    new Dimension(GridSizeMode.AutoSize),
                };
                Content = new Drawable[][]
                {
                    new Drawable[]
                    {
                        icon = new ThemableSpriteIcon
                        {
                            Size = new Vector2(12),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = ThemeSlot.Gray190,
                        },
                        text = new ThemableSpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Colour = ThemeSlot.Gray190,
                            Margin = new MarginPadding { Left = 12, Right = 18 },
                        },
                    }
                };
            }
        }
    }
}
