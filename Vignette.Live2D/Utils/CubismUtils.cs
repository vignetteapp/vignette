// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System.IO;
using Newtonsoft.Json;
using Vignette.Live2D.IO.Serialization;
using Vignette.Live2D.IO.Serialization.Converters;

namespace Vignette.Live2D.Utils
{
    public static class CubismUtils
    {
        public static T ReadJsonSetting<T>(Stream stream) where T : ICubismJsonSetting
        {
            using var reader = new StreamReader(stream);
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd(), new JsonVector2Converter());
        }
    }
}
