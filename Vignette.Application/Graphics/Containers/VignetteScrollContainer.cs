// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Application.Graphics.Shapes;
using Vignette.Application.Graphics.Themes;

namespace Vignette.Application.Graphics.Containers
{
    public class VignetteScrollContainer : VignetteScrollContainer<Drawable>
    {
        public VignetteScrollContainer(Direction scrollDirection = Direction.Vertical)
            : base(scrollDirection)
        {
        }
    }

    public class VignetteScrollContainer<T> : ScrollContainer<T>
        where T : Drawable
    {
        public VignetteScrollContainer(Direction scrollDirection = Direction.Vertical)
            : base(scrollDirection)
        {
        }

        protected override ScrollbarContainer CreateScrollbar(Direction direction) => new VignetteScrollbar(direction);

        private class VignetteScrollbar : ScrollbarContainer
        {
            public VignetteScrollbar(Direction direction)
                : base(direction)
            {
                Child = new VignetteBox
                {
                    RelativeSizeAxes = Axes.Both,
                    ThemeColour = ThemeColour.NeutralPrimary,
                };
            }

            public override void ResizeTo(float val, int duration = 0, Easing easing = Easing.None)
            {
                this.ResizeTo(new Vector2(8) { [(int)ScrollDirection] = val }, duration, easing);
            }
        }
    }
}
