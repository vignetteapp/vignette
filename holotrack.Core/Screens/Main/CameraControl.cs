using System.Linq;
using holotrack.Core.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Camera;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace holotrack.Core.Screens.Main
{
    public class CameraControl : Container
    {
        private Container hover;
        private CameraSprite display;
        private HoloTrackDropdown<string> deviceList;

        [Resolved]
        private CameraManager cameraManager { get; set; }

        public CameraControl()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Children = new Drawable[]
            {
                new HoloTrackPanel
                {
                    Height = 150,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        display = new CameraSprite
                        {
                            FillMode = FillMode.Fill,
                            FillAspectRatio = 1,
                            RelativeSizeAxes = Axes.Both,
                        },
                        hover = new Container
                        {
                            Alpha = 0,
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Colour = ColourInfo.GradientVertical(Color4.Black.Opacity(0.6f), Color4.Black.Opacity(0)),
                                    Height = 0.25f,
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    RelativeSizeAxes = Axes.Both,
                                },
                                new Box
                                {
                                    Colour = ColourInfo.GradientVertical(Color4.Black.Opacity(0), Color4.Black.Opacity(0.6f)),
                                    Height = 0.25f,
                                    Anchor = Anchor.BottomCentre,
                                    Origin = Anchor.BottomCentre,
                                    RelativeSizeAxes = Axes.Both,
                                },
                            },
                        }
                    }
                },
                deviceList = new CameraDeviceSelection(cameraManager)
                {
                    Width = 0.95f,
                    Alpha = 0,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Margin = new MarginPadding { Top = 5 },
                    RelativeSizeAxes = Axes.X,
                },
            };
            
            deviceList.Current.ValueChanged += (v) => display.CameraID = cameraManager.CameraDeviceNames.ToList().IndexOf(v.NewValue);
        }

        protected override bool OnHover(HoverEvent e)
        {
            hover.FadeInFromZero(100);
            deviceList.FadeInFromZero(100);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);
            hover.FadeOutFromOne(100);
            deviceList.FadeOutFromOne(100);
        }

        private class CameraDeviceSelection : HoloTrackDropdown<string>
        {
            private CameraManager cameraManager;

            protected override DropdownMenu CreateMenu() => new CameraDeviceMenu();

            public CameraDeviceSelection(CameraManager cameraManager)
            {
                this.cameraManager = cameraManager;

                Items = cameraManager.CameraDeviceNames;
                cameraManager.OnNewDevice += updateDevices;
                cameraManager.OnLostDevice += updateDevices;
            }

            private void updateDevices(string name) => Items = cameraManager.CameraDeviceNames;

            private class CameraDeviceMenu : HoloTrackDropdownMenu
            {
                protected override bool OnHover(HoverEvent e) => false;
            }
        }
    }
}