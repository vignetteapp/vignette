using System.Collections.Generic;
using System.IO;

namespace holotrack.IO.Imports
{
    public abstract class Importer
    {
        private List<string> extensions => new List<string>();
        public IReadOnlyList<string> Extensions => extensions;

        protected readonly FileStore Files;

        public Importer(FileStore files)
        {
            Files = files;
        }

        public void AddExtension(string ext)
        {
            if (!ext.StartsWith('.')) ext.Insert(0, ".");
            extensions.Add(ext);
        }

        public bool IsFileSupported(string file) => extensions.Contains(Path.GetExtension(file));

        public abstract void Import(string path);
    }
}