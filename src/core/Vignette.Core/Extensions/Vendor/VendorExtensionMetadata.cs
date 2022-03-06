// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vignette.Core.IO.Serialization;

namespace Vignette.Core.Extensions.Vendor
{
    public class VendorExtensionMetadata : IExtension
    {
        public string Name { get; set; } = @"Extension";
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Identifier { get; set; } = @"extension";

        [JsonConverter(typeof(VersionConverter))]
        public Version Version { get; set; } = new Version("0.0.0");

        [JsonConverter(typeof(FlagConverter<ExtensionIntents>))]
        public ExtensionIntents Intents { get; set; } = ExtensionIntents.None;
        public IReadOnlyList<VendorExtensionDependency> Dependencies { get; set; } = Array.Empty<VendorExtensionDependency>();

        public bool Equals(IExtension other) => false;
    }
}
