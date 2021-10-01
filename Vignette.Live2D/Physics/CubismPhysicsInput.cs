// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osuTK;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Utils;

namespace Vignette.Live2D.Physics
{
    public class CubismPhysicsInput
    {
        public string SourceId { get; set; }

        public Vector2 ScaleOfTranslation { get; set; }

        public float AngleScale { get; set; }

        public float Weight { get; set; }

        public CubismPhysicsSource SourceComponent { get; set; }

        public bool IsInverted { get; set; }

        public CubismParameter Source { get; set; }

        public NormalizedParameterValueGetter GetNormalizedParameterValue { get; set; }

        public delegate void NormalizedParameterValueGetter(
            ref Vector2 targetTranslation,
            ref float targetAngle,
            CubismParameter parameter,
            CubismPhysicsNormalization normalization,
            float weight
        );

        public void InitializeGetter()
        {
            switch (SourceComponent)
            {
                case CubismPhysicsSource.X:
                    GetNormalizedParameterValue = getInputTranslationXFromNormalizedParameterValue;
                    break;

                case CubismPhysicsSource.Y:
                    GetNormalizedParameterValue = getInputTranslationYFromNormalizedParameterValue;
                    break;

                case CubismPhysicsSource.Angle:
                    GetNormalizedParameterValue = getInputAngleFromNormalizedParameterValue;
                    break;
            }
        }

        private void getInputTranslationXFromNormalizedParameterValue(
            ref Vector2 targetTranslation,
            ref float targetAngle,
            CubismParameter parameter,
            CubismPhysicsNormalization normalization,
            float weight
        )
        {
            targetTranslation.X += CubismMath.Normalize(
                parameter,
                normalization.Position.Minimum,
                normalization.Position.Maximum,
                normalization.Position.Default,
                IsInverted) * weight;
        }

        private void getInputTranslationYFromNormalizedParameterValue(
            ref Vector2 targetTranslation,
            ref float targetAngle,
            CubismParameter parameter,
            CubismPhysicsNormalization normalization,
            float weight
        )
        {
            targetTranslation.Y += CubismMath.Normalize(
                parameter,
                normalization.Position.Minimum,
                normalization.Position.Maximum,
                normalization.Position.Default,
                IsInverted) * weight;
        }

        private void getInputAngleFromNormalizedParameterValue(
            ref Vector2 targetTranslation,
            ref float targetAngle,
            CubismParameter parameter,
            CubismPhysicsNormalization normalization,
            float weight
        )
        {
            targetAngle += CubismMath.Normalize(
                parameter,
                normalization.Angle.Minimum,
                normalization.Angle.Maximum,
                normalization.Angle.Default,
                IsInverted) * weight;
        }
    }
}
