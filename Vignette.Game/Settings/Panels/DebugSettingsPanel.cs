// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Panels
{
    public class DebugSettingsPanel : SettingsSubPanel
    {
        public DebugSettingsPanel()
        {
            Children = new Drawable[]
            {
                new BuildInformationSection(),
                new DebugControlsSection(),
            };
        }

        private class BuildInformationSection : SettingsSection
        {
            public override LocalisableString Label => "Build Information";
        }

        private class DebugControlsSection : SettingsSection
        {
            [BackgroundDependencyLoader]
            private void load(VignetteConfigManager gameConfig, FrameworkConfigManager frameworkConfig, FrameworkDebugConfigManager debugConfig, Storage storage)
            {
                Child = new SettingsSubSection
                {
                    Children = new Drawable[]
                    {
                        new SettingsSwitch
                        {
                            Label = "Show FPS",
                            Current = gameConfig.GetBindable<bool>(VignetteSetting.ShowFpsOverlay),
                        },
                        new SettingsSwitch
                        {
                            Label = "Show log overlay",
                            Current = frameworkConfig.GetBindable<bool>(FrameworkSetting.ShowLogOverlay),
                        },
                        new SettingsSwitch
                        {
                            Label = "Bypass front-to-back render pass",
                            Current = debugConfig.GetBindable<bool>(DebugSetting.BypassFrontToBackPass),
                        },
                        new OpenExternalLinkButton(storage.GetStorageForDirectory("./logs"))
                        {
                            Label = "Open logs folder"
                        },
                    },
                };
            }
        }
    }
}
