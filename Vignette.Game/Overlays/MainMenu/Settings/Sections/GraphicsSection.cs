// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using Vignette.Game.Overlays.MainMenu.Settings.Components;

namespace Vignette.Game.Overlays.MainMenu.Settings.Sections
{
    public class GraphicsSection : SettingsSection
    {
        public override LocalisableString Header => "Graphics";

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager config)
        {
            AddRange(new Drawable[]
            {
                new SettingsEnumDropdown<FrameSync>
                {
                    Label = "Frame limiter",
                    Current = config.GetBindable<FrameSync>(FrameworkSetting.FrameSync),
                },
                new SettingsEnumDropdown<ExecutionMode>
                {
                    Label = "Threading mode",
                    Current = config.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode),
                },
            });
        }
    }
}
