// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Linq;
using osu.Framework.Allocation;
using Vignette.Game.Tracking;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.Graphics.Controllers;

namespace Vignette.Game.Screens.Stage
{
    public class AvatarController : CubismController
    {
        [Resolved(canBeNull: true)]
        private TrackingComponent tracker { get; set; }

        private CubismParameter angleX => tryGetParameter("ParamAngleX", "PARAM_ANGLE_X");
        private CubismParameter angleY => tryGetParameter("ParamAngleY", "PARAM_ANGLE_Y");
        private CubismParameter angleZ => tryGetParameter("ParamAngleZ", "PARAM_ANGLE_Z");
        private CubismParameter bodyAngleX => tryGetParameter("ParamBodyAngleX", "PARAM_BODY_ANGLE_X");
        private CubismParameter bodyAngleY => tryGetParameter("ParamBodyAngleY", "PARAM_BODY_ANGLE_Y");
        private CubismParameter bodyAngleZ => tryGetParameter("ParamBodyAngleZ", "PARAM_BODY_ANGLE_Z");
        private CubismParameter mouthOpenY => tryGetParameter("ParamMouthOpenY", "PARAM_MOUTH_OPEN_Y");
        private CubismParameter eyeLOpen => tryGetParameter("ParamEyeLOpen", "PARAM_EYE_L_OPEN");
        private CubismParameter eyeROpen => tryGetParameter("ParamEyeROpen", "PARAM_EYE_R_OPEN");

        protected override void Update()
        {
            base.Update();

            if (tracker?.Faces.Count == 0)
                return;

            var face = tracker.Faces[0];

            setNormalizedParamValue(angleX, face.Angles.X);
            setNormalizedParamValue(angleY, face.Angles.Y);
            setNormalizedParamValue(angleZ, face.Angles.Z);

            setNormalizedParamValue(bodyAngleX, face.Position.X);
            setNormalizedParamValue(bodyAngleY, face.Position.Y);
            setNormalizedParamValue(bodyAngleZ, face.Position.Z);

            setNormalizedParamValue(mouthOpenY, face.MouthOpen);
            setNormalizedParamValue(eyeLOpen, face.LeftEyeOpen);
            setNormalizedParamValue(eyeROpen, face.RightEyeOpen);
        }

        /// <summary>
        /// Some models use CONSTANT_CASE for cubism parameter names while others use PascalCase.
        /// So there may be other cases or names used. This function is to try them one by one.
        /// </summary>
        /// <param name="paramPossibleNames">
        /// The possible names of the cubism parameter.
        /// </param>
        /// <returns>
        /// The first cubism parameter found.
        /// </returns>
        private CubismParameter tryGetParameter(params string[] paramPossibleNames)
        {
            CubismParameter parameter = null;

            foreach (var name in paramPossibleNames)
            {
                parameter = Model.Parameters.FirstOrDefault(p => p.Name == name);
                if (parameter != null)
                    break;
            }

            return parameter;
        }

        private void setNormalizedParamValue(CubismParameter parameter, float value)
        {
            if (parameter == null)
                return;

            parameter.Value = parameter.Maximum * value;
        }
    }
}
