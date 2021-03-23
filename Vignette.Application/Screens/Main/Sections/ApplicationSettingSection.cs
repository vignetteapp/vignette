// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using System.Drawing;
using System.Linq;
using Humanizer;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using osuTK;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics;
using Vignette.Application.Graphics.Containers;
using Vignette.Application.Graphics.Interface;
using Vignette.Application.Graphics.Sprites;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.IO.Monitors;
using Vignette.Application.Screens.Main.Controls;

namespace Vignette.Application.Screens.Main.Sections
{
    public class ApplicationSettingSection : ToolbarSection
    {
        public override string Title => "Settings";

        public override IconUsage Icon => FluentSystemIcons.Filled.Settings24;

        private ThemeSettingDropdown themesDropdown;

        private ResolutionSettingDropdown resolutionDropdown;

        private ThemedCheckbox resizableCheckbox;

        private readonly BindableList<Size> resolutions = new BindableList<Size>();

        private readonly IBindableList<WindowMode> windowModes = new BindableList<WindowMode>();

        private readonly IBindable<Display> currentDisplay = new Bindable<Display>();

        [BackgroundDependencyLoader]
        private void load(ApplicationConfigManager appConfig, FrameworkConfigManager frameworkConfig, VignetteApplicationBase app, GameHost host, TextureStore textures, ThemeStore themes, Storage storage)
        {
            if (host.Window != null)
            {
                currentDisplay.BindTo(host.Window.CurrentDisplayBindable);
                windowModes.BindTo(host.Window.SupportedWindowModes);
            }

            var windowSizeBindable = frameworkConfig.GetBindable<Size>(FrameworkSetting.WindowedSize);
            var brandingText = new ThemedTextFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AutoSizeAxes = Axes.Y,
                TextAnchor = Anchor.BottomCentre,
                RelativeSizeAxes = Axes.X,
            };

            brandingText.AddText(app.Version, (s) => s.Font = SegoeUI.SemiBold.With(size: 16, fixedWidth: true));

            Children = new Drawable[]
            {
                new ThemedSpriteText
                {
                    Font = SegoeUI.Bold.With(size: 18),
                    Text = "Interface"
                },
                themesDropdown = new ThemeSettingDropdown
                {
                    Label = "Theme",
                    ItemSource = themes.Loaded,
                },
                new ThemedTextButton
                {
                    Text = "Open Themes Folder",
                    Width = 200,
                    Action = () => themes.OpenInNativeExplorer(),
                },
                new ThemedSpriteText
                {
                    Font = SegoeUI.Bold.With(size: 18),
                    Text = "Graphics"
                },
                new LabelledDropdown<WindowMode>
                {
                    Label = "Window Mode",
                    ItemSource = windowModes,
                    Current = frameworkConfig.GetBindable<WindowMode>(FrameworkSetting.WindowMode),
                },
                resizableCheckbox = new ThemedCheckbox
                {
                    Text = "Resizable Window",
                    Current = appConfig.GetBindable<bool>(ApplicationSetting.WindowResizable),
                },
                resolutionDropdown = new ResolutionSettingDropdown
                {
                    Label = "Resolution",
                    ItemSource = resolutions,
                    Current = frameworkConfig.GetBindable<Size>(FrameworkSetting.SizeFullscreen),
                },
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
                new ThemedSpriteText
                {
                    Font = SegoeUI.Bold.With(size: 18),
                    Text = "Debug"
                },
                new ThemedCheckbox
                {
                    Text = "Show FPS",
                    Current = appConfig.GetBindable<bool>(ApplicationSetting.ShowFpsOverlay),
                },
                new ThemedCheckbox
                {
                    Text = "Show Log Overlay",
                    Current = frameworkConfig.GetBindable<bool>(FrameworkSetting.ShowLogOverlay),
                },
                new ThemedTextButton
                {
                    Text = "Open Vignette Folder",
                    Width = 200,
                    Action = () => storage.OpenInNativeExplorer(),
                },
                new ThemedTextButton
                {
                    Text = "Open Logs Folder",
                    Width = 200,
                    Action = () => storage.OpenPathInNativeExplorer("./logs"),
                },
                new FillFlowContainer
                {
                    Margin = new MarginPadding { Top = 30 },
                    Spacing = new Vector2(0, 15),
                    Direction = FillDirection.Vertical,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new ThemedSprite
                        {
                            Size = new Vector2(32),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Texture = textures.Get("branding"),
                        },
                        brandingText
                    }
                }
            };

            resolutionDropdown.Current.BindValueChanged((e) => windowSizeBindable.Value = e.NewValue, true);
            resizableCheckbox.Current.BindValueChanged((e) =>
            {
                if (host.Window is SDL2DesktopWindow window)
                    window.Resizable = e.NewValue;

                resolutionDropdown.Alpha = !e.NewValue ? 1 : 0;

                if (!e.NewValue)
                    resolutionDropdown.Current.TriggerChange();
            }, true);

            var themeConfig = appConfig.GetBindable<string>(ApplicationSetting.Theme);
            themesDropdown.Current.Value = themes.GetReference(themeConfig.Value);
            themesDropdown.Current.ValueChanged += e => themeConfig.Value = e.NewValue.Name;
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

        private class ThemeSettingDropdown : LabelledFileDropdown<Theme>
        {
            protected override Drawable CreateControl() => new ThemeDropdownControl();

            private class ThemeDropdownControl : LabelledFileDropdownControl
            {
                protected override LocalisableString GenerateItemText(MonitoredFile<Theme> item)
                    => item.Name.Humanize(LetterCasing.Title);
            }
        }

        private class ResolutionSettingDropdown : LabelledDropdown<Size>
        {
            protected override Drawable CreateControl() => new ResolutionDropdownControl();

            private class ResolutionDropdownControl : ThemedDropdown<Size>
            {
                protected override LocalisableString GenerateItemText(Size item)
                {
                    return $"{item.Width} x {item.Height}";
                }
            }
        }
    }
}
