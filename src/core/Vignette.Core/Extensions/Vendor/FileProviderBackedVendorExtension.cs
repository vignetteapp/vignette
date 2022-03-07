// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.IO;
using System.Text.Json;
using Microsoft.ClearScript;
using Stride.Core.IO;

namespace Vignette.Core.Extensions.Vendor
{
    public abstract class FileProviderBackedVendorExtension : VendorExtension
    {
        private const string metadata = @"meta.json";
        private const string entry = @"extension.js";
        public const string MountPath = @"/extensions";
        protected readonly IVirtualFileProvider Files;
        protected readonly IVirtualFileProvider Data;
        protected readonly string BasePath;
        public override Uri DocumentUri => new Uri(Path.Combine(BasePath, "extension.js"));

        protected FileProviderBackedVendorExtension(IVirtualFileProvider files, string basePath)
            : base(getMetadata(files))
        {
            Files = files;
            BasePath = basePath;

            if (Intents.HasFlag(ExtensionIntents.Files))
                Data = VirtualFileSystem.MountFileSystem($@"{MountPath}/data/{Identifier}", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "extensions", Identifier));
        }

        protected sealed override string GetDocumentContent(DocumentInfo documentInfo)
        {
            if (!Files.FileExists(entry))
                throw new FileNotFoundException(null, entry);

            using var stream = Files.OpenStream(entry, VirtualFileMode.Open, VirtualFileAccess.Read);
            using var reader = new StreamReader(stream);

            string code = reader.ReadToEnd();
            documentInfo.SourceMapUri = new Uri(code[code.IndexOf("//# sourceMappingURL=")..].Replace("//# sourceMappingURL=", string.Empty));

            return code;
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
