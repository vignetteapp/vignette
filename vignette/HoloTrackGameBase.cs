using System.Collections.Generic;
using System.Linq;
using FaceRecognitionDotNet;
using vignette.Configuration;
using vignette.IO;
using vignette.IO.Imports;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Cubism;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace vignette
{
    public class VignetteGameBase : Game
    {
        protected VignetteConfigManager LocalConfig;
        protected Storage Storage { get; set; }
        protected FileStore Files;

        private readonly List<Importer> importers = new List<Importer>();
        private DependencyContainer dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public VignetteGameBase()
        {
            Name = @"vignette";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(CubismResources.ResourceAssembly), @"Resources"));
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(VignetteGame).Assembly), @"Resources"));

            dependencies.Cache(LocalConfig);
            dependencies.Cache(Files);

            var cubismAssets = new CubismAssetStore(new NamespacedResourceStore<byte[]>(Resources, @"Live2D"));
            dependencies.Cache(cubismAssets);

            var userCubismAssets = new UserCubismAssetStore(Files);
            dependencies.Cache(userCubismAssets);

            Textures.AddStore(new TextureLoaderStore(Files.Store));

            var cameraManager = new CameraManager(Host.UpdateThread) { EventScheduler = Scheduler };
            dependencies.Cache(cameraManager);

            dependencies.CacheAs<IReadOnlyList<Importer>>(importers);

            // Temporarily read the models in the output directory. We'll have a better support for embedded resources at a later date.
            dependencies.Cache(FaceRecognition.Create($"{RuntimeInfo.StartupDirectory}/models"));

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
            LocalConfig ??= new VignetteConfigManager(Storage);

            Files ??= new FileStore(Storage);
            importers.Add(new BackgroundImporter(Files));
            importers.Add(new CubismAssetImporter(Files));
        }

        public void Import(string file) => importers.Where(i => i.IsFileSupported(file)).FirstOrDefault()?.Add(file);
    }
}