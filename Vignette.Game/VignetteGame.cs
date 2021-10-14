// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using Vignette.Game.Screens;
using Vignette.Game.Settings;

namespace Vignette.Game
{
    public class VignetteGame : VignetteGameBase
    {
        public ScreenStack ScreenStack { get; private set; }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        private DependencyContainer dependencies;
        private SettingsOverlay overlay;

        public VignetteGame()
        {
            Name = @"Vignette";
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            dependencies.CacheAs(this);
            dependencies.CacheAs(overlay = new SettingsOverlay());

            AddRange(new Drawable[]
            {
                ScreenStack = new ScreenStack(new SplashScreen()),
                overlay,
            });

            overlay.State.ValueChanged += _ => handleOverlayStateChange();
        }

        private void handleOverlayStateChange()
        {
            bool visible = overlay.State.Value == Visibility.Visible;
            ScreenStack.TransformTo(nameof(Margin), new MarginPadding { Left = visible ? SettingsSidebar.WIDTH : 0 }, 300, visible ? Easing.OutCirc : Easing.InCirc);
        }
    }
}
