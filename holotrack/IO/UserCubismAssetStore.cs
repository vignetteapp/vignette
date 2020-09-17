using System.IO;
using CubismFramework;
using osu.Framework.Graphics.Cubism;

namespace holotrack.IO
{
    public class UserCubismAssetStore : CubismAssetStore
    {
        private readonly FileStore files;

        public UserCubismAssetStore(FileStore files)
            : base(files.Store)
        {
            this.files = files;
        }

        public new CubismAsset Get(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            return new CubismAsset(name, (path) => GetStream(files.Retrieve(Path.GetFileName(path))));
        }
    }
}