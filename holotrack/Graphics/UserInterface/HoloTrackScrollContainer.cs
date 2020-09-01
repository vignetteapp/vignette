using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace holotrack.Graphics.UserInterface
{
    public class HoloTrackScrollContainer : HoloTrackScrollContainer<Drawable>
    {
        public HoloTrackScrollContainer(Direction scrollDirection = Direction.Vertical)
            : base(scrollDirection)
        {
        }
    }

    public class HoloTrackScrollContainer<T> : ScrollContainer<T>
        where T : Drawable
    {
        public HoloTrackScrollContainer(Direction scrollDirection = Direction.Vertical)
            : base(scrollDirection)
        {
        }

        protected override ScrollbarContainer CreateScrollbar(Direction direction) => new BasicScrollbar(direction);

        private class BasicScrollbar : ScrollbarContainer
        {
            public BasicScrollbar(Direction direction)
                : base(direction)
            {
                Masking = true;
                CornerRadius = 5;

                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Gray,
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