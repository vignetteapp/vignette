// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public abstract class NavigationView<T> : Container
    {
        public Bindable<T> Current
        {
            get => Control.Current;
            set => Control.Current = value;
        }

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
        }

        protected abstract NavigationViewTabControl CreateTabControl();

        protected abstract class NavigationViewTabControl : TabControl<T>
        {
            protected override Dropdown<T> CreateDropdown() => null;
        }

        protected abstract class NavigationViewTabItem : TabItem<T>
        {
            private readonly ThemableBox background;

            private ThemableSpriteText text;

            private ThemableSpriteIcon icon;

            protected virtual LocalisableString? Text => null;

            protected virtual IconUsage? Icon => null;

            protected readonly ThemableBox Highlight;

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

                if (Icon != null)
                {
                    LabelContainer.Add(icon = new ThemableSpriteIcon
                    {
                        Icon = Icon ?? default,
                        Size = new Vector2(16),
                        Colour = ThemeSlot.Black,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                if (Text != null)
                {
                    LabelContainer.Add(text = new ThemableSpriteText
                    {
                        Text = Text ?? string.Empty,
                        Font = SegoeUI.Regular.With(size: 18),
                        Colour = ThemeSlot.Black,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    });
                }

                if ((Icon != null && Text != null) || ForceTextMargin)
                    text.Margin = new MarginPadding { Left = TextLeftMargin };

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
