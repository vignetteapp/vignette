// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Stride.Core.IO;

namespace Vignette.Core.IO
{
    public class ArchiveFileProvider : VirtualFileProviderBase, IDisposable
    {
        private readonly ZipArchive archive;
        private readonly Dictionary<string, ZipArchiveEntry> entries = new Dictionary<string, ZipArchiveEntry>();

        public ArchiveFileProvider(string rootPath, string archivePath)
            : base(rootPath)
        {
            archive = ZipFile.OpenRead(archivePath);
            entries = archive.Entries.ToDictionary(k => k.FullName, v => v);
        }

        public override Stream OpenStream(string url, VirtualFileMode mode, VirtualFileAccess access, VirtualFileShare share = VirtualFileShare.Read, StreamFlags streamFlags = StreamFlags.None)
        {
            if (!entries.TryGetValue(url, out var entry))
                throw new FileNotFoundException();

            if (mode != VirtualFileMode.Open || access != VirtualFileAccess.Read)
                throw new UnauthorizedAccessException(@"File system is read-only.");

            lock (archive)
            {
                var memory = new MemoryStream();

                using (var stream = entry.Open())
                    stream.CopyTo(memory);

                memory.Position = 0;
                return memory;
            }
        }

        public override bool DirectoryExists(string url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            if (!url.EndsWith('/'))
                url += '/';

            return entries.Any(x => x.Key.StartsWith(url));
        }

        public override bool FileExists(string url)
            => entries.ContainsKey(url);

        public override long FileSize(string url)
        {
            if (!entries.TryGetValue(url, out var entry))
                throw new FileNotFoundException();

            return entry.Length;
        }

        public new void Dispose()
        {
            base.Dispose();
            archive.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
