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
using osu.Framework.Platform;
using Vignette.Application.Configuration.Settings.Components;
using Vignette.Application.Graphics.Interface;

namespace Vignette.Application.Configuration.Settings.Sections
{
    public class PlatformSettingSection : SettingSection
    {
        public override string Label => "Platform";

        public override IconUsage Icon => FontAwesome.Solid.LayerGroup;

        private readonly BindableList<Size> resolutions = new BindableList<Size>();

        private IBindableList<WindowMode> windowModes = new BindableList<WindowMode>();

        private IBindable<Display> currentDisplay = new Bindable<Display>();

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager frameworkConfig, Storage storage, GameHost host)
        {
            if (host.Window != null)
            {
                currentDisplay.BindTo(host.Window.CurrentDisplayBindable);
                windowModes.BindTo(host.Window.SupportedWindowModes);
            }

            Children = new Drawable[]
            {
                new Label { Text = "Display" },
                new LabelledDropdown<WindowMode>
                {
                    Label = "Window Mode",
                    ItemSource = windowModes,
                    Current = frameworkConfig.GetBindable<WindowMode>(FrameworkSetting.WindowMode),
                },
                new ResolutionSettingDropdown
                {
                    Label = "Resolution",
                    ItemSource = resolutions,
                    Current = frameworkConfig.GetBindable<Size>(FrameworkSetting.SizeFullscreen),
                },
                new Label { Text = "Renderer" },
                new LabelledEnumDropdown<FrameSync>
                {
                    Label = "Frame Limiter",
                    Current = frameworkConfig.GetBindable<FrameSync>(FrameworkSetting.FrameSync),
                },
                new LabelledEnumDropdown<ExecutionMode>
                {
                    Label = "Threading Mode",
                    Current = frameworkConfig.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode),
                },
                new Label { Text = "Storage" },
                new ButtonText
                {
                    Text = "Open Vignette Folder",
                    Width = 200,
                    Action = () => storage.OpenInNativeExplorer(),
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            currentDisplay.BindValueChanged(display => Schedule(() =>
            {
                resolutions.AddRange(
                    display.NewValue.DisplayModes
                        .Where(m => m.Size.Width >= 800 && m.Size.Height >= 600)
                        .OrderByDescending(m => Math.Max(m.Size.Height, m.Size.Width))
                        .Select(m => m.Size)
                        .Distinct()
                );
            }), true);
        }

        private class ResolutionSettingDropdown : LabelledDropdown<Size>
        {
            protected override Drawable CreateControl() => new ResolutionDropdownControl();

            private class ResolutionDropdownControl : VignetteDropdown<Size>
            {
                protected override string GenerateItemText(Size item)
                {
                    return $"{item.Width} x {item.Height}";
                }
            }
        }
    }
}
