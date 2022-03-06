// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.IO;
using Microsoft.ClearScript.V8;
using Stride.Core.IO;

namespace Vignette.Core.Extensions.Vendor
{
    public class FolderBackedVendorExtension : FileProviderBackedVendorExtension
    {
        public FolderBackedVendorExtension(V8Runtime runtime, string path)
            : base(runtime, VirtualFileSystem.MountFileSystem($"/extension/{Path.GetFileNameWithoutExtension(path)}", path), path)
        {
        }
    }
}
