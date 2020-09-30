using System.Linq;
using vignette.Configuration;
using vignette.Graphics;
using vignette.Graphics.Interface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Framework.Input.Events;
using osuTK;

namespace vignette.Overlays.SidePanel
{
    public class CameraDisplay : Container
    {
        private readonly Container hover;
        private readonly CameraSprite sprite;
        private readonly CameraDevicesDropdown dropdown;

        [Resolved]
        private CameraManager camera { get; set; }

        [Resolved]
        private VignetteConfigManager config { get; set; }

        public byte[] TextureData => sprite.CaptureData;

        public CameraDisplay()
        {
            Size = new Vector2(260, 195);

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = VignetteColor.Darker,
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

            var device = config.GetBindable<string>(VignetteSetting.CameraDevice);
            dropdown.Current.ValueChanged += v => sprite.CameraID = camera.CameraDeviceNames.ToList().IndexOf(device.Value = v.NewValue);

            camera.OnLostDevice += _ => updateItems();
            camera.OnNewDevice += _ => updateItems();
        }

        private void updateItems()
        {
            dropdown.Items = camera.CameraDeviceNames;
        }


        private class CameraDevicesDropdown : VignetteDropdown<string>
        {
            protected override DropdownHeader CreateHeader() => new CameraDevicesDropdownHeader();

            private class CameraDevicesDropdownHeader : VignetteDropdownHeader
            {
                public CameraDevicesDropdownHeader()
                {
                    BackgroundColour = Colour4.Transparent;
                    BackgroundColourHover = Colour4.Transparent;

                    AddRange(new Drawable[]
                    {
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
                    });
                }
            }
        }
    }
}