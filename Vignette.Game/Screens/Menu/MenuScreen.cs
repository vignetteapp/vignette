// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;

namespace Vignette.Game.Screens.Menu
{
    public abstract class MenuScreen : VisibilityContainer
    {
        public abstract LocalisableString Title { get; }

        public abstract IconUsage Icon { get; }

        protected override bool StartHidden => true;

        public MenuScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        protected override void PopIn()
        {
            this
                .ScaleTo(1.1f)
                .ScaleTo(1.0f, 200, Easing.OutQuint)
                .FadeInFromZero(200, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            this
                .ScaleTo(1.1f, 200, Easing.OutQuint)
                .FadeOutFromOne();
        }
    }
}
