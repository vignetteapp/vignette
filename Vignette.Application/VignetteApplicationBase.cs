// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.Input;
using Vignette.Application.IO;

namespace Vignette.Application
{
    public class VignetteApplicationBase : Game
    {
        public string Version => GetType().Assembly.GetName().Version.ToString(3);

        public bool IsDevelopmentBuild { get; private set; }

        public static bool IsInsiderBuild =>
#if INSIDERS
                true;
#else
                false;
#endif

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        private DependencyContainer dependencies;

        protected override Container<Drawable> Content => content;

        private readonly Container content;

        protected Storage Storage { get; private set; }

        protected ApplicationConfigManager LocalConfig;

        public VignetteApplicationBase()
        {
            IsDevelopmentBuild = DebugUtils.IsDebugBuild;

            AddRangeInternal(new[]
            {
                new SafeAreaContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new DrawSizePreservingFillContainer
                    {
                        Child = new GlobalActionContainer(this)
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = content = new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                        },
                    },
                },
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(VignetteApplicationBase).Assembly), @"Resources"));

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

            dependencies.CacheAs(this);
            dependencies.CacheAs(LocalConfig);

            var files = Storage.GetStorageForDirectory("files");
            dependencies.CacheAs(new BackgroundImageStore(Scheduler, files));
            dependencies.CacheAs(new BackgroundVideoStore(Scheduler, files));
            dependencies.CacheAs(new ThemeStore(Scheduler, files, new NamespacedResourceStore<byte[]>(Resources, "Themes")));
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            var fpsDisplayBindable = LocalConfig.GetBindable<bool>(ApplicationSetting.ShowFpsOverlay);
            fpsDisplayBindable.ValueChanged += val => { FrameStatistics.Value = val.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None; };
            fpsDisplayBindable.TriggerChange();

            FrameStatistics.ValueChanged += e => fpsDisplayBindable.Value = e.NewValue != FrameStatisticsMode.None;
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Storage ??= host.Storage;
            LocalConfig ??= IsDevelopmentBuild
                ? new DevelopmentApplicationConfigManager(Storage)
                : new ApplicationConfigManager(Storage);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            LocalConfig?.Dispose();
        }
    }
}
