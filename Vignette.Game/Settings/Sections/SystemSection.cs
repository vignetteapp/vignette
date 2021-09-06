// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Drawing;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Settings.Components;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Settings.Sections
{
    public class SystemSection : SettingsSection
    {
        public override IconUsage Icon => SegoeFluent.Settings;

        public override LocalisableString Label => "System";

        private SettingsSwitch resizableSetting;
        private ResolutionDropdown resolutionSetting;
        private SettingsDropdown<WindowMode> windowModeSetting;
        private readonly IBindableList<WindowMode> windowModes = new BindableList<WindowMode>();
        private readonly IBindable<Display> currentDisplay = new Bindable<Display>();
        private readonly BindableList<Size> resolutions = new BindableList<Size>();

        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager gameConfig, FrameworkConfigManager frameworkConfig, ThemeManagingContainer themeManager, GameHost host, Storage storage)
        {
            if (host.Window != null)
            {
                windowModes.BindTo(host.Window.SupportedWindowModes);
                currentDisplay.BindTo(host.Window.CurrentDisplayBindable);
            }

            Children = new Drawable[]
            {
                new SettingsSubSection
                {
                    Label = "General",
                    Children = new Drawable[]
                    {
                        new SettingsDropdown<string>
                        {
                            Icon = SegoeFluent.LocalLanguage,
                            Label = "Language",
                            Items = new[] { "English" },
                        },
                    }
                },
                new SettingsSubSection
                {
                    Label = "Graphics",
                    Children = new Drawable[]
                    {
                        new SettingsEnumDropdown<FrameSync>
                        {
                            Label = "Frame limiter",
                            Current = frameworkConfig.GetBindable<FrameSync>(FrameworkSetting.FrameSync),
                        },
                        new SettingsEnumDropdown<ExecutionMode>
                        {
                            Label = "Threading mode",
                            Current = frameworkConfig.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode),
                        },
                    }
                },
                new SettingsSubSection
                {
                    Label = "Window",
                    Children = new Drawable[]
                    {
                        resizableSetting = new SettingsSwitch
                        {
                            Label = "Allow window to be resizable",
                            Current = gameConfig.GetBindable<bool>(VignetteSetting.WindowResizable),
                        },
                        windowModeSetting = new SettingsDropdown<WindowMode>
                        {
                            Label = "Window mode",
                            Current = frameworkConfig.GetBindable<WindowMode>(FrameworkSetting.WindowMode),
                            ItemSource = windowModes,
                        },
                        resolutionSetting = new ResolutionDropdown
                        {
                            Label = "Resolution",
                        },
                    },
                },
                new SettingsSubSection
                {
                    Label = "Appearance",
                    Children = new Drawable[]
                    {
                        new ThemeDropdown
                        {
                            Icon = SegoeFluent.PaintBrush,
                            Label = "Theme",
                            Current = themeManager.Current,
                            ItemSource = themeManager.UseableThemes,
                        },
                        new OpenSubPanelButton<ThemeDesignerPanel>(themeManager)
                        {
                            Label = "Open theme designer",
                        },
                        new OpenExternalLinkButton(themeManager.Store)
                        {
                            Label = "Open themes folder",
                        },
                    },
                },
                new SettingsSubSection
                {
                    Label = "Developer",
                    Children = new Drawable[]
                    {
                        new OpenExternalLinkButton(storage)
                        {
                            Label = "Open Vignette folder",
                        },
                        new OpenSubPanelButton<DebugSettingsPanel>
                        {
                            Icon = SegoeFluent.WindowDevTools,
                            Label = "Open debug settings",
                        },
                    }
                }
            };

            windowModeSetting.Current.BindValueChanged(e =>
            {
                resolutionSetting.Current = e.NewValue == WindowMode.Windowed
                    ? gameConfig.GetBindable<Size>(VignetteSetting.WindowSize)
                    : frameworkConfig.GetBindable<Size>(FrameworkSetting.SizeFullscreen);
            }, true);

            resizableSetting.Current.BindValueChanged(e =>
            {
                windowModeSetting.Alpha = resolutionSetting.Alpha = !e.NewValue ? 1 : 0;
            }, true);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            currentDisplay.BindValueChanged(display => Schedule(() =>
            {
                if (display.NewValue != null)
                {
                    resolutions.AddRange(
                        display.NewValue.DisplayModes
                            .Where(m => m.Size.Width >= 800 && m.Size.Height >= 600)
                            .OrderByDescending(m => Math.Max(m.Size.Height, m.Size.Width))
                            .Select(m => m.Size)
                            .Distinct()
                    );
                }

                // Check if the framework default window size is present and append it
                if (resolutions.FirstOrDefault(m => m.Width == 1366 && m.Height == 768).Equals(default))
                {
                    resolutionSetting.Items = resolutions
                        .Append(new Size(1366, 768))
                        .OrderByDescending(m => Math.Max(m.Height, m.Width))
                        .Skip(1);
                }
                else
                {
                    // Don't include the first item as it is basically borderless.
                    resolutionSetting.Items = resolutions.Skip(1);
                }
            }), true);
        }

        private class ResolutionDropdown : SettingsDropdown<Size>
        {
            protected override FluentDropdown<Size> CreateControl() => new ResolutionDropdownControl { Width = 125 };

            private class ResolutionDropdownControl : FluentDropdown<Size>
            {
                protected override LocalisableString GenerateItemText(Size item) => $"{item.Width} x {item.Height}";
            }
        }

        private class ThemeDropdown : SettingsDropdown<Theme>
        {
            protected override FluentDropdown<Theme> CreateControl() => new ThemeDropdownControl { Width = 125 };

            private class ThemeDropdownControl : FluentDropdown<Theme>
            {
                protected override LocalisableString GenerateItemText(Theme item) => item.Name;
            }
        }
    }
}
