using System.IO;
using System.IO.Compression;
using System.Linq;

namespace holotrack.IO.Imports
{
    public class CubismAssetImporter : Importer
    {
        public CubismAssetImporter(FileStore files)
            : base(files)
        {
            AddExtension(".zip");
        }

        public override void Import(string path)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                if (!archive.Entries.Any(f => f.Name.Contains(".model3.json")))
                    throw new FileNotFoundException("The provided zip file is not a valid Live2D model.");

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    using (var stream = entry.Open())
                    {
                        var memStream = new MemoryStream();
                        stream.CopyTo(memStream);
                        Files.Add(entry.FullName, memStream, FileType.ModelAsset);
                    }
                }
            }
        }
    }
}