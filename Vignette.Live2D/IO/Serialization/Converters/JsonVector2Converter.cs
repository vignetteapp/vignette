// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using Newtonsoft.Json;
using osuTK;
using System;

namespace Vignette.Live2D.IO.Serialization.Converters
{
    internal class JsonVector2Converter : JsonConverter<Vector2>
    {
        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var vector = new Vector2();

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                    break;

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var name = reader.Value?.ToString();
                    int? val = reader.ReadAsInt32();

                    if (val == null)
                        continue;

                    switch (name)
                    {
                        case "X":
                            vector.X = val.Value;
                            break;

                        case "Y":
                            vector.Y = val.Value;
                            break;
                    }
                }
            }

            return vector;
        }

        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
            => serializer.Serialize(writer, new { value.X, value.Y });
    }
}
