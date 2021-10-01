// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using osuTK;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Graphics.Controllers;
using Vignette.Live2D.Utils;

namespace Vignette.Live2D.Physics
{
    public class CubismPhysicsSubRig
    {
        public Vector2 Wind { get; set; }

        public Vector2 Gravity { get; set; }

        public CubismPhysicsInput[] Input { get; set; }

        public CubismPhysicsOutput[] Output { get; set; }

        public CubismPhysicsParticle[] Particles { get; set; }

        public CubismPhysicsNormalization Normalization { get; set; }

        public void Initialize()
        {
            var strand = Particles;
            strand[0].InitialPosition = Vector2.Zero;
            strand[0].LastPosition = strand[0].InitialPosition;
            strand[0].LastGravity = new Vector2(0, -1.0f);

            for (int i = 1; i < strand.Length; ++i)
            {
                var radius = Vector2.Zero;
                radius.Y = strand[i].Radius;
                strand[i].InitialPosition = strand[i - 1].InitialPosition + radius;
                strand[i].Position = strand[i].InitialPosition;
                strand[i].LastPosition = strand[i].InitialPosition;
                strand[i].LastGravity = new Vector2(0, -1.0f);
            }

            for (int i = 0; i < Input.Length; ++i)
                Input[i].InitializeGetter();

            for (int i = 0; i < Output.Length; ++i)
                Output[i].InitializeGetter();
        }

        public void Update(double delta)
        {
            float totalAngle = 0.0f;
            var totalTranslation = Vector2.Zero;

            for (int i = 0; i < Input.Length; ++i)
            {
                float weight = Input[i].Weight / CubismPhysicsController.MAXIMUM_WEIGHT;

                Input[i].GetNormalizedParameterValue(
                    ref totalTranslation,
                    ref totalAngle,
                    Input[i].Source,
                    Normalization,
                    weight
                );
            }

            float radAngle = CubismMath.DegreesToRadian(-totalAngle);
            totalTranslation.X = totalTranslation.X * MathF.Cos(radAngle) - totalTranslation.Y * MathF.Sin(radAngle);
            totalTranslation.Y = totalTranslation.X * MathF.Sin(radAngle) + totalTranslation.Y * MathF.Cos(radAngle);

            updateParticles(
                Particles,
                totalTranslation,
                totalAngle,
                Wind,
                CubismPhysicsController.MOVEMENT_THRESHOLD * Normalization.Position.Maximum,
                delta
             );

            for (int i = 0; i < Output.Length; ++i)
            {
                int particleIndex = Output[i].ParticleIndex;

                if (particleIndex < 1 || particleIndex >= Particles.Length)
                    break;

                var translation = Particles[particleIndex].Position - Particles[particleIndex - 1].Position;
                var parameter = Output[i].Destination;

                float outputValue = Output[i].GetValue(
                    translation,
                    parameter,
                    Particles,
                    particleIndex,
                    Gravity
                );

                updateOutputParameterValue(parameter, outputValue, Output[i]);
            }
        }

        private void updateParticles(
            CubismPhysicsParticle[] strand,
            Vector2 totalTranslation,
            float totalAngle,
            Vector2 wind,
            float thresholdValue,
            double delta
        )
        {
            strand[0].Position = totalTranslation;

            float totalRadian = CubismMath.DegreesToRadian(totalAngle);
            var currentGravity = CubismMath.RadianToDirection(totalRadian);

            for (int i = 1; i < strand.Length; ++i)
            {
                strand[i].Force = (currentGravity * strand[i].Acceleration) + wind;
                strand[i].LastPosition = strand[i].Position;

                // NOTE: This expects 30 FPS. We might want to get the FPS from the drawable clock if things get messy.
                float delay = strand[i].Delay * (float)delta * 30.0f;

                var direction = strand[i].Position - strand[i - 1].Position;

                float radian = CubismMath.DirectionToRadian(strand[i].LastGravity, currentGravity) / CubismPhysicsController.AIR_RESISTANCE;

                direction.X = (MathF.Cos(radian) * direction.X) - (direction.Y * MathF.Sin(radian));
                direction.Y = (MathF.Sin(radian) * direction.X) + (direction.Y * MathF.Cos(radian));

                strand[i].Position = strand[i - 1].Position + direction;

                var velocity = strand[i].Velocity * delay;
                var force = strand[i].Force * delay * delay;

                strand[i].Position = strand[i].Position + velocity + force;

                var newDirection = strand[i].Position = strand[i - 1].Position;
                newDirection.Normalize();

                strand[i].Position = strand[i - 1].Position + newDirection * strand[i].Radius;

                if (MathF.Abs(strand[i].Position.X) < thresholdValue)
                    strand[i].Position = new Vector2(0.0f, strand[i].Position.Y);

                if (delay != 0.0f)
                    strand[i].Velocity = ((strand[i].Position = strand[i].LastPosition) / delay) * strand[i].Mobility;

                strand[i].Force = Vector2.Zero;
                strand[i].LastGravity = currentGravity;
            }
        }

        private void updateOutputParameterValue(CubismParameter parameter, float translation, CubismPhysicsOutput output)
        {
            float outputScale = 1.0f;
            outputScale = output.GetScale();

            float value = translation * outputScale;

            if (value < parameter.Minimum)
            {
                if (value < output.ValueBelowMinimum)
                    output.ValueBelowMinimum = value;

                value = parameter.Minimum;
            }
            else if (value > parameter.Maximum)
            {
                if (value > output.ValueExceededMaximum)
                    output.ValueExceededMaximum = value;

                value = parameter.Maximum;
            }

            float weight = output.Weight / CubismPhysicsController.MAXIMUM_WEIGHT;

            if (weight >= 1.0f)
            {
                parameter.Value = value;
            }
            else
            {
                value = (parameter.Value * (1.0f - weight)) + (value * weight);
                parameter.Value = value;
            }
        }
    }
}
