// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Threading;

namespace Vignette.Camera.Platform
{
    public class LinuxCameraManager : CameraManager
    {
        public LinuxCameraManager(Scheduler scheduler)
            : base(scheduler)
        {
        }

        protected override IEnumerable<CameraInfo> EnumerateAllDevices()
        {
            for (int i = 0; i < Directory.EnumerateDirectories(@"/dev/").Count(); i++)
            {
                string path = $"/dev/video{i}";

                string friendlyName;
                string friendlyNamePath = $"/sys/class/video4linux/video{i}/name";

                if (File.Exists(friendlyNamePath))
                {
                    friendlyName = $"({i}) {File.ReadAllText(friendlyNamePath).TrimEnd('\n')}";
                }
                else
                {
                    friendlyName = path;
                }

                yield return new CameraInfo(friendlyName, path);
            }
        }
    }
}
