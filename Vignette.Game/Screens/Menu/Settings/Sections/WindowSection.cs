// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Drawing;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.Graphics.UserInterface;

namespace Vignette.Game.Screens.Menu.Settings.Sections
{
    public class WindowSection : SettingsSection
    {
        public override LocalisableString Header => "Window";

        private SettingsCheckbox resizableSetting;
        private SettingsDropdown<Size> resolutionWindowedSetting;
        private SettingsDropdown<Size> resolutionFullscreenSetting;
        private SettingsDropdown<WindowMode> windowModeSetting;

        private readonly IBindableList<WindowMode> windowModes = new BindableList<WindowMode>();
        private readonly IBindable<Display> currentDisplay = new Bindable<Display>();
        private readonly BindableList<Size> resolutions = new BindableList<Size>();


        [BackgroundDependencyLoader]
        private void load(VignetteConfigManager gameConfig, FrameworkConfigManager frameworkConfig, GameHost host)
        {
            if (host.Window != null)
            {
                windowModes.BindTo(host.Window.SupportedWindowModes);
                currentDisplay.BindTo(host.Window.CurrentDisplayBindable);
            }

            AddRange(new Drawable[]
            {
                resizableSetting = new SettingsCheckbox
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
                resolutionFullscreenSetting = new ResolutionDropdown
                {
                    Label = "Resolution",
                    Current = frameworkConfig.GetBindable<Size>(FrameworkSetting.SizeFullscreen),
                    ItemSource = resolutions,
                },
                resolutionWindowedSetting = new ResolutionDropdown
                {
                    Label = "Resolution",
                    Current = gameConfig.GetBindable<Size>(VignetteSetting.WindowSize),
                },
            });

            // We cannot disable the bindable obtained from framework config as it is internally modified.
            var frameworkWindowSize = frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize);

            resolutionWindowedSetting.Current.BindValueChanged(e =>
            {
                if (!resizableSetting.Current.Value)
                    frameworkWindowSize.Value = e.NewValue;
            });

            resizableSetting.Current.BindValueChanged(e =>
            {
                resolutionWindowedSetting.Current.TriggerChange();
                windowModeSetting.Current.Disabled = e.NewValue;
                resolutionWindowedSetting.Current.Disabled = e.NewValue;
                resolutionFullscreenSetting.Current.Disabled = e.NewValue;
            }, true);

            windowModeSetting.Current.BindValueChanged(e =>
            {
                resizableSetting.Current.Disabled = e.NewValue != WindowMode.Windowed;

                Schedule(() =>
                {
                    resolutionWindowedSetting.Alpha = e.NewValue == WindowMode.Windowed ? 1 : 0;
                    resolutionFullscreenSetting.Alpha = e.NewValue == WindowMode.Fullscreen ? 1 : 0;
                });
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

                // Check if the framework default window size is present and append it if it isn't so the window size
                // wouldn't suddenly jump to an unexpected value when entering settings.
                if (resolutions.FirstOrDefault(m => m.Width == 1366 && m.Height == 768).Equals(default(Size)))
                {
                    resolutionWindowedSetting.Items = resolutions
                        .Append(new Size(1366, 768))
                        .OrderByDescending(m => Math.Max(m.Height, m.Width))
                        .Skip(1);
                }
                else
                {
                    // Don't include the first item as it is basically borderless.
                    resolutionWindowedSetting.Items = resolutions.Skip(1);
                }
            }), true);
        }

        private class ResolutionDropdown : SettingsDropdown<Size>
        {
            protected override Drawable CreateControl() => new ResolutionDropdownControl { Width = 200 };

            private class ResolutionDropdownControl : FluentDropdown<Size>
            {
                protected override LocalisableString GenerateItemText(Size item)
                    => $"{item.Width} x {item.Height}";
            }
        }
    }
}
