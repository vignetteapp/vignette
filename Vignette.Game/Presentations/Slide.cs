// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Vignette.Game.Presentations
{
    public abstract class Slide : CompositeDrawable, ISlide
    {
        public Slide()
        {
            RelativeSizeAxes = Axes.Both;
        }

        public abstract void OnEntering(ISlide last);

        public abstract void OnExiting(ISlide next);
    }
}
