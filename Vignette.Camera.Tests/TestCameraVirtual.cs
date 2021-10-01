// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System.Collections.Generic;
using Emgu.CV.CvEnum;
using Vignette.Camera.Tests.Resources;

namespace Vignette.Camera.Tests
{
    internal class TestCameraVirtual : CameraVirtual
    {
        public new string FilePath => base.FilePath;

        public new string State => base.State.ToString();

        public TestCameraVirtual()
            : base(TestResources.GetStream(@"earth.mp4"), EncodingFormat.JPEG, new Dictionary<ImwriteFlags, int>
            {
                { ImwriteFlags.JpegQuality, 20 },
                { ImwriteFlags.JpegOptimize, 1 },
            })
        {
        }
    }
}
