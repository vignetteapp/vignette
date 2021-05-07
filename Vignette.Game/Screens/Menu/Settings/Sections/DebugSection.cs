// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Themeing;

namespace Vignette.Game.Screens.Menu.Settings.Sections
{
    public class DebugSection : SettingsSection
    {
        public override LocalisableString Header => "Debug";

        private ThemableSpriteText versionText;

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager gameConfig, FrameworkConfigManager frameworkConfig, FrameworkDebugConfigManager debugConfig, VignetteGameBase game, Storage storage)
        {
            AddRange(new Drawable[]
            {
                new Container
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Margin = new MarginPadding { Bottom = 30 },
                    Child = versionText = new ThemableSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = game.Version,
                        Font = SegoeUI.Bold.With(size: 24),
                    },
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
                    Action = () => storage.OpenPathInNativeExplorer("./logs"),
                },
            });

            if (!game.IsDeployedBuild && game.IsDebugBuild)
                versionText.Colour = ThemeSlot.Error;
            else if (!game.IsDeployedBuild)
                versionText.Colour = ThemeSlot.Success;
            else
                versionText.Colour = ThemeSlot.Gray190;
        }
    }
}
