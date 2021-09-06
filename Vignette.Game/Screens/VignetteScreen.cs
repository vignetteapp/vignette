// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using Vignette.Game.Input;
using Vignette.Game.Settings;

namespace Vignette.Game.Screens
{
    public class VignetteScreen : Screen, IKeyBindingHandler<GlobalAction>
    {
        protected virtual bool CanOpenSettingsOverlay => true;

        [Resolved]
        private SettingsOverlay settings { get; set; }

        public override void OnEntering(IScreen last)
        {
            if (settings.State.Value == Visibility.Visible && !CanOpenSettingsOverlay)
                settings.State.Value = Visibility.Hidden;

            base.OnEntering(last);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleSettings:
                    if (CanOpenSettingsOverlay)
                        settings.ToggleVisibility();
                    return true;

                default:
                    return false;
            }
        }

        public void OnReleased(GlobalAction action)
        {
        }
    }
}
