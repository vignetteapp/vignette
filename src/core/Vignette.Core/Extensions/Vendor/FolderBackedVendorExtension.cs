// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.IO;
using Stride.Core.IO;

namespace Vignette.Core.Extensions.Vendor
{
    public class FolderBackedVendorExtension : FileProviderBackedVendorExtension
    {
        public FolderBackedVendorExtension(string path)
            : base(VirtualFileSystem.MountFileSystem($"{MountPath}/{Path.GetFileNameWithoutExtension(path)}", path), path)
        {
        }
    }
}
