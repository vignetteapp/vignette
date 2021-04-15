// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Platform;
using Vignette.Game.Configuration;
using Vignette.Game.IO;

namespace Vignette.Game
{
    public class VignetteGameBase : osu.Framework.Game
    {
        public bool IsDebugBuild { get; private set; }

        protected Storage Storage;

        protected VignetteConfigManager LocalConfig;

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        private Container content;

        protected override Container<Drawable> Content => content;

        public VignetteGameBase()
        {
            IsDebugBuild = DebugUtils.IsDebugBuild;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.CacheAs(LocalConfig);

            var showFps = LocalConfig.GetBindable<bool>(VignetteSetting.ShowFpsOverlay);
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
        }
    }
}
