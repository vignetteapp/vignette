// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Localisation;
using osuTK;
using SixLabors.ImageSharp.PixelFormats;
using System;
using Vignette.Camera.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Containers;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Settings.Components;
using Vignette.Game.Tracking;

namespace Vignette.Game.Settings.Sections
{
    public class RecognitionSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.EyeShow;

        public override LocalisableString Label => "Recognition";

        public event Action CalibrateAction;

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
                        new SettingsDropdown<string>
                        {
                            Icon = SegoeFluent.Camera,
                            Label = "Device",
                            ItemSource = devices,
                            Current = config.GetBindable<string>(VignetteSetting.CameraDevice),
                        },
                    }
                },
                new ThemableTextFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.TopLeft,
                    Text = "To properlay calibrate, please open your mouth and eyes wide first!",
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(5, 0),
                    Child = new FluentButton
                    {
                        Text = "Calibrate",
                        Width = 90,
                        Style = ButtonStyle.Secondary,
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.TopRight,
                        Action = () => CalibrateAction?.Invoke(),
                    },
                },
            };

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

        private class CameraTrackingPreview : Container
        {
            [Resolved(canBeNull: true)]
            private TrackingComponent tracker { get; set; }

            private Sprite preview;

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
                    },
                    preview = new Sprite
                    {
                        RelativeSizeAxes = Axes.Both,
                        FillMode = FillMode.Fit,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                };
            }

            protected override void Update()
            {
                base.Update();

                if (tracker?.OutputFrame == null)
                    return;

                lock (tracker.OutputFrame)
                {
                    var pixelData = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(tracker.OutputFrame, tracker.OutputFrameWidth, tracker.OutputFrameHeight);

                    var texture = new Texture(tracker.OutputFrameWidth, tracker.OutputFrameHeight);
                    var upload = new TextureUpload(pixelData);
                    texture.SetData(upload);

                    preview.Texture = texture;
                };
            }
        }
    }
}
