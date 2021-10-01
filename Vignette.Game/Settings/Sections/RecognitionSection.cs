// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osuTK;
using Vignette.Camera;
using Vignette.Camera.Graphics;
using Vignette.Camera.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Sections
{
    public class RecognitionSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.EyeShow;

        public override LocalisableString Label => "Recognition";

        [Resolved]
        private CameraManager camera { get; set; }

        private readonly BindableList<string> devices = new BindableList<string>();

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Label = "Camera",
                    Children = new Drawable[]
                    {
                        new CameraPreview(),
                        new SettingsDropdown<string>
                        {
                            Icon = SegoeFluent.Camera,
                            Label = "Device",
                            ItemSource = devices,
                            Current = config.GetBindable<string>(VignetteSetting.CameraDevice),
                        },
                    }
                },
            };

            System.Console.WriteLine("\n\x1b[34m==== Camera device names ====");
            foreach (var cdn in camera.CameraDeviceNames)
                System.Console.WriteLine(cdn);
            System.Console.WriteLine("==== Camera device names ====\x1b[0m");
            devices.AddRange(camera.CameraDeviceNames);
            camera.OnNewDevice += onNewCameraDevice;
            camera.OnLostDevice += onLostCameraDevice;
        }

        private void onNewCameraDevice(string name)
        {
            if (!devices.Contains(name))
                devices.Add(name);
        }

        private void onLostCameraDevice(string name)
        {
            if (devices.Contains(name))
                devices.Remove(name);
        }

        private class CameraPreview : Container
        {
            [Resolved]
            private IBindable<CameraDevice> device { get; set; }

            private DrawableCameraDevice preview;

            [BackgroundDependencyLoader]
            private void load()
            {
                Size = new Vector2(250, 140);
                Masking = true;
                CornerRadius = 5;
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Black,
                    }
                };

                device.BindValueChanged(_ => onNewCameraDevice(), true);
            }

            private void onNewCameraDevice()
            {
                preview?.Expire();

                if (device.Value == null)
                    return;

                Add(preview = new DrawableCameraDevice(device.Value, false)
                {
                    RelativeSizeAxes = Axes.Both,
                });
            }
        }

        private class AdvancedCameraSettingsPanel : SettingsSubPanel
        {
        }
    }
}
