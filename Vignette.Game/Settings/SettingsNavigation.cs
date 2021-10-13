// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Settings
{
    public class SettingsNavigation : NavigationVerticalTabControl<SettingsSection>
    {
        public SettingsNavigation()
        {
            AutoSizeAxes = Axes.Y;
            TabContainer.RelativeSizeAxes = Axes.X;
            TabContainer.AutoSizeAxes = Axes.Y;
        }

        protected override TabItem<SettingsSection> CreateTabItem(SettingsSection value)
            => new MainMenuNavigationTabItem(value);

        protected class MainMenuNavigationTabItem : NavigationVerticalTabItem
        {
            protected override IconUsage? Icon => Value.Icon;

            protected override bool ForceTextMargin => false;

            public MainMenuNavigationTabItem(SettingsSection value)
                : base(value)
            {
                Height = 40;
            }
        }
    }
}
