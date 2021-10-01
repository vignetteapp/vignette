// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osuTK;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Utils;

namespace Vignette.Live2D.Physics
{
    public class CubismPhysicsOutput
    {
        public string DestinationId { get; set; }

        public int ParticleIndex { get; set; }

        public Vector2 TranslationScale { get; set; }

        public float AngleScale { get; set; }

        public float Weight { get; set; }

        public CubismPhysicsSource SourceComponent { get; set; }

        public bool IsInverted { get; set; }

        public float ValueBelowMinimum { get; set; }

        public float ValueExceededMaximum { get; set; }

        public CubismParameter Destination { get; set; }

        public ValueGetter GetValue { get; set; }

        public ScaleGetter GetScale { get; set; }

        public bool UseAngleCorrection { get; set; }

        public delegate float ValueGetter(
            Vector2 translation,
            CubismParameter parameter,
            CubismPhysicsParticle[] particles,
            int particleIndex,
            Vector2 gravity
        );

        public delegate float ScaleGetter();

        public void InitializeGetter()
        {
            switch (SourceComponent)
            {
                case CubismPhysicsSource.X:
                    {
                        GetScale = getOutputScaleTranslationX;
                        GetValue = getOutputTranslationX;
                        break;
                    }

                case CubismPhysicsSource.Y:
                    {
                        GetScale = getOutputScaleTranslationY;
                        GetValue = getOutputTranslationY;
                        break;
                    }

                case CubismPhysicsSource.Angle:
                    {
                        GetScale = getOutputScaleAngle;
                        GetValue = getOutputAngle;
                        break;
                    }
            }
        }

        private float getOutputTranslationX(
            Vector2 translation,
            CubismParameter parameter,
            CubismPhysicsParticle[] particles,
            int particleIndex,
            Vector2 gravity
        )
        {
            float outputValue = translation.X;

            if (IsInverted)
                outputValue *= -1.0f;

            return outputValue;
        }

        private float getOutputTranslationY(
            Vector2 translation,
            CubismParameter parameter,
            CubismPhysicsParticle[] particles,
            int particleIndex,
            Vector2 gravity
        )
        {
            float outputValue = translation.Y;

            if (IsInverted)
                outputValue *= -1.0f;

            return outputValue;
        }

        private float getOutputAngle(
            Vector2 translation,
            CubismParameter parameter,
            CubismPhysicsParticle[] particles,
            int particleIndex,
            Vector2 gravity
        )
        {
            Vector2 parentGravity;

            if (UseAngleCorrection)
            {
                if (particleIndex < 2)
                {
                    parentGravity = gravity;
                    parentGravity.Y *= -1.0f;
                }
                else
                {
                    parentGravity = particles[particleIndex - 1].Position - particles[particleIndex - 2].Position;
                }
            }
            else
            {
                parentGravity = gravity;
                parentGravity.Y *= -1.0f;
            }

            float outputValue = CubismMath.DirectionToRadian(parentGravity, translation);

            if (IsInverted)
                outputValue *= -1.0f;

            return outputValue;
        }

        private float getOutputScaleTranslationX() => TranslationScale.X;

        private float getOutputScaleTranslationY() => TranslationScale.Y;

        private float getOutputScaleAngle() => AngleScale;
    }
}
