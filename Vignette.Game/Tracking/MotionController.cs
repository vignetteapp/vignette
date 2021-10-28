// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using Akihabara.Framework.Protobuf;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using Vignette.Live2D.Graphics.Controllers;

namespace Vignette.Game.Tracking
{
    public class MotionController : CubismController
    {
        [BackgroundDependencyLoader]
        private void load()
        {

        }

        public void ApplyLandmarks(List<NormalizedLandmarkList> landmarks)
        {

        }
    }
}
