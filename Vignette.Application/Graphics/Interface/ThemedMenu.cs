// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.Graphics.Sprites;

namespace Vignette.Application.Graphics.Interface
{
    public class ThemedMenu : Menu
    {
        public ThemedMenu(Direction direction, bool topLevelMenu = false)
            : base(direction, topLevelMenu)
        {
            BackgroundColour = Colour4.Transparent;
        }

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item) => new DrawableVignetteMenuItem(item);

        protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new ThemedScrollContainer(direction);

        protected override Menu CreateSubMenu() => new ThemedMenu(Direction.Vertical)
        {
            Anchor = Direction == Direction.Horizontal ? Anchor.BottomLeft : Anchor.TopRight
        };

        private class DrawableVignetteMenuItem : DrawableMenuItem
        {
            public DrawableVignetteMenuItem(MenuItem item)
                : base(item)
            {
            }

            protected override Drawable CreateContent() => new ThemedSpriteText
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Padding = new MarginPadding(2),
            };
        }
    }
}
