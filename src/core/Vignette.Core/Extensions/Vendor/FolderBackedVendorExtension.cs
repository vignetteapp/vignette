// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.IO;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Stride.Core.IO;

namespace Vignette.Core.Extensions.Vendor
{
    public class FolderBackedVendorExtension : VendorExtension
    {
        private readonly string path;

        public FolderBackedVendorExtension(V8Runtime runtime, string path)
            : base(runtime, VirtualFileSystem.MountFileSystem($"/extension/{Path.GetFileNameWithoutExtension(path)}", path))
        {
            this.path = path;
        }

        protected sealed override DocumentInfo CreateDocumentInfo(string code) => new DocumentInfo(new Uri(Path.Combine(path, "extension.js")))
        {
            Category = ModuleCategory.Standard,
            SourceMapUri = new Uri(code[code.IndexOf("//# sourceMappingURL=")..].Replace("//# sourceMappingURL=", string.Empty))
        };
    }
}
