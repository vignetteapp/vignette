using System.Collections.Generic;
using System.IO;
using osu.Framework.Bindables;

namespace holotrack.IO.Imports
{
    public abstract class Importer
    {
        private readonly List<string> extensions = new List<string>();
        public readonly BindableList<FileMetadata> Imported =  new BindableList<FileMetadata>();
        public IReadOnlyList<string> Extensions => extensions;

        protected readonly FileStore Files;

        public Importer(FileStore files)
        {
            Files = files;
            Imported.AddRange(Populate());
        }

        public void AddExtension(string ext)
        {
            if (!ext.StartsWith('.')) ext.Insert(0, ".");
                extensions.Add(ext);
        }

        public void Add(string path) => Imported.AddRange(Import(path));
        public bool IsFileSupported(string file) => extensions.Contains(Path.GetExtension(file));
    
        protected virtual IEnumerable<FileMetadata> Populate() => Files.Context.AsQueryable();
        protected abstract IEnumerable<FileMetadata> Import(string path);
    }
}