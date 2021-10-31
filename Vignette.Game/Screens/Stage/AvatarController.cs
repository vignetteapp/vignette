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

            setNormalizedParamValue("ParamMouthOpenY", face.MouthOpen);
            setNormalizedParamValue("ParamEyeLOpen", face.LeftEyeOpen);
            setNormalizedParamValue("ParamEyeROpen", face.RightEyeOpen);
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
