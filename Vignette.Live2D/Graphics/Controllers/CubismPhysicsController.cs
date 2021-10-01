// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Allocation;
using osuTK;
using System;
using System.Linq;
using Vignette.Live2D.IO.Serialization;
using Vignette.Live2D.Physics;
using Vignette.Live2D.Utils;

namespace Vignette.Live2D.Graphics.Controllers
{
    /// <summary>
    /// A controller that manages the physics of a Live2D model.
    /// </summary>
    public class CubismPhysicsController : CubismController
    {
        public const float AIR_RESISTANCE = 5.0f;

        public const float MAXIMUM_WEIGHT = 100.0f;

        public const float MOVEMENT_THRESHOLD = 0.001f;

        public bool UseAngleCorrection { get; set; } = true;

        private CubismPhysicsRig physicsRig;

        [BackgroundDependencyLoader]
        private void load()
        {
            if (string.IsNullOrEmpty(Model.Settings.FileReferences.Physics))
                return;

            using var stream = Model.Resources.GetStream(Model.Settings.FileReferences.Physics);
            var setting = CubismUtils.ReadJsonSetting<CubismPhysicsSetting>(stream);

            physicsRig = new CubismPhysicsRig
            {
                Gravity = setting.Meta.EffectiveForces.Gravity,
                Wind = setting.Meta.EffectiveForces.Wind,
                SubRigs = new CubismPhysicsSubRig[setting.Meta.PhysicsSettingCount],
            };

            for (int i = 0; i < physicsRig.SubRigs.Length; ++i)
            {
                physicsRig.SubRigs[i] = new CubismPhysicsSubRig
                {
                    Gravity = physicsRig.Gravity,
                    Wind = physicsRig.Wind,
                    Input = readInputs(setting.PhysicsSettings[i].Input.ToArray()),
                    Output = readOutputs(setting.PhysicsSettings[i].Output.ToArray()),
                    Particles = readParticles(setting.PhysicsSettings[i].Vertices.ToArray()),
                    Normalization = readNormalization(setting.PhysicsSettings[i].Normalization),
                };
            }

            physicsRig.Initialize();
        }

        protected override void Update()
        {
            base.Update();
            physicsRig.Update(Clock.ElapsedFrameTime / 1000);
        }

        private CubismPhysicsInput[] readInputs(CubismPhysicsSetting.PhysicsSetting.InputSetting[] settings)
        {
            var data = new CubismPhysicsInput[settings.Length];

            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = new CubismPhysicsInput
                {
                    Source = Model.Parameters.FirstOrDefault(p => p.Name == settings[i].Source.Id),
                    SourceId = settings[i].Source.Id,
                    AngleScale = 0.0f,
                    ScaleOfTranslation = Vector2.Zero,
                    Weight = settings[i].Weight,
                    SourceComponent = Enum.Parse<CubismPhysicsSource>(settings[i].Type),
                    IsInverted = settings[i].Reflect,
                };
            }

            return data;
        }

        private CubismPhysicsOutput[] readOutputs(CubismPhysicsSetting.PhysicsSetting.OutputSetting[] settings)
        {
            var data = new CubismPhysicsOutput[settings.Length];

            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = new CubismPhysicsOutput
                {
                    Destination = Model.Parameters.FirstOrDefault(p => p.Name == settings[i].Destination.Id),
                    DestinationId = settings[i].Destination.Id,
                    ParticleIndex = settings[i].VertexIndex,
                    TranslationScale = Vector2.Zero,
                    AngleScale = settings[i].Scale,
                    Weight = settings[i].Weight,
                    SourceComponent = Enum.Parse<CubismPhysicsSource>(settings[i].Type),
                    IsInverted = settings[i].Reflect,
                    ValueBelowMinimum = 0.0f,
                    ValueExceededMaximum = 0.0f,
                    UseAngleCorrection = UseAngleCorrection,
                };
            }

            return data;
        }

        private CubismPhysicsParticle[] readParticles(CubismPhysicsSetting.PhysicsSetting.VertexSetting[] settings)
        {
            var data = new CubismPhysicsParticle[settings.Length];

            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = new CubismPhysicsParticle
                {
                    InitialPosition = settings[i].Position,
                    Mobility = settings[i].Mobility,
                    Delay = settings[i].Delay,
                    Acceleration = settings[i].Acceleration,
                    Radius = settings[i].Radius,
                    Position = Vector2.Zero,
                    LastPosition = Vector2.Zero,
                    LastGravity = Vector2.Zero,
                    Force = Vector2.Zero,
                    Velocity = Vector2.Zero,
                };
            }

            return data;
        }

        private CubismPhysicsNormalization readNormalization(CubismPhysicsSetting.PhysicsSetting.NormalizationSetting setting)
        {
            return new CubismPhysicsNormalization
            {
                Position = new CubismPhysicsNormalizationTuplet
                {
                    Maximum = setting.Position.Maximum,
                    Minimum = setting.Position.Minimum,
                    Default = setting.Position.Default,
                },
                Angle = new CubismPhysicsNormalizationTuplet
                {
                    Maximum = setting.Angle.Maximum,
                    Minimum = setting.Angle.Minimum,
                    Default = setting.Angle.Default,
                }
            };
        }
    }
}
