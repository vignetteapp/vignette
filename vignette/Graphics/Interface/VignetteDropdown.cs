using vignette.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;

namespace vignette.Graphics.Interface
{
    public class VignetteDropdown<T> : Dropdown<T>
    {
        protected override DropdownHeader CreateHeader() => new VignetteDropdownHeader();

        protected override DropdownMenu CreateMenu() => new VignetteDropdownMenu();

        public class VignetteDropdownHeader : DropdownHeader
        {
            private readonly Sprite chevron;
            private readonly VignetteSpriteText label;

            protected override string Label 
            {
                get => label.Text;
                set => label.Text = value;
            }

            public VignetteDropdownHeader()
            {
                Masking = true;
                CornerRadius = 5;
                BackgroundColour = VignetteColor.Dark;
                BackgroundColourHover = VignetteColor.Dark;

                Children = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 25,
                        Margin = new MarginPadding { Left = 10 },
                        Child = label = new VignetteSpriteText
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

        public class VignetteDropdownMenu : DropdownMenu
        {
            public VignetteDropdownMenu()
            {
                Margin = new MarginPadding { Top = 5 };
                BackgroundColour = VignetteColor.Lighter;
            }

            public override bool HandleNonPositionalInput => State == MenuState.Open;

            protected override bool OnHover(HoverEvent e) => false;

            protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new DrawableVignetteDropdownMenuItem(item);

            protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new VignetteScrollContainer(direction);

            protected override Menu CreateSubMenu() => new VignetteMenu(Direction.Vertical);

            private class DrawableVignetteDropdownMenuItem : DrawableDropdownMenuItem
            {
                public override bool HandlePositionalInput => true;

                public DrawableVignetteDropdownMenuItem(MenuItem item)
                    : base(item)
                {
                    BackgroundColour = Colour4.Transparent;
                    BackgroundColourHover = Colour4.Black.Opacity(0.4f);
                    BackgroundColourSelected = Colour4.Black.Opacity(0.4f);
                }

                protected override Drawable CreateContent() => new VignetteSpriteText { Margin = new MarginPadding(5) };
            }
        }
    }
}