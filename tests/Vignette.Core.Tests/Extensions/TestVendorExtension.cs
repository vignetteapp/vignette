// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Vignette.Core.Extensions.Vendor;

namespace Vignette.Core.Tests.Extensions
{
    public class TestVendorExtension : VendorExtension
    {
        public string Code { get; set; }
        public new IReadOnlyDictionary<string, object> Channels => base.Channels;

        public TestVendorExtension(V8Runtime runtime)
            : base(runtime, new VendorExtensionMetadata())
        {
        }

        protected override DocumentInfo GetDocumentInfo(out string code)
        {
            code = Code;
            return new DocumentInfo(Name) { Category = ModuleCategory.Standard };
        }
    }
}
