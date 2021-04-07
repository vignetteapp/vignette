// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Graphics.Performance;
using osu.Framework.Platform;
using Vignette.Game.Configuration;

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

        public VignetteGameBase()
        {
            IsDebugBuild = DebugUtils.IsDebugBuild;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.CacheAs(LocalConfig);

            var showFps = LocalConfig.Config.ShowFpsOverlay.GetBoundCopy();
            showFps.BindValueChanged(e => FrameStatistics.Value = e.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None, true);
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
