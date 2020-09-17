using System.IO;
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
        public readonly IDocumentCollection<FileMetadata> Context;
        protected readonly DataStore Data;

        public FileStore(Storage storage)
        {
            Storage = storage.GetStorageForDirectory(@"files");
            Store = new StorageBackedResourceStore(Storage);
            Data = new DataStore($"{storage.GetFullPath(string.Empty)}/files.json");
            Context = Data.GetCollection<FileMetadata>();
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