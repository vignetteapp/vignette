// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

namespace Vignette.Camera.Graphics
{
    /// <summary>
    /// A <see cref="CameraDevice"/> that can be added to the scene hierarchy.
    /// </summary>
    public class DrawableCameraDevice : DrawableCameraWrapper<CameraDevice>, ICameraDevice
    {
        /// <inheritdoc cref="CameraDevice.Saturation"/>
        public double Saturation
        {
            get => Camera.Saturation;
            set => Camera.Saturation = value;
        }

        /// <inheritdoc cref="CameraDevice.Contrast"/>
        public double Contrast
        {
            get => Camera.Contrast;
            set => Camera.Contrast = value;
        }

        /// <inheritdoc cref="CameraDevice.Exposure"/>
        public double Exposure
        {
            get => Camera.Exposure;
            set => Camera.Exposure = value;
        }

        /// <inheritdoc cref="CameraDevice.Gain"/>
        public double Gain
        {
            get => Camera.Gain;
            set => Camera.Gain = value;
        }

        /// <inheritdoc cref="CameraDevice.Hue"/>
        public double Hue
        {
            get => Camera.Hue;
            set => Camera.Hue = value;
        }

        /// <inheritdoc cref="CameraDevice.Focus"/>
        public double Focus
        {
            get => Camera.Focus;
            set => Camera.Focus = value;
        }

        /// <inheritdoc cref="CameraDevice.AutoExposure"/>t
        public double AutoExposure
        {
            get => Camera.AutoExposure;
            set => Camera.AutoExposure = value;
        }

        /// <inheritdoc cref="CameraDevice.AutoFocus"/>
        public double AutoFocus
        {
            get => Camera.AutoFocus;
            set => Camera.AutoFocus = value;
        }


        public DrawableCameraDevice(CameraDevice camera, bool disposeUnderlyingCameraOnDispose = true)
            : base(camera, disposeUnderlyingCameraOnDispose)
        {
        }

        /// <inheritdoc cref="CameraDevice.Record"/>
        public bool Record(string path) => Camera.Record(path);

        /// <inheritdoc cref="CameraDevice.Save"/>
        public bool Save() => Camera.Save();
    }
}
