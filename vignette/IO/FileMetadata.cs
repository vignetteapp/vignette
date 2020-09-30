namespace vignette.IO
{
    public class FileMetadata
    {
        public readonly FileType Type;
        public readonly string Path;
        public readonly string Hash;

        public FileMetadata(string path, string hash, FileType type)
        {
            Path = path;
            Hash = hash;
            Type = type;
        }
    }
}