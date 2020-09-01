using holotrack.Graphics;
using holotrack.Graphics.UserInterface;
using holotrack.Screens.Main.Menus;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;

namespace holotrack.Screens.Main
{
    public class SideMenuHeader : Container
    {
        private readonly SideMenuTabControl control;
        public Bindable<SideMenuTab> Current => control?.Current;

        public SideMenuHeader()
        {
            Height = 30;
            RelativeSizeAxes = Axes.X;

            Child = new TooltipContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new HoloTrackPanel
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = HoloTrackColor.ControlBackground,
                            RelativeSizeAxes = Axes.Both,
                        },
                        control = new SideMenuTabControl
                        {
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                }
            };
        }

        public class SideMenuTabControl : TabControl<SideMenuTab>
        {
            protected override Dropdown<SideMenuTab> CreateDropdown() => null;
            protected override TabItem<SideMenuTab> CreateTabItem(SideMenuTab value) => new SideMenuTabItem(value);

            private class SideMenuTabItem : TabItem<SideMenuTab>, IHasTooltip
            {
                private readonly SideMenuTab menuTab;
                private readonly Container background;
                private readonly Box backgroundBox;
                private readonly SpriteIcon icon;

                public string TooltipText => menuTab?.Name;

                public SideMenuTabItem(SideMenuTab value)
                    : base(value)
                {
                    menuTab = value;

                    RelativeSizeAxes = Axes.Y;
                    AutoSizeAxes = Axes.X;
                    Margin = new MarginPadding { Horizontal = 5 };
                    Anchor = Anchor.Centre;
                    Origin = Anchor.Centre;

                    AddRange(new Drawable[]
                    {
                        background = new Container
                        {
                            Masking = true,
                            CornerRadius = 3,
                            BorderColour = HoloTrackColor.ControlBorder,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(25),
                            Child = backgroundBox = new Box
                            {
                                Colour = Colour4.Transparent,
                                RelativeSizeAxes = Axes.Both,
                            }
                        },
                        icon = new SpriteIcon
                        {
                            Icon = menuTab.Icon,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(15),
                        }
                    });
                }

                protected override void OnActivated()
                {
                    icon.Colour = Colour4.Black;
                    background.BorderThickness = 0;
                    backgroundBox.Colour = Colour4.White;
                }

                protected override void OnDeactivated()
                {
                    icon.Colour = Colour4.White;
                    backgroundBox.Colour = Colour4.Transparent;
                }

                protected override bool OnHover(HoverEvent e)
                {
                    if (!Active.Value)
                        background.BorderThickness = 1;

                    return base.OnHover(e);
                }

                protected override void OnHoverLost(HoverLostEvent e)
                {
                    base.OnHoverLost(e);

                    if (!Active.Value)
                        background.BorderThickness = 0;
                }
            }
        }
    }
}