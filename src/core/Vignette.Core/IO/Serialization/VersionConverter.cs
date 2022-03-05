// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using Newtonsoft.Json;

namespace Vignette.Core.IO.Serialization
{
    public class VersionConverter : JsonConverter<Version>
    {
        public override Version ReadJson(JsonReader reader, Type objectType, Version existingValue, bool hasExistingValue, JsonSerializer serializer)
            => new Version(serializer.Deserialize<string>(reader));

        public override void WriteJson(JsonWriter writer, Version value, JsonSerializer serializer)
            => writer.WriteValue(value.ToString(3));
    }
}
