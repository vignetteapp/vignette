// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using Microsoft.ClearScript;

namespace Vignette.Core.Extensions.Vendor.Modules
{
    public abstract class VendorExtensionModule
    {
        [ScriptMember(ScriptAccess.None)]
        public abstract string Name { get; }
        protected readonly VendorExtension Extension;

        public VendorExtensionModule(VendorExtension extension)
        {
            Extension = extension;
        }

        [ScriptMember(ScriptAccess.None)]
        public IDictionary<string, object> ToDocumentContext(DocumentInfo _)
            => new Dictionary<string, object> { { Name, this } };
    }
}
