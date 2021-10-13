// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Video;

namespace Vignette.Game.Screens.Stage
{
    public class VideoBackground : FileAssociatedBackground
    {
        public override IEnumerable<string> Extensions => new[] { ".mp4" };

        protected override Drawable CreateBackground(Stream stream) => new Video(stream)
        {
            Loop = true,
            FillMode = FillMode.Fit,
            RelativeSizeAxes = Axes.Both,
        };
    }
}
