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

        private static string[] names_angle_x = new string[] { "ParamAngleX", "PARAM_ANGLE_X" };
        private static string[] names_angle_y = new string[] { "ParamAngleY", "PARAM_ANGLE_Y" };
        private static string[] names_angle_z = new string[] { "ParamAngleZ", "PARAM_ANGLE_Z" };
        private static string[] names_mouth_open_y = new string[] { "ParamMouthOpenY", "PARAM_MOUTH_OPEN_Y" };
        private static string[] names_eye_l_open = new string[] { "ParamEyeLOpen", "PARAM_EYE_L_OPEN" };
        private static string[] names_eye_r_open = new string[] { "ParamEyeROpen", "PARAM_EYE_R_OPEN" };

        private CubismParameter angleX => tryGetParameter(names_angle_x);
        private CubismParameter angleY => tryGetParameter(names_angle_y);
        private CubismParameter angleZ => tryGetParameter(names_angle_z);
        private CubismParameter mouthOpenY => tryGetParameter(names_mouth_open_y);
        private CubismParameter eyeLOpen => tryGetParameter(names_eye_l_open);
        private CubismParameter eyeROpen => tryGetParameter(names_eye_r_open);

        protected override void Update()
        {
            base.Update();

            if (tracker?.Faces.Count == 0)
                return;

            var face = tracker.Faces[0];

            setNormalizedParamValue(angleX, face.Angles.X);
            setNormalizedParamValue(angleY, face.Angles.Y);
            setNormalizedParamValue(angleZ, face.Angles.Z);
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
        private CubismParameter tryGetParameter(string[] paramPossibleNames)
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
