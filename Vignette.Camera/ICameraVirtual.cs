// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

namespace Vignette.Camera
{
    /// <summary>
    /// A virtual camera device instantiated from a file.
    /// </summary>
    public interface ICameraVirtual
    {
        bool Loop { get; set; }

        int FrameCount { get; }

        int Position { get; }

        void Seek(int frame);
    }
}
