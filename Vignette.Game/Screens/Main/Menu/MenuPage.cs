// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;

namespace Vignette.Game.Screens.Main.Menu
{
    public abstract class MenuPage : VisibilityContainer
    {
        public abstract LocalisableString Title { get; }

        public abstract IconUsage Icon { get; }

        protected override bool StartHidden => true;

        public MenuPage()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
        }

        protected override void PopIn() => this.FadeIn();

        protected override void PopOut() => this.FadeOut();
    }
}
