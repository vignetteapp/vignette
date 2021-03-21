// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using Vignette.Application.Camera;
using Vignette.Application.Camera.Graphics;
using Vignette.Application.Camera.Platform;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.Screens.Main.Controls;

namespace Vignette.Application.Screens.Main.Sections
{
    public class CameraSettingSection : ToolbarSection
    {
        public override string Title => "Camera";

        public override IconUsage Icon => FluentSystemIcons.Filled.Camera24;

        private readonly BindableList<string> deviceList = new BindableList<string>();

        private LabelledDropdown<string> cameraDropdown;

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager config, CameraManager manager)
        {
            Children = new Drawable[]
            {
                new Container
                {
                    Height = 200,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Colour4.Black,
                        },
                        new CameraViewContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                    },
                },
                cameraDropdown = new LabelledDropdown<string>
                {
                    Label = "Camera",
                    Current = config.GetBindable<string>(ApplicationSetting.CameraDevice),
                    ItemSource = deviceList,
                },
            };

            manager.OnLostDevice += d => deviceList.Remove(d);
            manager.OnNewDevice += d => deviceList.Add(d);

            deviceList.AddRange(manager.CameraDeviceNames);
        }

        private class CameraViewContainer : Presentation<CameraSlide>
        {
            private Bindable<string> deviceName;

            [Resolved]
            private CameraManager manager { get; set; }

            [BackgroundDependencyLoader]
            private void load(ApplicationConfigManager config)
            {
                deviceName = config.GetBindable<string>(ApplicationSetting.CameraDevice);
                deviceName.BindValueChanged(handleCameraChange, true);
            }

            private void handleCameraChange(ValueChangedEvent<string> name)
            {
                if (Current.Value != null)
                    Remove(Current.Value);

                // TODO: Pass CameraDevice via Dependency Injection.
                var device = new CameraDevice(manager.CameraDeviceNames.TakeWhile(n => n != name.NewValue).Count());
                Add(new CameraSlide(device));
            }
        }

        private class CameraSlide : PresentationSlide
        {
            private DrawableCameraDevice camera;

            public CameraSlide(CameraDevice device)
            {
                Child = camera = new DrawableCameraDevice(device)
                {
                    RelativeSizeAxes = Axes.Both,
                };
            }

            public override void OnEntering()
            {
                this.FadeIn(300, Easing.OutQuint);
                camera.Start();
            }

            public override void OnExiting()
            {
                this.FadeOut(300, Easing.OutQuint);
                camera.Stop(false);
            }
        }
    }
}
