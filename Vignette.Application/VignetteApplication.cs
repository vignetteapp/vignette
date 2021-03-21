// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Screens;
using Vignette.Application.Screens.Intro;

namespace Vignette.Application
{
    public class VignetteApplication : VignetteApplicationBase
    {
        public VignetteApplication()
        {
            Name = $"Vignette{(IsInsiderBuild ? " Insiders" : string.Empty)}";
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Add(new ScreenStack(new Loader())
            {
                RelativeSizeAxes = Axes.Both,
            });
        }
    }
}
