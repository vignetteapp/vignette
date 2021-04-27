// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class NavigationView<T> : Container
    {
        public IReadOnlyList<T> Items
        {
            get => Control.Items;
            set => Control.Items = value;
        }

        protected NavigationViewTabControl Control;

        public NavigationView()
        {
            Control = CreateTabControl().With(d =>
            {
                d.RelativeSizeAxes = Axes.Both;
            });

            Add(new ThemableBox
            {
                Colour = ThemeSlot.White,
                RelativeSizeAxes = Axes.Both,
            });
        }

        protected abstract NavigationViewTabControl CreateTabControl();

        protected abstract class NavigationViewTabControl : TabControl<T>
        {
            protected override Dropdown<T> CreateDropdown() => null;
        }

        protected abstract class NavigationViewTabItem : TabItem<T>
        {
            private readonly ThemableBox background;

            protected readonly ThemableBox Highlight;

            protected readonly ThemableSpriteText Text;

            protected readonly ThemableSpriteIcon Icon;

            protected readonly Container LabelContainer;

            protected virtual bool ForceTextMargin => true;

            protected virtual float TextLeftMargin => 35;

            public NavigationViewTabItem(T value)
                : base(value)
            {
                Highlight = new ThemableBox { Colour = ThemeSlot.AccentPrimary };

                Add(background = new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.Transparent,
                });

                Add(LabelContainer = new Container
                {
                    Margin = new MarginPadding { Horizontal = 12 },
                    AutoSizeAxes = Axes.X,
                    RelativeSizeAxes = Axes.Y,
                });

                if (value is IHasIcon valueIcon)
                {
                    LabelContainer.Add(Icon = new ThemableSpriteIcon
                    {
                        Icon = valueIcon.Icon,
                        Size = new Vector2(16),
                        Colour = ThemeSlot.Black,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                if (value is IHasText valueText)
                {
                    LabelContainer.Add(Text = new ThemableSpriteText
                    {
                        Text = valueText.Text,
                        Font = SegoeUI.Regular.With(size: 18),
                        Colour = ThemeSlot.Black,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                if (Icon != null || ForceTextMargin)
                    Text.Margin = new MarginPadding { Left = TextLeftMargin };

                updateBackground();
            }

            private void updateBackground()
            {
                if (isPressed)
                    background.Colour = ThemeSlot.Gray30;
                else if (IsHovered)
                    background.Colour = ThemeSlot.Gray20;
                else
                    background.Colour = ThemeSlot.Transparent;
            }

            private bool isPressed;

            protected override bool OnMouseDown(MouseDownEvent e)
            {
                isPressed = true;
                updateBackground();
                return base.OnMouseDown(e);
            }

            protected override void OnMouseUp(MouseUpEvent e)
            {
                isPressed = false;
                updateBackground();
                base.OnMouseUp(e);
            }

            protected override bool OnHover(HoverEvent e)
            {
                updateBackground();
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                updateBackground();
                base.OnHoverLost(e);
            }
        }
    }
}
