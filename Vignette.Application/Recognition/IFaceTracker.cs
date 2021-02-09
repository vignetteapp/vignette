// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using osuTK;
using Vignette.Application.Camera;

namespace Vignette.Application.Recognition
{
    public interface IFaceTracker
    {
        public IReadOnlyList<Face> Faces { get; }

        public int Tracked { get; }

        public double Delta { get; }

        public void StartTracking(ICamera camera);

        public void StopTracking();

        public Vector3? GetHeadAngles(int index = 0);

        public Vector2? GetHeadPosition(int index = 0);

        public Vector2? GetEyePupilPosition(FaceRegion eye, int headIndex = 0);

        public float? GetEyeLidOpen(FaceRegion eye, int headIndex = 0);

        public float? GetMouthOpen(int index = 0);
    }
}
