// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Vignette.Application.Graphics.Containers
{
    /// <summary>
    /// A slide of a <see cref="PrsesntationContainer"/>.
    /// </summary>
    public class PresentationSlide : Container
    {
        public PresentationSlide()
        {
            Alpha = 0;
            RelativeSizeAxes = Axes.Both;
        }

        public virtual void OnEntering()
        {
            Alpha = 1;
        }

        public virtual void OnExiting()
        {
            Alpha = 0;
        }
    }
}
