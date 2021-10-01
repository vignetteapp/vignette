// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;

namespace Vignette.Live2D.IO.Serialization
{
    [Serializable]
    public class CubismUserdataSetting : ICubismJsonSetting
    {
        public int Version { get; set; }

        public Metadata Meta { get; set; }

        public Userdata UserData { get; set; }

        public class Metadata
        {
            public int UserDataCount { get; set; }

            public int TotalUserDataSize { get; set; }
        }

        public class Userdata
        {
            public string Target { get; set; }

            public string Id { get; set; }

            public string Value { get; set; }
        }
    }
}
