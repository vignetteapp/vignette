// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Text.Json.Serialization;
using Vignette.Core.IO.Serialization;

namespace Vignette.Core.Extensions.Vendor
{
    public struct VendorExtensionDependency : IEquatable<VendorExtensionDependency>
    {
        [JsonPropertyName("id")]
        public string Identifier { get; set; }

        [JsonConverter(typeof(VersionConverter))]
        public Version Version { get; set; }

        [JsonPropertyName("required")]
        public bool Required { get; set; }

        public bool Equals(VendorExtensionDependency other)
            => other.Identifier == Identifier && other.Version == Version;

        public override bool Equals(object obj)
        {
            if (obj is VendorExtensionDependency dep)
                return Equals(dep);

            return false;
        }

        public override int GetHashCode()
            => HashCode.Combine(Identifier, Version);

        public override string ToString()
            => $@"{Identifier} ({Version})";

        public static bool operator ==(VendorExtensionDependency left, VendorExtensionDependency right)
            => left.Equals(right);

        public static bool operator !=(VendorExtensionDependency left, VendorExtensionDependency right)
            => !(left == right);
    }
}
