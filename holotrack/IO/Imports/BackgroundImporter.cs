using System.Collections.Generic;
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

        protected override IEnumerable<FileMetadata> Import(string path) => new[] { Files.Add(path, File.OpenRead(path), FileType.Background) };
        protected override IEnumerable<FileMetadata> Populate() => Files.Context.Find(f => f.Type == FileType.Background);
    }
}