using System.Linq;
using holotrack.Configuration;
using holotrack.Graphics;
using holotrack.Graphics.Interface;
using holotrack.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Framework.Input.Events;
using osuTK;

namespace holotrack.Overlays.SidePanel
{
    public class CameraDisplay : Container
    {
        private readonly Container hover;
        private readonly CameraSprite sprite;
        private readonly CameraDevicesDropdown dropdown;

        [Resolved]
        private CameraManager camera { get; set; }

        [Resolved]
        private HoloTrackConfigManager config { get; set; }

        public CameraDisplay()
        {
            Size = new Vector2(260, 195);

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = HoloTrackColor.Darker,
                    RelativeSizeAxes = Axes.Both,
                },
                sprite = new CameraSprite
                {
                    FillMode = FillMode.Fit,
                    RelativeSizeAxes = Axes.Both,
                },
                hover = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Black.Opacity(0.75f),
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding(10),
                            Child = dropdown = new CameraDevicesDropdown { RelativeSizeAxes = Axes.X },
                        }
                    }
                },
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            hover.FadeIn(200, Easing.OutQuint);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);
            hover.FadeOut(200, Easing.OutQuint);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updateItems();

            var device = config.GetBindable<string>(HoloTrackSetting.CameraDevice);
            dropdown.Current.ValueChanged += v => sprite.CameraID = camera.CameraDeviceNames.ToList().IndexOf(device.Value = v.NewValue);

            camera.OnLostDevice += _ => updateItems();
            camera.OnNewDevice += _ => updateItems();
        }

        private void updateItems()
        {
            dropdown.Items = camera.CameraDeviceNames;
        }


        private class CameraDevicesDropdown : Dropdown<string>
        {
            protected override DropdownHeader CreateHeader() => new CameraDevicesDropdownHeader();

            protected override DropdownMenu CreateMenu() => new CameraDevicesDropdownMenu();

            private class CameraDevicesDropdownHeader : DropdownHeader
            {
                private readonly Sprite chevron;
                private readonly HoloTrackSpriteText label;

                protected override string Label 
                {
                    get => label.Text;
                    set => label.Text = value;
                }

                public CameraDevicesDropdownHeader()
                {
                    BackgroundColour = Colour4.Transparent;
                    BackgroundColourHover = Colour4.Transparent;

                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 25,
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
                        },
                        new Box
                        {
                            Width = 0.5f,
                            Height = 1,
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Colour = ColourInfo.GradientHorizontal(Colour4.Transparent, Colour4.White),
                            RelativeSizeAxes = Axes.X,
                        },
                        new Box
                        {
                            Width = 0.5f,
                            Height = 1,
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            Colour = ColourInfo.GradientHorizontal(Colour4.White, Colour4.Transparent),
                            RelativeSizeAxes = Axes.X,
                        },
                    };
                }

                [BackgroundDependencyLoader]
                private void load(TextureStore store)
                {
                    chevron.Texture = store.Get("chevron-down");
                }
            }

            private class CameraDevicesDropdownMenu : DropdownMenu
            {
                public CameraDevicesDropdownMenu()
                {
                    Margin = new MarginPadding { Top = 5 };
                    BackgroundColour = HoloTrackColor.Lighter;
                }

                public override bool HandleNonPositionalInput => State == MenuState.Open;

                protected override bool OnHover(HoverEvent e) => false;

                protected override DrawableDropdownMenuItem CreateDrawableDropdownMenuItem(MenuItem item) => new CameraDevicesDrawableDropdownMenuItem(item);

                protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new HoloTrackScrollContainer(direction);

                protected override Menu CreateSubMenu() => new CameraDevicesMenu(Direction.Vertical);

                private class CameraDevicesDrawableDropdownMenuItem : DrawableDropdownMenuItem
                {
                    public override bool HandlePositionalInput => true;

                    public CameraDevicesDrawableDropdownMenuItem(MenuItem item)
                        : base(item)
                    {
                        Margin = new MarginPadding(5);
                        BackgroundColour = Colour4.Transparent;
                        BackgroundColourHover = Colour4.Transparent;
                        BackgroundColourSelected = Colour4.Transparent;
                    }

                    protected override Drawable CreateContent() => new HoloTrackSpriteText();
                }

                private class CameraDevicesMenu : Menu
                {
                    public CameraDevicesMenu(Direction direction, bool topLevelMenu = false)
                        : base(direction, topLevelMenu)
                    {
                        BackgroundColour = Colour4.Transparent;
                    }

                    protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item) => new DrawableCameraDeviceMenuItem(item);

                    protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new HoloTrackScrollContainer(direction);

                    protected override Menu CreateSubMenu() => new CameraDevicesMenu(Direction.Vertical);

                    private class DrawableCameraDeviceMenuItem : DrawableMenuItem
                    {
                        public DrawableCameraDeviceMenuItem(MenuItem item)
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
        }
    }
}