// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class FluentMenu : Menu
    {
        public FluentMenu(Direction direction, bool topLevelMenu = false)
            : base(direction, topLevelMenu)
        {
            ScrollbarVisible = false;
            BackgroundColour = Colour4.Transparent;
            ItemsContainer.Padding = new MarginPadding(1);

            AddInternal(new ThemableEffectBox
            {
                Depth = 1,
                Colour = ThemeSlot.White,
                Shadow = true,
                BorderColour = ThemeSlot.Gray30,
                CornerRadius = 5.0f,
                RelativeSizeAxes = Axes.Both,
            });
        }

        public override void Add(MenuItem item)
        {
            base.Add(item);

            var drawableMenuItems = ItemsContainer.Children.OfType<DrawableFluentMenuItem>();
            bool hasIcon = drawableMenuItems.Any(d => d.Item.Icon != null);
            drawableMenuItems.ForEach(d => d.ShowIcon = hasIcon);
        }

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item)
        {
            switch (item)
            {
                case FluentMenuItem menuItem:
                    return new DrawableFluentMenuItem(menuItem);

                case FluentMenuDivider divider:
                    return new DrawableFluentMenuDivider(divider);

                case FluentMenuHeader header:
                    return new DrawableFluentMenuHeader(header);

                default:
                    throw new NotSupportedException();
            }
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

        protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction)
            => new FluentScrollContainer(direction);

        protected override Menu CreateSubMenu() => new FluentMenu(Direction.Vertical)
        {
            Anchor = Direction == Direction.Horizontal ? Anchor.BottomLeft : Anchor.TopRight,
        };
    }
}
