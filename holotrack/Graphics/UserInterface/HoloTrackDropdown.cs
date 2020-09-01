using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace holotrack.Graphics.UserInterface
{
    public class HoloTrackDropdown<T> : Dropdown<T>
    {
        protected override DropdownHeader CreateHeader() => new HoloTrackDropdownHeader();

        protected override DropdownMenu CreateMenu() => new HoloTrackDropdownMenu();

        protected class HoloTrackDropdownHeader : DropdownHeader
        {
            private readonly SpriteText label;
            protected override string Label
            {
                get => label?.Text;
                set
                {
                    if (label != null)
                        label.Text = value;
                }
            }

            public HoloTrackDropdownHeader()
            {
                AutoSizeAxes = Axes.None;
                Height = 30;
                Masking = true;
                CornerRadius = 3;
                BorderColour = HoloTrackColor.ControlBorder;
                BorderThickness = 3;

                BackgroundColour = Colour4.Transparent;
                BackgroundColourHover = Colour4.Transparent;

                Children = new Drawable[]
                {
                    new SpriteIcon
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Left = 15 },
                        Icon = FontAwesome.Solid.ChevronDown,
                        Size = new Vector2(8),
                    },
                    label = new SpriteText
                    {
                        Font = HoloTrackFont.Control,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Margin = new MarginPadding { Left = 35 },
                        AlwaysPresent = true,
                    }
                };
            }
        }

        protected class HoloTrackDropdownMenu : DropdownMenu
        {
            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new HoloTrackDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new HoloTrackScrollContainer(direction);

            protected override Menu CreateSubMenu() => new BasicMenu(Direction.Vertical);

            public HoloTrackDropdownMenu()
            {
                Margin = new MarginPadding { Vertical = 5 };
                BackgroundColour = HoloTrackColor.ControlBorder;
                MaskingContainer.CornerRadius = 3;
            }

            protected class HoloTrackDropdownMenuItem : DrawableDropdownMenuItem
            {
                public HoloTrackDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    BackgroundColour = Colour4.Transparent;
                    BackgroundColourHover = Colour4.Black.Opacity(0.4f);
                    BackgroundColourSelected = Colour4.Black.Opacity(0.4f);
                }

                protected override Drawable CreateContent() => new SpriteText
                {
                    Font = HoloTrackFont.Control,
                    Margin = new MarginPadding(5),
                };
            }
        }
    }
}