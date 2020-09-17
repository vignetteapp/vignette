using holotrack.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;

namespace holotrack.Graphics.Interface
{
    public class HoloTrackDropdown<T> : Dropdown<T>
    {
        protected override DropdownHeader CreateHeader() => new HoloTrackDropdownHeader();

        protected override DropdownMenu CreateMenu() => new HoloTrackDropdownMenu();

        public class HoloTrackDropdownHeader : DropdownHeader
        {
            private readonly Sprite chevron;
            private readonly HoloTrackSpriteText label;

            protected override string Label 
            {
                get => label.Text;
                set => label.Text = value;
            }

            public HoloTrackDropdownHeader()
            {
                Masking = true;
                CornerRadius = 5;
                BackgroundColour = HoloTrackColor.Dark;
                BackgroundColourHover = HoloTrackColor.Dark;

                Children = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 25,
                        Margin = new MarginPadding { Left = 10 },
                        Child = label = new HoloTrackSpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                        },
                    },
                    chevron = new Sprite
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Margin = new MarginPadding { Right = 10 },
                    },
                };
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore store)
            {
                chevron.Texture = store.Get("chevron-down");
            }
        }

        public class HoloTrackDropdownMenu : DropdownMenu
        {
            public HoloTrackDropdownMenu()
            {
                Margin = new MarginPadding { Top = 5 };
                BackgroundColour = HoloTrackColor.Lighter;
            }

            public override bool HandleNonPositionalInput => State == MenuState.Open;

            protected override bool OnHover(HoverEvent e) => false;

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawableHoloTrackDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new HoloTrackScrollContainer(direction);

            protected override Menu CreateSubMenu() => new HoloTrackMenu(Direction.Vertical);

            private class DrawableHoloTrackDropdownMenuItem : DrawableDropdownMenuItem
            {
                public override bool HandlePositionalInput => true;

                public DrawableHoloTrackDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    BackgroundColour = Colour4.Transparent;
                    BackgroundColourHover = Colour4.Black.Opacity(0.4f);
                    BackgroundColourSelected = Colour4.Black.Opacity(0.4f);
                }

                protected override Drawable CreateContent() => new HoloTrackSpriteText { Margin = new MarginPadding(5) };
            }
        }
    }
}