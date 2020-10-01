using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace vignette.Graphics.Interface
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

        protected override ScrollbarContainer CreateScrollbar(Direction direction) => new VignetteScrollBar(direction);

        private class VignetteScrollBar : ScrollbarContainer
        {
            public VignetteScrollBar(Direction direction)
                : base(direction)
            {
                Masking = true;
                CornerRadius = 5;

                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = VignetteColor.Darker,
                };
            }

            public override void ResizeTo(float val, int duration = 0, Easing easing = Easing.None)
            {
                Vector2 size = new Vector2(8)
                {
                    [(int)ScrollDirection] = val
                };
                this.ResizeTo(size, duration, easing);
            }
        }
    }
}