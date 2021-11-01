// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Linq;
using osu.Framework.Allocation;
using Vignette.Game.Settings;
using Vignette.Game.Settings.Sections;
using Vignette.Game.Tracking;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Graphics.Controllers;

namespace Vignette.Game.Screens.Stage
{
    public class AvatarController : CubismController
    {
        [Resolved(canBeNull: true)]
        private TrackingComponent tracker { get; set; }

        [Resolved]
        private SettingsOverlay overlay { get; set; }

        private CubismParameter angleX => tryGetParameter("ParamAngleX", "PARAM_ANGLE_X");
        private CubismParameter angleY => tryGetParameter("ParamAngleY", "PARAM_ANGLE_Y");
        private CubismParameter angleZ => tryGetParameter("ParamAngleZ", "PARAM_ANGLE_Z");
        private CubismParameter bodyAngleX => tryGetParameter("ParamBodyAngleX", "PARAM_BODY_ANGLE_X");
        private CubismParameter bodyAngleY => tryGetParameter("ParamBodyAngleY", "PARAM_BODY_ANGLE_Y");
        private CubismParameter bodyAngleZ => tryGetParameter("ParamBodyAngleZ", "PARAM_BODY_ANGLE_Z");
        private CubismParameter mouthOpenY => tryGetParameter("ParamMouthOpenY", "PARAM_MOUTH_OPEN_Y");
        private CubismParameter eyeLOpen => tryGetParameter("ParamEyeLOpen", "PARAM_EYE_L_OPEN");
        private CubismParameter eyeROpen => tryGetParameter("ParamEyeROpen", "PARAM_EYE_R_OPEN");

        private CubismPart armA => tryGetPart("PartArmA", "PART_ARM_A");
        private CubismPart armB => tryGetPart("PartArmB", "PART_ARM_B");

        private FaceData face;

        #region Scalars for calibration
        private float mouthOpenScalar = 1;
        private float eyeLOpenScalar = 1;
        private float eyeROpenScalar = 1;
        #endregion

        protected override void LoadComplete()
        {
            base.LoadComplete();
            overlay.AvatarSection.CalibrateAction += Calibrate;
        }

        public void Calibrate()
        {
            if (face == null)
                return;

            mouthOpenScalar = face.MouthOpen;
            eyeLOpenScalar = face.LeftEyeOpen;
            eyeROpenScalar = face.RightEyeOpen;
        }

        protected override void Update()
        {
            base.Update();

            if (tracker?.Faces.Count == 0)
                return;

            face = tracker.Faces[0];

            setPartOpacity(armA, 1f);
            setPartOpacity(armB, 0f);

            setNormalized(angleX, face.Angles.X);
            setNormalized(angleY, face.Angles.Y);
            setNormalized(angleZ, face.Angles.Z);

            setNormalized(bodyAngleX, face.Position.X);
            setNormalized(bodyAngleY, face.Position.Y);
            setNormalized(bodyAngleZ, face.Position.Z);

            setNormalized(mouthOpenY, face.MouthOpen / mouthOpenScalar);
            setNormalized(eyeLOpen, face.LeftEyeOpen / eyeLOpenScalar);
            setNormalized(eyeROpen, face.RightEyeOpen / eyeROpenScalar);
        }

        /// <summary>
        /// Some models use CONSTANT_CASE for cubism parameter names while others use PascalCase.
        /// So there may be other cases or names used. This function is to try them one by one.
        /// </summary>
        /// <param name="possibleNames">
        /// The possible names of the cubism parameter.
        /// </param>
        /// <returns>
        /// The first cubism parameter found.
        /// </returns>
        private CubismParameter tryGetParameter(params string[] possibleNames)
        {
            CubismParameter parameter = null;

            foreach (var name in possibleNames)
            {
                parameter = Model.Parameters.FirstOrDefault(p => p.Name == name);
                if (parameter != null)
                    break;
            }

            return parameter;
        }

        private CubismPart tryGetPart(params string[] possibleNames)
        {
            CubismPart part = null;

            foreach (var name in possibleNames)
            {
                part = Model.Parts.FirstOrDefault(p => p.Name == name);
                if (part != null)
                    break;
            }

            return part;
        }

        private void setNormalized(CubismParameter parameter, float value)
        {
            if (parameter == null)
                return;

            parameter.TargetValue = parameter.Maximum * value;
            parameter.CurrentValue = lerp(parameter.CurrentValue, parameter.TargetValue, .3f);
        }

        private void setPartOpacity(CubismPart part, float value)
        {
            if (part == null)
                return;

            part.TargetOpacity = value;
            part.CurrentOpacity = lerp(part.CurrentOpacity, part.TargetOpacity, .5f);
        }

        private float lerp(float start, float end, float p) => (end - start) * p + start;
    }
}
