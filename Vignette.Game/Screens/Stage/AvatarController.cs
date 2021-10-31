// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Linq;
using osu.Framework.Allocation;
using Vignette.Game.Tracking;
using Vignette.Live2D.Graphics.Controllers;

namespace Vignette.Game.Screens.Stage
{
    public class AvatarController : CubismController
    {
        [Resolved]
        private TrackingComponent tracker { get; set; }

        protected override void Update()
        {
            base.Update();

            if (tracker.Faces.Count == 0)
                return;

            var face = tracker.Faces[0];

            setNormalizedParamValue("ParamAngleX", face.Angles.X);
            setNormalizedParamValue("PARAM_ANGLE_X", face.Angles.X);

            setNormalizedParamValue("ParamAngleY", face.Angles.Y);
            setNormalizedParamValue("PARAM_ANGLE_Y", face.Angles.Y);

            setNormalizedParamValue("PARAM_ANGLE_Z", face.Angles.Z);
            setNormalizedParamValue("ParamAngleZ", face.Angles.Z);

            setNormalizedParamValue("ParamMouthOpenY", face.MouthOpen);
            setNormalizedParamValue("PARAM_MOUTH_OPEN_Y", face.MouthOpen);

            setNormalizedParamValue("ParamEyeLOpen", face.LeftEyeOpen);
            setNormalizedParamValue("PARAM_EYE_L_OPEN", face.LeftEyeOpen);

            setNormalizedParamValue("ParamEyeROpen", face.RightEyeOpen);
            setNormalizedParamValue("PARAM_EYE_R_OPEN", face.RightEyeOpen);
        }

        private void setNormalizedParamValue(string paramName, float value)
        {
            var parameter = Model.Parameters.FirstOrDefault(p => p.Name == paramName);

            if (parameter == null)
                return;

            parameter.Value = parameter.Maximum * value;
        }
    }
}
