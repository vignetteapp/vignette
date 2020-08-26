using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Input;
using osu.Framework.IO.Stores;

namespace holotrack.Core
{
    public class HoloTrackGame : Game
    {
        private DependencyContainer dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public HoloTrackGame()
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

            AddFont(Resources, @"Fonts/NotoSans");
        }
    }
}