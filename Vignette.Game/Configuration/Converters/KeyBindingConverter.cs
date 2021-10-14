// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Input.Bindings;

namespace Vignette.Game.Configuration.Converters
{
    public abstract class KeybindingConverter<T> : JsonConverter<List<IKeyBinding>>
    {
        public override List<IKeyBinding> ReadJson(JsonReader reader, Type objectType, List<IKeyBinding> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var keybindings = new List<IKeyBinding>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                    break;

                if (reader.TokenType != JsonToken.PropertyName)
                    continue;

                if (!TryGetAction(reader.Value.ToString(), out var action))
                    continue;

                reader.Read();

                if (reader.TokenType != JsonToken.String)
                    continue;

                var keys = reader.Value.ToString()
                        .Split('+', StringSplitOptions.RemoveEmptyEntries)
                        .Select(str => Enum.TryParse<InputKey>(str, out var inputKey) ? inputKey : InputKey.None)
                        .Where(key => key != InputKey.None);

                keybindings.Add(new KeyBinding(keys.ToArray(), action));
            }

            if (!hasExistingValue)
                return keybindings;

            return keybindings.Union(existingValue, new KeyBindEqualityComparer()).ToList();
        }

        public override void WriteJson(JsonWriter writer, List<IKeyBinding> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            foreach (var keybind in value)
            {
                writer.WritePropertyName(StringifyAction(keybind));
                writer.WriteValue(string.Join('+', keybind.KeyCombination.Keys.Select(k => k.ToString())));
            }

            writer.WriteEndObject();
        }

        protected abstract bool TryGetAction(string name, out T value);

        protected abstract string StringifyAction(IKeyBinding keybind);

        private class KeyBindEqualityComparer : IEqualityComparer<IKeyBinding>
        {
            public bool Equals(IKeyBinding x, IKeyBinding y) => x.Action.Equals(y.Action);

            public int GetHashCode([DisallowNull] IKeyBinding obj) => obj.Action.GetHashCode();
        }
    }
}
