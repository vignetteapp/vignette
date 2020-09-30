using vignette.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace vignette.Graphics.Interface
{
    public class VignetteMenu : Menu
    {
        public VignetteMenu(Direction direction, bool topLevelMenu = false)
            : base(direction, topLevelMenu)
        {
            BackgroundColour = Colour4.Transparent;
        }

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item) => new DrawableVignetteMenuItem(item);

        protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new VignetteScrollContainer(direction);

        protected override Menu CreateSubMenu() => new VignetteMenu(Direction.Vertical);

        private class DrawableVignetteMenuItem : DrawableMenuItem
        {
            public DrawableVignetteMenuItem(MenuItem item)
                : base(item)
            {
                BackgroundColour = Colour4.Transparent;
                BackgroundColourHover = Colour4.Transparent;
            }

            protected override Drawable CreateContent() => new VignetteSpriteText
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Padding = new MarginPadding(2),
            };
        }
    }
}