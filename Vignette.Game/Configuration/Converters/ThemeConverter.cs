// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Configuration.Converters
{
    public class ThemeConverter : JsonConverter<Theme>
    {
        public override Theme ReadJson(JsonReader reader, Type objectType, Theme existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var theme = new Theme();

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                    break;

                if (reader.TokenType != JsonToken.PropertyName)
                    continue;

                var key = reader.Value.ToString();

                if (!slot_map.ContainsKey(key))
                    continue;

                var slot = slot_map[key];

                if (Theme.CONSTANT_SLOTS.Contains(slot))
                    continue;

                reader.Read();

                if (reader.TokenType != JsonToken.String)
                    continue;

                if (!Colour4.TryParseHex(reader.Value.ToString(), out var colour))
                    continue;

                theme.Set(slot, colour);
            }

            return theme;
        }

        public override void WriteJson(JsonWriter writer, Theme value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            foreach (var slot in Enum.GetValues<ThemeSlot>())
            {
                if (Theme.CONSTANT_SLOTS.Contains(slot))
                    continue;

                writer.WritePropertyName(slot.GetDescription());
                writer.WriteValue(value.Get(slot).ToHex());
            }

            writer.WriteEndObject();
        }

        private static readonly Dictionary<string, ThemeSlot> slot_map = Enum.GetValues<ThemeSlot>().ToDictionary(s => s.GetDescription(), s => s);
    }
}
