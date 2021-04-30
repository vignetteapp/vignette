// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Screens;
using Vignette.Game.Configuration;
using Vignette.Game.IO;
using Vignette.Game.Screens;
using Vignette.Game.Screens.Menu;
using Vignette.Game.Themeing;

namespace Vignette.Game
{
    public class VignetteGame : VignetteGameBase
    {
        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public VignetteGame()
        {
            Name = @"Vignette";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.CacheAs<IThemeSource>(new ThemeManager(UserResources, LocalConfig));
            Add(new ScreenStack(new MainMenu()));
        }
    }
}
