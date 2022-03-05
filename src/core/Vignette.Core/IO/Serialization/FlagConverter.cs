// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using Newtonsoft.Json;

namespace Vignette.Core.IO.Serialization
{
    public class FlagConverter<T> : JsonConverter<T>
        where T : struct, Enum
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] values = serializer.Deserialize<string[]>(reader);

            int val = 0;
            foreach (string value in values)
            {
                if (Enum.TryParse<T>(value, true, out var result))
                    val |= Convert.ToInt32(result);
            }

            return (T)Enum.ToObject(typeof(T), val);
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            writer.WriteStartArray();

            foreach (var flag in Enum.GetValues<T>())
            {
                if (value.HasFlag(flag))
                    writer.WriteValue(Enum.GetName(flag));
            }

            writer.WriteEndArray();
        }
    }
}
