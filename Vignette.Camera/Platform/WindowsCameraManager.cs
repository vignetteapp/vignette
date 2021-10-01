// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System.Collections.Generic;
using DirectShowLib;
using osu.Framework.Threading;

namespace Vignette.Camera.Platform
{
    public class WindowsCameraManager : CameraManager
    {
        public WindowsCameraManager(Scheduler scheduler)
            : base(scheduler)
        {
        }

        protected override IEnumerable<CameraInfo> EnumerateAllDevices()
        {
            foreach (var device in DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice))
                yield return new CameraInfo(device.Name, device.DevicePath);
        }
    }
}
