// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

namespace Vignette.Camera
{
    /// <summary>
    /// A physical camera device
    /// </summary>
    public interface ICameraDevice
    {
        double Saturation { get; set; }

        double Contrast { get; set; }

        double Exposure { get; set; }

        double Gain { get; set; }

        double Hue { get; set; }

        double Focus { get; set; }

        double AutoExposure { get; set; }

        double AutoFocus { get; set; }

        bool Record(string path);

        bool Save();
    }
}
