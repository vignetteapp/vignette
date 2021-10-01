// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

namespace Vignette.Camera
{
    public struct CameraInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public CameraInfo(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
