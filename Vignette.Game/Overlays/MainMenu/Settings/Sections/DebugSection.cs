// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Overlays.MainMenu.Settings.Components;

namespace Vignette.Game.Overlays.MainMenu.Settings.Sections
{
    public class DebugSection : SettingsSection
    {
        public override LocalisableString Header => "Debug";

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager gameConfig, FrameworkConfigManager frameworkConfig, FrameworkDebugConfigManager debugConfig, VignetteGameBase game, Storage storage)
        {
            AddRange(new Drawable[]
            {
                new SettingsLabel
                {
                    Label = "Version",
                    Text = game.Version,
                },
                new SettingsCheckbox
                {
                    Label = "Show FPS",
                    Current = gameConfig.GetBindable<bool>(VignetteSetting.ShowFpsOverlay),
                },
                new SettingsCheckbox
                {
                    Label = "Show log overlay",
                    Current = frameworkConfig.GetBindable<bool>(FrameworkSetting.ShowLogOverlay),
                },
                new SettingsCheckbox
                {
                    Label = "Bypass front-to-back render pass",
                    Current = debugConfig.GetBindable<bool>(DebugSetting.BypassFrontToBackPass),
                },
                new SettingsButton
                {
                    Label = "Open logs folder",
                    Icon = FluentSystemIcons.WindowNew24,
                    Action = () => storage.OpenPathInNativeExplorer("./logs"),
                },
            });
        }
    }
}
