// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;
using System.IO;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    public class WrappedStorage : Storage
    {
        protected Storage UnderlyingStorage { get; private set; }

        public WrappedStorage(Storage storage)
            : base(string.Empty)
        {
            UnderlyingStorage = storage;
        }

        public override void Delete(string path)
            => UnderlyingStorage.Delete(path);

        public override void DeleteDatabase(string name)
            => UnderlyingStorage.DeleteDatabase(name);

        public override void DeleteDirectory(string path)
            => UnderlyingStorage.DeleteDirectory(path);

        public override bool Exists(string path)
            => UnderlyingStorage.Exists(path);

        public override bool ExistsDirectory(string path)
            => UnderlyingStorage.ExistsDirectory(path);

        public override string GetDatabaseConnectionString(string name)
            => UnderlyingStorage.GetDatabaseConnectionString(name);

        public override IEnumerable<string> GetDirectories(string path)
            => UnderlyingStorage.GetDirectories(path);

        public override IEnumerable<string> GetFiles(string path, string pattern = "*")
            => UnderlyingStorage.GetFiles(path, pattern);

        public override string GetFullPath(string path, bool createIfNotExisting = false)
            => UnderlyingStorage.GetFullPath(path, createIfNotExisting);

        public override Stream GetStream(string path, FileAccess access = FileAccess.Read, FileMode mode = FileMode.OpenOrCreate)
            => UnderlyingStorage.GetStream(path, access, mode);

        public override void OpenPathInNativeExplorer(string path)
            => UnderlyingStorage.OpenPathInNativeExplorer(path);
    }
}
