// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Platform;

namespace Vignette.Game.IO
{
    public class RecursiveNativeStorage : NativeStorage
    {
        public RecursiveNativeStorage(string path, GameHost host)
            : base(path, host)
        {
        }

        public override IEnumerable<string> GetDirectories(string path) => getRelativePaths(Directory.GetDirectories(GetFullPath(path), "*", SearchOption.AllDirectories));

        public override IEnumerable<string> GetFiles(string path, string pattern = "*") => getRelativePaths(Directory.GetFiles(GetFullPath(path), pattern, SearchOption.AllDirectories));

        private IEnumerable<string> getRelativePaths(IEnumerable<string> paths)
        {
            string basePath = Path.GetFullPath(GetFullPath(string.Empty));
            return paths.Select(Path.GetFullPath).Select(path =>
            {
                if (!path.StartsWith(basePath, StringComparison.Ordinal))
                    throw new ArgumentException($"\"{path}\" does not start with \"{basePath}\" and is probably malformed");

                return path.AsSpan(basePath.Length).TrimStart(Path.DirectorySeparatorChar).ToString();
            });
        }
    }
}
