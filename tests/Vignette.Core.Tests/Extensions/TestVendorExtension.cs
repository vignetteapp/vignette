// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using Microsoft.ClearScript;
using Vignette.Core.Extensions.Vendor;

namespace Vignette.Core.Tests.Extensions
{
    public class TestVendorExtension : VendorExtension
    {
        public string Code { get; set; }
        public new IReadOnlyDictionary<string, object> Channels => base.Channels;

        public TestVendorExtension()
            : base(new VendorExtensionMetadata())
        {
        }

        protected override string GetDocumentContent(DocumentInfo documentInfo) => Code;
    }
}
