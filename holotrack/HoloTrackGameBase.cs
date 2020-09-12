using holotrack.Configuration;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Input;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace holotrack
{
    public class HoloTrackGameBase : Game
    {
        protected HoloTrackConfigManager LocalConfig;
        protected Storage Storage { get; set; }

        private DependencyContainer dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public HoloTrackGameBase()
        {
            Name = @"holotrack";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(CubismResources.ResourceAssembly), @"Resources"));
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(HoloTrackGame).Assembly), @"Resources"));

            var cubismAssets = new CubismAssetStore(new NamespacedResourceStore<byte[]>(Resources, @"Live2D"));
            dependencies.Cache(cubismAssets);

            var cameraManager = new CameraManager(Host.UpdateThread) { EventScheduler = Scheduler };
            dependencies.Cache(cameraManager);

            dependencies.Cache(LocalConfig);

            AddFont(Resources, @"Fonts/NotoExtraCond");
            AddFont(Resources, @"Fonts/NotoExtraCond-Italic");
            AddFont(Resources, @"Fonts/NotoExtraCond-Light");
            AddFont(Resources, @"Fonts/NotoExtraCond-LightItalic");
            AddFont(Resources, @"Fonts/NotoExtraCond-Bold");
            AddFont(Resources, @"Fonts/NotoExtraCond-BoldItalic");
            AddFont(Resources, @"Fonts/NotoExtraCond-Medium");
            AddFont(Resources, @"Fonts/NotoExtraCond-MediumItalic");
            AddFont(Resources, @"Fonts/NotoExtraCond-Black");
            AddFont(Resources, @"Fonts/NotoExtraCond-BlackItalic");
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Storage ??= host.Storage;
            LocalConfig ??= new HoloTrackConfigManager(Storage);
        }
    }
}