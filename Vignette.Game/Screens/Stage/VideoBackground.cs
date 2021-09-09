// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

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
