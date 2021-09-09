// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentDropdownMenu : FluentMenu
    {
        public Drawable SelectedDrawableItem => DrawableMenuItems.FirstOrDefault(d => d.IsSelected);

        protected IEnumerable<DrawableFluentDropdownMenuItem> DrawableMenuItems => Children.OfType<DrawableFluentDropdownMenuItem>();

        private FluentDropdown dropdown;

        public FluentDropdownMenu(FluentDropdown dropdown)
            : base(Direction.Vertical)
        {
            this.dropdown = dropdown;
        }

        public void SelectItem(MenuItem item)
        {
            DrawableMenuItems.ForEach(d =>
            {
                d.IsSelected = d.Item == item;
                if (d.IsSelected)
                    ContentContainer.ScrollIntoView(d, false);
            });
        }

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item)
        {
            switch (item)
            {
                case FluentMenuItem menuItem:
                    return new DrawableFluentDropdownMenuItem(menuItem);

                default:
                    return base.CreateDrawableMenuItem(item);
            }
        }

        protected override sealed void AnimateClose() => this.FadeOut();

        protected override sealed void UpdateSize(Vector2 newSize)
        {
            Width = MathF.Max(dropdown.DrawWidth, newSize.X);
            this.ResizeHeightTo(newSize.Y, 200, Easing.OutQuint);
        }

        protected class DrawableFluentDropdownMenuItem : DrawableFluentMenuItem
        {
            public bool IsSelected
            {
                get => !Item.Action.Disabled && isSelected;
                set
                {
                    if (value == isSelected)
                        return;

                    isSelected = value;
                    UpdateBackgroundColour();
                }
            }

            private bool isSelected;
            private readonly ThemableCircle highlight;

            public DrawableFluentDropdownMenuItem(FluentMenuItem item)
                : base(item)
            {
                Background.Add(highlight = new ThemableCircle
                {
                    Width = 3,
                    Alpha = IsSelected ? 1 : 0,
                    Height = 0.5f,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Colour = ThemeSlot.AccentPrimary,
                    RelativeSizeAxes = Axes.Y,
                });
            }

            protected override void UpdateBackgroundColour()
            {
                if (IsPressed || IsSelected)
                    BackgroundBox.Colour = ThemeSlot.Gray30;
                else if (IsHovered)
                    BackgroundBox.Colour = ThemeSlot.Gray20;
                else
                    BackgroundBox.Colour = ThemeSlot.Transparent;

                highlight?.FadeTo(IsSelected ? 1 : 0);
            }
        }
    }
}
