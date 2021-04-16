// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using Vignette.Game.Graphics.Themes;

namespace Vignette.Game.Graphics.Containers
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

        protected override ScrollbarContainer CreateScrollbar(Direction direction) => new Scroll(direction);

        protected class Scroll : ScrollbarContainer
        {
            private readonly Box box;

            private Bindable<Theme> theme;

            public Scroll(Direction direction)
                : base(direction)
            {
                Child = box = new Box { RelativeSizeAxes = Axes.Both };
                Masking = true;
                CornerRadius = 5;
            }

            [BackgroundDependencyLoader]
            private void load(Bindable<Theme> t)
            {
                theme = t.GetBoundCopy();
                theme.BindValueChanged(e => box.Colour = e.NewValue.NeutralPrimaryAlt, true);
            }

            public override void ResizeTo(float val, int duration = 0, Easing easing = Easing.None)
            {
                Vector2 size = new Vector2(6)
                {
                    [(int)ScrollDirection] = val,
                };
                this.ResizeTo(size, duration, easing);
            }
        }
    }
}
