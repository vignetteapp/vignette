using holotrack.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace holotrack.Graphics.Interface
{
    public class HoloTrackMenu : Menu
    {
        public HoloTrackMenu(Direction direction, bool topLevelMenu = false)
            : base(direction, topLevelMenu)
        {
            BackgroundColour = Colour4.Transparent;
        }

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item) => new DrawableHoloTrackMenuItem(item);

        protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new HoloTrackScrollContainer(direction);

        protected override Menu CreateSubMenu() => new HoloTrackMenu(Direction.Vertical);

        private class DrawableHoloTrackMenuItem : DrawableMenuItem
        {
            public DrawableHoloTrackMenuItem(MenuItem item)
                : base(item)
            {
                BackgroundColour = Colour4.Transparent;
                BackgroundColourHover = Colour4.Transparent;
            }

            protected override Drawable CreateContent() => new HoloTrackSpriteText
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Padding = new MarginPadding(2),
            };
        }
    }
}