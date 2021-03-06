// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using Vignette.Application.Configuration;
using Vignette.Application.Graphics.Themes;
using Vignette.Application.IO;
using Vignette.Application.Live2D.Resources;

namespace Vignette.Application
{
    public class VignetteApplicationBase : Game
    {
        public bool IsDevelopmentBuild { get; }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        private DependencyContainer dependencies;

        protected override Container<Drawable> Content => content;

        private readonly Container content;

        protected Storage Storage { get; set; }

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
                        Child = content = new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(VignetteApplicationBase).Assembly), @"Resources"));
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(CubismResources.ResourceAssembly), @"Resources"));

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

            AddFont(Resources, @"Fonts/Raleway-Bold");
            AddFont(Resources, @"Fonts/Raleway-BoldItalic");

            dependencies.CacheAs(this);
            dependencies.CacheAs(LocalConfig);
            dependencies.CacheAs(new ThemeStore(Storage));
            dependencies.CacheAs(new ObservedVideoStore(Storage));
            dependencies.CacheAs(new ObservedTextureStore(Storage));
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
