// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Video;
using Vignette.Game.IO;

namespace Vignette.Game.Graphics.Backgrounds
{
    public class UserBackgroundVideo : UserBackground
    {
        private readonly string filename;

        public UserBackgroundVideo(string filename)
        {
            this.filename = filename;
        }

        [BackgroundDependencyLoader]
        private void load(UserResources resources)
        {
            var stream = resources.Videos.GetStream(filename);

            if (stream == null)
                return;

            InternalChild = new Video(stream)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
            };
        }
    }
}
