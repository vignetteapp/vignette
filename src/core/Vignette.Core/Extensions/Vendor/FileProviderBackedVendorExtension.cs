// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.IO;
using System.Text.Json;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Stride.Core.IO;

namespace Vignette.Core.Extensions.Vendor
{
    public abstract class FileProviderBackedVendorExtension : VendorExtension
    {
        private const string metadata = @"meta.json";
        private const string entry = @"extension.js";
        protected readonly IVirtualFileProvider Files;
        protected readonly string BasePath;

        protected FileProviderBackedVendorExtension(V8Runtime runtime, IVirtualFileProvider files, string basePath)
            : base(runtime, getMetadata(files))
        {
            Files = files;
            BasePath = basePath;
        }

        protected sealed override DocumentInfo GetDocumentInfo(out string code)
        {
            if (!Files.FileExists(entry))
                throw new FileNotFoundException(null, entry);

            using var stream = Files.OpenStream(entry, VirtualFileMode.Open, VirtualFileAccess.Read);
            using var reader = new StreamReader(stream);

            code = reader.ReadToEnd();

            return new DocumentInfo(new Uri(Path.Combine(BasePath, "extension.js")))
            {
                Category = ModuleCategory.Standard,
                SourceMapUri = new Uri(code[code.IndexOf("//# sourceMappingURL=")..].Replace("//# sourceMappingURL=", string.Empty))
            };
        }

        private static VendorExtensionMetadata getMetadata(IVirtualFileProvider files)
        {
            if (!files.FileExists(metadata))
                throw new FileNotFoundException(null, metadata);

            using var stream = files.OpenStream(metadata, VirtualFileMode.Open, VirtualFileAccess.Read);
            using var reader = new StreamReader(stream);
            return JsonSerializer.Deserialize<VendorExtensionMetadata>(reader.ReadToEnd(), new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
