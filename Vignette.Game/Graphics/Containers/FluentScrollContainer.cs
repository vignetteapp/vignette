// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.Containers
{
    public class FluentScrollContainer : FluentScrollContainer<Drawable>
    {
        public FluentScrollContainer(Direction scrollDirection = Direction.Vertical)
            : base(scrollDirection)
        {
        }
    }

    public class FluentScrollContainer<T> : ScrollContainer<T>
        where T : Drawable
    {
        public FluentScrollContainer(Direction scrollDirection = Direction.Vertical)
            : base(scrollDirection)
        {
        }

        protected override ScrollbarContainer CreateScrollbar(Direction direction)
            => new FluentScrollbar(direction);

        protected class FluentScrollbar : ScrollbarContainer
        {
            public FluentScrollbar(Direction direction)
                : base(direction)
            {
                Child = new ThemableContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 2.5f,
                    Colour = ThemeSlot.Gray160,
                };
            }

            public override void ResizeTo(float val, int duration = 0, Easing easing = Easing.None)
            {
                var size = new Vector2(4) { [(int)ScrollDirection] = val };
                this.ResizeTo(size, duration, easing);
            }
        }
    }
}
