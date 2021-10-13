// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Settings.Components;

namespace Vignette.Game.Settings.Panels
{
    public class DebugSettingsPanel : SettingsSubPanel
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();
            Children = new Drawable[]
            {
                new BuildInformationSection(),
                new DebugControlsSection(),
            };
        }

        private class BuildInformationSection : SettingsSection
        {
            public override LocalisableString Label => "Build Information";

            public BuildInformationSection()
            {
                Child = new DetailsItem();
            }

            private class DetailsItem : SettingsItem
            {
                private TableContainer table;
                private readonly Dictionary<LocalisableString, string> details = new Dictionary<LocalisableString, string>();

                [BackgroundDependencyLoader]
                private void load(VignetteGameBase game)
                {
                    details.Add("Version", game.Version);
                    details.Add("Channel", VignetteGameBase.IsInsidersBuild ? "Insiders" : "Public");
                    details.Add("Framework Version", resolveFrameworkVersion());
                    details.Add("Live2D Runtime Version", "Unknown");
                    details.Add("Live2D Model Version", "Unknown");
                }

                protected override void LoadComplete()
                {
                    base.LoadComplete();

                    Foreground.RelativeSizeAxes = Axes.X;
                    Foreground.Margin = new MarginPadding(10);
                    Foreground.Child = table = new TableContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        ShowHeaders = false,
                        Columns = new[]
                        {
                            new TableColumn(null, Anchor.TopRight, new Dimension(GridSizeMode.Absolute, 120)),
                            new TableColumn(null, Anchor.TopLeft, new Dimension(GridSizeMode.Absolute, 10)),
                            new TableColumn(null, Anchor.TopLeft, new Dimension(GridSizeMode.Distributed)),
                        },
                    };

                    var content = new Drawable[details.Count, 3];

                    int i = 0;
                    foreach ((var key, string value) in details)
                    {
                        content[i, 0] = new ThemableSpriteText
                        {
                            Colour = ThemeSlot.Black,
                            Font = SegoeUI.Bold,
                            Text = key,
                        };

                        content[i, 1] = null;

                        content[i, 2] = new ThemableSpriteText
                        {
                            Colour = ThemeSlot.Black,
                            Text = value,
                        };

                        i++;
                    }

                    table.Content = content;
                    Foreground.Height = 16 * details.Count;
                }

                private static string resolveFrameworkVersion()
                {
                    var version = typeof(osu.Framework.Game).Assembly.GetName().Version;
                    return $"{version.Major}.{version.Minor}.{version.Build}";
                }
            }
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
