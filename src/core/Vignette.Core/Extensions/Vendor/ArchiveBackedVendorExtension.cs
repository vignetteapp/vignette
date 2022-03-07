// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Stride.Core.IO;
using Vignette.Core.IO;

namespace Vignette.Core.Extensions.Vendor
{
    public class ArchiveBackedVendorExtension : FileProviderBackedVendorExtension
    {
        public ArchiveBackedVendorExtension(string archivePath)
            : base(new ArchiveFileProvider($"{MountPath}/{Path.GetFileNameWithoutExtension(archivePath)}", archivePath), string.Empty)
        {
        }

        protected sealed override void Prepare(V8ScriptEngine engine)
        {
            engine.DocumentSettings.Loader = new ArchiveBackedDocumentLoader(this);
        }

        private class ArchiveBackedDocumentLoader : DocumentLoader
        {
            private readonly ArchiveBackedVendorExtension extension;

            public ArchiveBackedDocumentLoader(ArchiveBackedVendorExtension extension)
            {
                this.extension = extension;
            }

            public override Task<Document> LoadDocumentAsync(DocumentSettings settings, DocumentInfo? sourceInfo, string specifier, DocumentCategory category, DocumentContextCallback contextCallback)
            {
                if (!extension.Files.FileExists(specifier))
                    throw new FileNotFoundException(null, specifier);

                var stream = extension.Files.OpenStream(specifier, VirtualFileMode.Open, VirtualFileAccess.Read);
                var info = new DocumentInfo(new Uri(specifier)) { Category = ModuleCategory.Standard, ContextCallback = contextCallback };

                return Task.FromResult<Document>(new StreamBackedDocument(info, stream));
            }
        }

        private class StreamBackedDocument : Document
        {
            public override DocumentInfo Info { get; }
            public override Stream Contents { get; }

            public StreamBackedDocument(DocumentInfo info, Stream contents)
            {
                Info = info;
                Contents = contents;
            }
        }
    }
}
