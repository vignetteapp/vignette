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
        protected virtual bool AllowToggleSettings => true;

        protected virtual bool ShowSettingsOneEnter => false;

        [Resolved]
        private SettingsOverlay settings { get; set; }

        public override void OnEntering(IScreen last)
        {
            if (settings.State.Value == Visibility.Visible && !AllowToggleSettings)
                settings.Hide();

            if (AllowToggleSettings && ShowSettingsOneEnter)
                settings.Show();

            base.OnEntering(last);
        }

        public bool OnPressed(GlobalAction action)
        {
            switch (action)
            {
                case GlobalAction.ToggleSettings:
                    if (AllowToggleSettings)
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
