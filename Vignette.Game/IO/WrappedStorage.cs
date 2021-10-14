// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

// Copyright (c) 2021 ppy Pty Ltd <contact@ppy.sh>.

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    public class WrappedStorage : Storage
    {
        protected Storage UnderlyingStorage { get; private set; }

        private readonly string subPath;

        public WrappedStorage(Storage storage, string subPath = null)
            : base(string.Empty)
        {
            UnderlyingStorage = storage;
            this.subPath = subPath;
        }

        protected virtual string MutatePath(string path)
        {
            if (path == null)
                return null;

            return !string.IsNullOrEmpty(subPath) ? Path.Combine(subPath, path) : path;
        }

        public IEnumerable<string> ToLocalRelative(IEnumerable<string> paths)
        {
            string localRoot = GetFullPath(string.Empty);

            foreach (var path in paths)
                yield return Path.GetRelativePath(localRoot, UnderlyingStorage.GetFullPath(path));
        }

        public override string GetFullPath(string path, bool createIfNotExisting = false)
            => UnderlyingStorage.GetFullPath(MutatePath(path), createIfNotExisting);

        public override bool Exists(string path)
            => UnderlyingStorage.Exists(MutatePath(path));

        public override bool ExistsDirectory(string path)
            => UnderlyingStorage.ExistsDirectory(MutatePath(path));

        public override void DeleteDirectory(string path)
            => UnderlyingStorage.DeleteDirectory(MutatePath(path));

        public override void Delete(string path)
            => UnderlyingStorage.Delete(MutatePath(path));

        public override IEnumerable<string> GetDirectories(string path)
            => ToLocalRelative(UnderlyingStorage.GetDirectories(MutatePath(path)));

        public override IEnumerable<string> GetFiles(string path, string pattern = "*")
            => ToLocalRelative(UnderlyingStorage.GetFiles(MutatePath(path), pattern));

        public override Stream GetStream(string path, FileAccess access = FileAccess.Read, FileMode mode = FileMode.OpenOrCreate)
            => UnderlyingStorage.GetStream(MutatePath(path), access, mode);

        public override string GetDatabaseConnectionString(string name)
            => UnderlyingStorage.GetDatabaseConnectionString(MutatePath(name));

        public override void DeleteDatabase(string name)
            => UnderlyingStorage.DeleteDatabase(MutatePath(name));

        public override void OpenPathInNativeExplorer(string path)
            => UnderlyingStorage.OpenPathInNativeExplorer(MutatePath(path));

        public override Storage GetStorageForDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Must be non-null and not empty string", nameof(path));

            if (!path.EndsWith(Path.DirectorySeparatorChar))
                path += Path.DirectorySeparatorChar;

            // create non-existing path.
            GetFullPath(path, true);

            return new WrappedStorage(this, path);
        }
    }
}
