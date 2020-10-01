using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace vignette.IO.Imports
{
    public class CubismAssetImporter : Importer
    {
        private const string model_extension = @".model3.json";

        public CubismAssetImporter(FileStore files)
            : base(files)
        {
            AddExtension(".zip");
        }

        protected override IEnumerable<FileMetadata> Import(string path)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                if (!archive.Entries.Any(f => f.Name.Contains(model_extension)))
                    throw new FileNotFoundException("The provided zip file is not a valid Live2D model.");

                var files = new List<FileMetadata>();
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    using (var stream = entry.Open())
                    {
                        var memStream = new MemoryStream();
                        stream.CopyTo(memStream);
                        files.Add(Files.Add(entry.FullName, memStream, FileType.ModelAsset));
                    }
                }

                return files;
            }
        }

        protected override IEnumerable<FileMetadata> Populate() => Files.Context.Find(f => f.Type == FileType.ModelAsset && f.Path.EndsWith(model_extension));
    }
}