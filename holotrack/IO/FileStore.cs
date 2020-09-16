using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using JsonFlatFileDataStore;
using osu.Framework.Extensions;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace holotrack.IO
{
    public class FileStore
    {
        public readonly Storage Storage;
        public readonly IResourceStore<byte[]> Store;
        protected readonly DataStore Data;
        protected readonly IDocumentCollection<FileMetadata> Context;

        public FileStore(Storage storage)
        {
            Storage = storage.GetStorageForDirectory(@"files");
            Store = new StorageBackedResourceStore(Storage);
            Data = new DataStore($"{storage.GetFullPath(string.Empty)}/files.json");
            Context = Data.GetCollection<FileMetadata>();
        }

        public FileMetadata AddBackground(string path) => Add(path, File.OpenRead(path), FileType.Background);

        public FileMetadata[] AddCubismModel(string path)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                if (!archive.Entries.Any(f => f.Name.Contains(".model3.json")))
                    throw new FileNotFoundException("The provided zip file is not a valid Live2D model.");

                var metas = new List<FileMetadata>();
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    using (var stream = entry.Open())
                    {
                        var memStream = new MemoryStream();
                        stream.CopyTo(memStream);
                        metas.Add(Add(entry.FullName, memStream, FileType.ModelAsset));
                    }
                }

                return metas.ToArray();
            }
        }

        public FileMetadata Add(string path, Stream data, FileType type)
        {
            var hash = data.ComputeSHA2Hash();
            var meta = new FileMetadata(Path.GetFileName(path), hash, type);

            if (!Storage.Exists(hash))
            {
                using (var stream = Storage.GetStream(hash + Path.GetExtension(path), FileAccess.Write))
                    data.CopyTo(stream);

                Context.InsertOne(meta);
            }

            data.Dispose();
            return meta;
        }

        public void Remove(string path)
        {
            var hash = Retrieve(path);
            if (!Storage.Exists(hash)) return;

            Storage.Delete(hash);
            Context.DeleteOne(file => file.Hash == hash);
        }

        public string Retrieve(string path)
        {
            var meta = Context.Find(file => file.Path == path).FirstOrDefault();

            if (meta != null)
                return meta.Hash + Path.GetExtension(meta.Path);
            else
                return string.Empty;
        }
    }
}