using System.IO;

namespace holotrack.IO.Imports
{
    public class BackgroundImporter : Importer
    {
        public BackgroundImporter(FileStore files)
            : base(files)
        {
            AddExtension(".png");
            AddExtension(".jpg");
        }

        public override void Import(string path) => Files.Add(path, File.OpenRead(path), FileType.Background);
    }
}