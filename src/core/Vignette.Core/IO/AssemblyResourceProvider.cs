// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Stride.Core.IO;

namespace Vignette.Core.IO
{
    public class AssemblyResourceProvider : VirtualFileProviderBase
    {
        private const string mount_point = "/assembly";
        private const string real_path = "/Resources";
        private readonly string prefix;
        private readonly Assembly assembly;
        private readonly IEnumerable<string> entries;

        public AssemblyResourceProvider(Assembly assembly)
            : base(mount_point)
        {
            this.assembly = assembly;
            prefix = assembly.GetName().Name;
            entries = assembly.GetManifestResourceNames().Select(p =>
            {
                p = p.Replace(prefix, string.Empty);

                char[] chars = p.ToCharArray();

                for (int i = 0; i < p.LastIndexOf('.'); i++)
                {
                    if (chars[i] == '.')
                        chars[i] = '/';
                }

                return new string(chars).Replace(real_path, mount_point);
            });
        }

        public Stream OpenStream(string url, StreamFlags streamFlags = StreamFlags.None)
            => OpenStream(url, VirtualFileMode.Open, VirtualFileAccess.Read, VirtualFileShare.Read, streamFlags);

        public override Stream OpenStream(string url, VirtualFileMode mode, VirtualFileAccess access, VirtualFileShare share = VirtualFileShare.Read, StreamFlags streamFlags = StreamFlags.None)
        {
            if (mode != VirtualFileMode.Open || access != VirtualFileAccess.Read || share != VirtualFileShare.Read)
                throw new InvalidOperationException("Read-only file provider.");

            if (!FileExists(url))
                throw new FileNotFoundException("File not found in assembly.");

            var stream = assembly.GetManifestResourceStream($"{prefix}{real_path.Replace('/', '.')}.{url.Replace('/', '.')}");

            if (streamFlags == StreamFlags.Seekable && (!stream?.CanSeek ?? false))
            {
                byte[] buffer = new byte[stream.Length - stream.Position];
                stream.Read(buffer, 0, buffer.Length);
                return new MemoryStream(buffer);
            }

            return stream;
        }

        public override bool DirectoryExists(string url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            if (!url.EndsWith("/"))
                url += "/";

            return entries.Any(x => x.StartsWith(url));
        }

        public override string[] ListFiles(string url, string searchPattern, VirtualSearchOption searchOption)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            // https://github.com/stride3d/stride/blob/master/sources/core/Stride.Core.Serialization/IO/DatabaseFileProvider.cs/#L95-L97
            searchPattern = Regex.Escape(searchPattern).Replace(@"\*", "[^/]*").Replace(@"\?", "[^/]");
            string recursivePattern = searchOption == VirtualSearchOption.AllDirectories ? "(.*/)*" : "/?";
            var regex = new Regex("^" + url + recursivePattern + searchPattern + "$");

            return entries.Where(s => regex.IsMatch(s)).ToArray();
        }

        public override bool FileExists(string url)
            => entries.Contains(url);

        public override long FileSize(string url)
        {
            using var stream = OpenStream(url);
            return stream.Length;
        }
    }
}
