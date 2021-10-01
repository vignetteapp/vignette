// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace Vignette.Camera
{
    /// <inheritdoc cref="ICameraDevice"/>
    public class CameraDevice : Camera, ICameraDevice
    {
        /// <summary>
        /// Gets or sets the device's saturation setting. Changes are only visible if your device supports it.
        /// </summary>
        public double Saturation
        {
            get => Capture.Get(CapProp.Saturation);
            set => Capture.Set(CapProp.Saturation, value);
        }

        /// <summary>
        /// Gets or sets the device's contrast setting. Changes are only visible if your device supports it.
        /// </summary>
        public double Contrast
        {
            get => Capture.Get(CapProp.Contrast);
            set => Capture.Set(CapProp.Contrast, value);
        }

        /// <summary>
        /// Gets or sets the device's exposure setting. Changes are only visible if your device supports it.
        /// </summary>
        public double Exposure
        {
            get => Capture.Get(CapProp.Exposure);
            set => Capture.Set(CapProp.Exposure, value);
        }

        /// <summary>
        /// Gets or sets the device's gain setting. Changes are only visible if your device supports it.
        /// </summary>
        public double Gain
        {
            get => Capture.Get(CapProp.Gain);
            set => Capture.Set(CapProp.Gain, value);
        }

        /// <summary>
        /// Gets or sets the device's hue setting. Changes are only visible if your device supports it.
        /// </summary>
        public double Hue
        {
            get => Capture.Get(CapProp.Hue);
            set => Capture.Set(CapProp.Hue, value);
        }

        /// <summary>
        /// Gets or sets the device's focus setting. Changes are only visible if your device supports it.
        /// </summary>
        public double Focus
        {
            get => Capture.Get(CapProp.Focus);
            set => Capture.Set(CapProp.Focus, value);
        }

        /// <summary>
        /// Gets or sets the device's auto exposure setting. Changes are only visible if your device supports it.
        /// </summary>
        public double AutoExposure
        {
            get => Capture.Get(CapProp.AutoExposure);
            set => Capture.Set(CapProp.AutoExposure, value);
        }

        /// <summary>
        /// Gets or sets the device's auto focus setting. Changes are only visible if your device supports it.
        /// </summary>
        public double AutoFocus
        {
            get => Capture.Get(CapProp.Autofocus);
            set => Capture.Set(CapProp.Autofocus, value);
        }

        private VideoWriter writer;

        /// <summary>
        /// Create a new camera from a physical device.
        /// </summary>
        /// <param name="cameraId">The camera's numeric identifier.</param>
        /// <param name="format">Image format used for encoding.</param>
        /// <param name="encodingParams">An array of parameters used for encoding.</param>
        public CameraDevice(int cameraId, EncodingFormat format = EncodingFormat.PNG, Dictionary<ImwriteFlags, int> encodingParams = null)
            : base(format, encodingParams)
        {
            Capture = new VideoCapture(cameraId);
        }

        /// <summary>
        /// Initializes a recording session.
        /// </summary>
        /// <param name="path">The destination path of the recording.</param>
        /// <returns>Whether it has successfully started a recording.</returns>
        public bool Record(string path)
        {
            if (writer != null)
                return false;

            writer = new VideoWriter(path, (int)FramesPerSecond, new Size(Size.X, Size.Y), true);

            return true;
        }

        /// <summary>
        /// Releases and disposes resources and ends the current recording.
        /// </summary>
        /// <returns>Whether it has successfully saved the recording.</returns>
        public bool Save()
        {
            if (writer == null)
                return false;

            writer.Dispose();
            writer = null;
            return true;
        }

        protected override void PreTick()
        {
            if (writer != null && Mat != null)
                writer.Write(Mat);
        }
    }
}
