// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Overlays.MainMenu
{
    public class MainMenuNavigationView : NavigationViewVertical<MenuView>
    {
        public MainMenuNavigationView()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            TabContainer.RelativeSizeAxes = Axes.X;
            TabContainer.AutoSizeAxes = Axes.Y;
        }

        protected override TabItem<MenuView> CreateTabItem(MenuView value)
            => new MainMenuNavigationTabItem(value);

        protected class MainMenuNavigationTabItem : NavigationViewVerticalTabItem
        {
            protected override LocalisableString? Text => Value.Title;

            protected override IconUsage? Icon => Value.Icon;

            public MainMenuNavigationTabItem(MenuView value)
                : base(value)
            {
            }
        }
    }
}
