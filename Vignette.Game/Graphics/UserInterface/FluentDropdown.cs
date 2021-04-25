// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentDropdown<T> : Dropdown<T>
    {
        private FluentDropdownHeader header;

        private FluentDropdownMenu menu;

        public DropdownStyle Style
        {
            get => header.Style;
            set => header.Style = value;
        }

        public FluentDropdown()
        {
            menu.StateChanged += s => header.MenuOpened = s == MenuState.Open;
        }

        protected override DropdownHeader CreateHeader()
            => header = new FluentDropdownHeader();
        protected override DropdownMenu CreateMenu()
            => menu = new FluentDropdownMenu();

        protected class FluentDropdownHeader : DropdownHeader
        {
            private ThemableMaskedBox background;

            private ThemableMaskedBox border;

            private ThemableSpriteIcon chevron;

            private DropdownStyle style;

            public DropdownStyle Style
            {
                get => style;
                set
                {
                    if (style == value)
                        return;

                    style = value;
                    Scheduler.AddOnce(updateStyle);
                }
            }

            private bool menuOpened;

            public bool MenuOpened
            {
                get => menuOpened;
                set
                {
                    if (menuOpened == value)
                        return;

                    menuOpened = value;
                    Scheduler.AddOnce(updateState);
                }
            }

            private ThemableSpriteText text;

            protected override LocalisableString Label
            {
                get => text.Text;
                set => text.Text = value;
            }

            public FluentDropdownHeader()
            {
                AutoSizeAxes = Axes.None;
                Height = 32;

                DisabledColour = Colour4.White;
                Background.Colour = Colour4.White;
                BackgroundColour = Colour4.White;
                BackgroundColourHover = Colour4.White;
                Background.Clear();

                Background.Children = new Drawable[]
                {
                    background = new ThemableMaskedBox
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    border = new ThemableMaskedBox(),
                };

                Foreground.Padding = new MarginPadding { Horizontal = 8 };

                AddRange(new Drawable[]
                {
                    text = new ThemableSpriteText
                    {
                        Font = SegoeUI.Regular.With(size: 16),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                    chevron = new ThemableSpriteIcon
                    {
                        Size = new Vector2(12),
                        Icon = FluentSystemIcons.ChevronDown16,
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Margin = new MarginPadding { Vertical = 5 },
                    },
                });

                Enabled.BindValueChanged(_ => updateStyle(), true);
            }

            private void updateState()
            {
                if (Enabled.Value)
                {
                    background.Colour = ThemeSlot.White;
                    chevron.Colour = ThemeSlot.Gray190;
                    text.Colour = ThemeSlot.Gray190;

                    if (Style == DropdownStyle.Bordered)
                    {
                        if (IsHovered && !menuOpened)
                            border.BorderColour = ThemeSlot.Gray160;
                        else if (menuOpened)
                            border.BorderColour = ThemeSlot.AccentPrimary;
                        else
                            border.BorderColour = ThemeSlot.Gray110;

                        border.BorderThickness = menuOpened ? 3.0f : 1.5f;
                    }

                    if (Style == DropdownStyle.Underlined)
                    {
                        if (IsHovered && !menuOpened)
                            border.Colour = ThemeSlot.Gray160;
                        else if (menuOpened)
                            border.Colour = ThemeSlot.AccentPrimary;
                        else
                            border.Colour = ThemeSlot.Gray110;
                    }
                }
                else
                {
                    border.BorderThickness = 0;
                    background.Colour = ThemeSlot.Gray30;
                    chevron.Colour = ThemeSlot.Gray90;
                    text.Colour = ThemeSlot.Gray90;
                }
            }

            private void updateStyle()
            {
                switch (Style)
                {
                    case DropdownStyle.Bordered:
                        background.CornerRadius = 2.5f;
                        border.Height = 1;
                        border.Colour = ThemeSlot.Transparent;
                        border.CornerRadius = 2.5f;
                        border.RelativeSizeAxes = Axes.Both;
                        break;

                    case DropdownStyle.Underlined:
                        background.CornerRadius = 0;
                        border.CornerRadius = 0;
                        border.RelativeSizeAxes = Axes.X;
                        border.Anchor = Anchor.BottomCentre;
                        border.Origin = Anchor.BottomCentre;
                        border.Height = 1.5f;
                        border.BorderColour = ThemeSlot.Transparent;
                        break;
                }

                updateState();
            }

            protected override bool OnHover(HoverEvent e)
            {
                updateState();
                return base.OnHover(e);
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                updateState();
                base.OnHoverLost(e);
            }

            protected override void OnFocus(FocusEvent e)
            {
                updateState();
                base.OnFocus(e);
            }

            protected override void OnFocusLost(FocusLostEvent e)
            {
                updateState();
                base.OnFocusLost(e);
            }
        }

        protected class FluentDropdownMenu : DropdownMenu
        {
            public FluentDropdownMenu()
            {
                ScrollbarVisible = false;
                BackgroundColour = Colour4.Transparent;
                ItemsContainer.Padding = new MarginPadding(1);
                AddInternal(new ThemableMaskedBox
                {
                    Depth = 1,
                    Colour = ThemeSlot.White,
                    BorderColour = ThemeSlot.Gray30,
                    CornerRadius = 1.5f,
                    BorderThickness = 1.5f,
                    RelativeSizeAxes = Axes.Both,
                });
            }

            protected override void AnimateOpen() => this.FadeIn(200, Easing.OutQuint);

            protected override void AnimateClose() => this.FadeOut(200, Easing.OutQuint);

            protected override void UpdateSize(Vector2 newSize)
            {
                if (Direction == Direction.Vertical)
                {
                    Width = newSize.X;
                    this.ResizeHeightTo(newSize.Y, 200, Easing.OutQuint);
                }
                else
                {
                    Height = newSize.Y;
                    this.ResizeWidthTo(newSize.X, 200, Easing.OutQuint);
                }
            }

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item)
                => new DrawableFluentDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction)
                => new FluentScrollContainer(direction);

            protected override Menu CreateSubMenu()
                => new FluentMenu(Direction.Vertical);
        }

        protected class DrawableFluentDropdownMenuItem : DropdownMenu.DrawableDropdownMenuItem
        {
            private ThemableBox background;

            public DrawableFluentDropdownMenuItem(MenuItem item)
                : base(item)
            {
                Masking = true;
                BackgroundColour = Colour4.White;
                BackgroundColourHover = Colour4.White;
                BackgroundColourSelected = Colour4.White;
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                background.Colour = IsSelected ? ThemeSlot.Gray30 : ThemeSlot.Transparent;
            }

            protected override Drawable CreateBackground()
                => background = new ThemableBox { RelativeSizeAxes = Axes.Both };

            protected override Drawable CreateContent()
                => new DropdownMenuContent();

            protected override void UpdateBackgroundColour()
            {
                if (isPressed || IsSelected)
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

            private class DropdownMenuContent : Container<ThemableSpriteText>, IHasText
            {
                public LocalisableString Text
                {
                    get => Child.Text;
                    set => Child.Text = value;
                }

                public DropdownMenuContent()
                {
                    Height = 32;
                    AutoSizeAxes = Axes.X;
                    Child = new ThemableSpriteText
                    {
                        Colour = ThemeSlot.Gray190,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Left = 8 },
                    };
                }
            }
        }
    }

    public enum DropdownStyle
    {
        Bordered,

        Underlined,
    }
}
