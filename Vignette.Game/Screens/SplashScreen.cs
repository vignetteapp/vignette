// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using Vignette.Game.Graphics.Typesets;

namespace Vignette.Game.Screens
{
    public class SplashScreen : Screen
    {
        private readonly SpriteIcon icon;

        public SplashScreen()
        {
            InternalChild = icon = new SpriteIcon
            {
                Size = new Vector2(64),
                Icon = VignetteFont.Logo,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            LoadComponentAsync(new StageScreen(), screen =>
            {
                icon.Delay(2000).FadeOutFromOne(500);
                Scheduler.AddDelayed(() => this.Push(screen), 3000);
            });
        }
    }
}
