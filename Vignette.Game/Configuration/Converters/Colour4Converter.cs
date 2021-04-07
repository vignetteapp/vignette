// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using Newtonsoft.Json;
using osu.Framework.Graphics;

namespace Vignette.Game.Configuration.Converters
{
    public class Colour4Converter : JsonConverter<Colour4>
    {
        public override Colour4 ReadJson(JsonReader reader, Type objectType, Colour4 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Colour4.FromHex((string)reader.Value);
        }
        public override void WriteJson(JsonWriter writer, Colour4 value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToHex());
        }
    }
}
