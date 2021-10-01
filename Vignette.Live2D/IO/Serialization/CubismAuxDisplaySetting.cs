// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Collections.Generic;

namespace Vignette.Live2D.IO.Serialization
{
    [Serializable]
    public class CubismAuxDisplaySetting : ICubismJsonSetting
    {
        public int Version { get; set; }

        public List<DisplayParameters> Parameters { get; set; }

        public List<DisplayParameters> ParameterGroups { get; set; }

        public List<DisplayParameters> Parts { get; set; }

        public class DisplayParameters
        {
            public string Id { get; set; }

            public string GroupId { get; set; }

            public string Name { get; set; }
        }
    }
}
