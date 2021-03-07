// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Linq;
using OpenCvSharp;
using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Testing;
using osu.Framework.Threading;
using osuTK;
using Vignette.Application.Camera;
using Vignette.Application.Camera.Graphics;
using Vignette.Application.Camera.Platform;
using Vignette.Application.Recognition;

namespace Vignette.Application.Tests.Visual.Recognition
{
    public abstract class TestSceneRecognition : TestScene
    {
        protected ICamera Camera;

        private DrawableCameraWrapper cameraSprite;

        private readonly FaceTracker tracker = null;

        private readonly Container cameraSpriteContainer;

        private readonly CameraManager cameraManager;

        private readonly FillFlowContainer virtualCameraControls;

        private readonly FillFlowContainer physicalCameraControls;

        private readonly BasicButton pauseButton;

        private readonly BasicCheckbox virtualCameraLooping;

        private readonly BasicDropdown<string> currentPhysicalCamera;

        private readonly BasicDropdown<CameraType> cameraTypeSelector;

        private static readonly ImageEncodingParam[] encodingparams = new[]
        {
            new ImageEncodingParam(ImwriteFlags.JpegQuality, 100),
            new ImageEncodingParam(ImwriteFlags.JpegOptimize, 50),
        };

        public TestSceneRecognition()
        {
            cameraManager = createSuitableCameraManager(Scheduler);

            Add(cameraSpriteContainer = new Container { RelativeSizeAxes = Axes.Both });

            Add(new FillFlowContainer
            {
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Width = 200.0f,
                Margin = new MarginPadding(5),
                Spacing = new Vector2(0, 5),
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                Children = new Drawable[]
                {
                    cameraTypeSelector = new BasicDropdown<CameraType>
                    {
                        RelativeSizeAxes = Axes.X,
                        Items = Enum.GetValues<CameraType>(),
                    },
                    virtualCameraControls = new FillFlowContainer
                    {
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        RelativeSizeAxes = Axes.X,
                        Spacing = new Vector2(0, 5),
                        Children = new Drawable[]
                        {
                            virtualCameraLooping = new BasicCheckbox { LabelText = @"Toggle Looping" },
                            pauseButton = new BasicButton
                            {
                                Text = @"Pause",
                                Height = 40,
                                RelativeSizeAxes = Axes.X,
                                Action = () =>
                                {
                                    if (Camera is CameraVirtual virtualCamera)
                                    {
                                        if (virtualCamera.Paused)
                                        {
                                            pauseButton.Text = @"Pause";
                                            virtualCamera.Resume();
                                        }
                                        else
                                        {
                                            pauseButton.Text = @"Unpause";
                                            virtualCamera.Pause();
                                        }
                                    }
                                }
                            }
                        }
                    },
                    physicalCameraControls = new FillFlowContainer
                    {
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        RelativeSizeAxes = Axes.X,
                        Spacing = new Vector2(0, 5),
                        Children = new Drawable[]
                        {
                            currentPhysicalCamera = new BasicDropdown<string> { RelativeSizeAxes = Axes.X }
                        }
                    }
                }
            });

            cameraTypeSelector.Current.BindValueChanged((mode) =>
            {
                cameraSprite?.Expire();
                tracker?.StopTracking();

                switch (mode.NewValue)
                {
                    case CameraType.Physical:
                        if (!(cameraManager?.CameraDeviceNames.Any() ?? false))
                        {
                            cameraTypeSelector.Current.Value = CameraType.Virtual;
                            return;
                        }

                        currentPhysicalCamera.Items = cameraManager.CameraDeviceNames;
                        cameraManager.Current.Value = cameraManager.CameraDeviceNames.First();
                        cameraManager.Current.TriggerChange();
                        virtualCameraControls.Alpha = 0.0f;
                        physicalCameraControls.Alpha = 1.0f;
                        break;

                    case CameraType.Virtual:
                        Camera = new CameraVirtual(VignetteTestResources.GetResource(@"bakamitai_deepfake_template.mp4"), EncodingFormat.JPEG, encodingparams);
                        cameraSpriteContainer.Add(cameraSprite = new DrawableCameraVirtual((CameraVirtual)Camera));
                        virtualCameraControls.Alpha = 1.0f;
                        physicalCameraControls.Alpha = 0.0f;
                        break;
                }

                cameraSprite.Anchor = Anchor.Centre;
                cameraSprite.Origin = Anchor.Centre;
                cameraSprite.Scale = new Vector2(2.0f);
                cameraSprite.Width = Camera.Width;
                cameraSprite.Height = Camera.Height;

                Camera.Start();
                tracker?.StartTracking(Camera);
                TrackerChanged(tracker);
            }, true);

            virtualCameraLooping.Current.ValueChanged += (state) =>
            {
                if (Camera is CameraVirtual virtualCamera)
                    virtualCamera.Loop = state.NewValue;
            };

            cameraManager.Current.BindTo(currentPhysicalCamera.Current);
            cameraManager.Current.ValueChanged += (cam) =>
            {
                if (cameraSprite is DrawableCameraDevice drawableCameraDevice)
                    drawableCameraDevice.Expire();

                Camera = new CameraDevice(Array.IndexOf(cameraManager.CameraDeviceNames.ToArray(), cam.NewValue), EncodingFormat.JPEG, encodingparams);
                cameraSpriteContainer.Add(cameraSprite = new DrawableCameraDevice((CameraDevice)Camera));
            };
        }

        protected virtual void TrackerChanged(FaceTracker tracker)
        {
        }

        private static CameraManager createSuitableCameraManager(Scheduler scheduler)
        {
            switch (RuntimeInfo.OS)
            {
                case RuntimeInfo.Platform.Windows:
                    return new LegacyWindowsCameraManager(scheduler);

                case RuntimeInfo.Platform.Linux:
                    return new LinuxCameraManager(scheduler);

                default:
                    return null;
            }
        }

        private enum CameraType
        {
            Virtual,

            Physical,
        }
    }
}
