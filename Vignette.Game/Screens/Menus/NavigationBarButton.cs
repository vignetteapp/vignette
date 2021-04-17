// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Screens.Menus
{
    public class NavigationBarButton : VignetteButton, IHasText
    {
        protected new NavigationBarItem Label => base.Label as NavigationBarItem;

        public LocalisableString Text
        {
            get => Label.Text;
            set => Label.Text = value;
        }

        public IconUsage Icon
        {
            get => Label.Icon;
            set => Label.Icon = value;
        }

        protected override Drawable CreateLabel() => new NavigationBarItem();
    }
}
