// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Collections.Generic;
using osuTK;

namespace Vignette.Live2D.IO.Serialization
{
    [Serializable]
    public class CubismPhysicsSetting : ICubismJsonSetting
    {
        public int Version { get; set; }

        public Metadata Meta { get; set; }

        public List<PhysicsSetting> PhysicsSettings { get; set; }

        public class Metadata
        {
            public int PhysicsSettingCount { get; set; }

            public int TotalInputCount { get; set; }

            public int TotalOutputCount { get; set; }

            public int VertexCount { get; set; }

            public EffectiveForce EffectiveForces { get; set; }

            public List<PhysicsDictionaryItem> PhysicsDictionary { get; set; }

            public class EffectiveForce
            {
                public Vector2 Gravity { get; set; }

                public Vector2 Wind { get; set; }
            }

            public class PhysicsDictionaryItem
            {
                public string Id { get; set; }

                public string Name { get; set; }
            }
        }

        public class PhysicsSetting
        {
            public string Id { get; set; }

            public List<InputSetting> Input { get; set; }

            public List<OutputSetting> Output { get; set; }

            public List<VertexSetting> Vertices { get; set; }

            public NormalizationSetting Normalization { get; set; }

            public class InputSetting
            {
                public SourceDestination Source { get; set; }

                public float Weight { get; set; }

                public string Type { get; set; }

                public bool Reflect { get; set; }
            }

            public class OutputSetting
            {
                public SourceDestination Destination { get; set; }

                public int VertexIndex { get; set; }

                public float Scale { get; set; }

                public float Weight { get; set; }

                public string Type { get; set; }

                public bool Reflect { get; set; }
            }

            public class VertexSetting
            {
                public Vector2 Position { get; set; }

                public float Mobility { get; set; }

                public float Delay { get; set; }

                public float Acceleration { get; set; }

                public float Radius { get; set; }
            }

            public class NormalizationSetting
            {
                public MinMaxSetting Position { get; set; }

                public MinMaxSetting Angle { get; set; }
            }

            public class SourceDestination
            {
                public string Target { get; set; }

                public string Id { get; set; }
            }

            public class MinMaxSetting
            {
                public float Minimum { get; set; }

                public float Default { get; set; }

                public float Maximum { get; set; }
            }
        }
    }
}
