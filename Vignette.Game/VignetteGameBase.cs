// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.IO;
using Vignette.Game.Resources;
using Vignette.Game.Themeing;

namespace Vignette.Game
{
    public class VignetteGameBase : osu.Framework.Game
    {
        public bool IsDebugBuild { get; private set; }

        protected Storage Storage;

        protected VignetteConfigManager LocalConfig;

        protected UserResources UserResources;

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        private Bindable<bool> showFps;

        private Container content;

        protected override Container<Drawable> Content => content;

        public VignetteGameBase()
        {
            IsDebugBuild = DebugUtils.IsDebugBuild;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(VignetteResources.ResourceAssembly));

            AddFont(Resources, @"Fonts/SegoeUI");
            AddFont(Resources, @"Fonts/SegoeUI-Italic");
            AddFont(Resources, @"Fonts/SegoeUI-Bold");
            AddFont(Resources, @"Fonts/SegoeUI-BoldItalic");
            AddFont(Resources, @"Fonts/SegoeUI-Black");
            AddFont(Resources, @"Fonts/SegoeUI-BlackItalic");
            AddFont(Resources, @"Fonts/SegoeUI-Light");
            AddFont(Resources, @"Fonts/SegoeUI-LightItalic");
            AddFont(Resources, @"Fonts/SegoeUI-SemiBold");
            AddFont(Resources, @"Fonts/SegoeUI-SemiBoldItalic");
            AddFont(Resources, @"Fonts/SegoeUI-SemiLight");
            AddFont(Resources, @"Fonts/SegoeUI-SemiLightItalic");

            AddFont(Resources, @"Fonts/Spartan-Bold");

            AddFont(Resources, @"Fonts/FluentSystemIcons-Filled");
            AddFont(Resources, @"Fonts/Vignette");

            dependencies.CacheAs(LocalConfig);

            UserResources = new UserResources(Host, Storage);
            dependencies.CacheAs(UserResources);
            dependencies.CacheAs(new ThemeManager(UserResources, LocalConfig));

            showFps = LocalConfig.GetBindable<bool>(VignetteSetting.ShowFpsOverlay);
            showFps.BindValueChanged(e => FrameStatistics.Value = e.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None, true);

            base.Content.Add(new SafeAreaContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new DrawSizePreservingFillContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = content = new Container
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                }
            });
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Storage ??= host.Storage;
            LocalConfig ??= IsDebugBuild
                ? new VignetteDevelopmentConfigManager(Storage)
                : new VignetteConfigManager(Storage);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            LocalConfig?.Dispose();
            UserResources?.Dispose();
        }
    }
}
