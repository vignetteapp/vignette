// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vignette.Live2D.Graphics.Controllers
{
    /// <summary>
    /// A controller that provides breathing motion to the <see cref="CubismModel"/>.
    /// </summary>
    public class CubismBreathController : CubismController
    {
        private readonly IEnumerable<CubismBreathParameter> settings = new List<CubismBreathParameter>();

        public CubismBreathController(params CubismBreathParameter[] parameters)
        {
            settings = parameters.Any() ? parameters : default_parameters;
        }

        protected override void Update()
        {
            base.Update();

            float phase = (float)Clock.ElapsedFrameTime / 1000 * 2.0f * MathF.PI;
            foreach (var setting in settings)
            {
                var parameter = Model.Parameters.FirstOrDefault(p => p.Name == setting.Parameter);

                if (parameter == null)
                    return;

                parameter.Value += (setting.Offset + setting.Peak * MathF.Sin(phase / setting.Cycle)) * setting.Weight;
            }
        }

        private static readonly CubismBreathParameter[] default_parameters = new[] { new CubismBreathParameter("ParamBreath", 0.5f, 0.5f, 3.2345f, 0.5f) };
    }

    public struct CubismBreathParameter
    {
        public string Parameter { get; set; }

        public float Offset { get; set; }

        public float Peak { get; set; }

        public float Cycle { get; set; }

        public float Weight { get; set; }

        public CubismBreathParameter(string parameterName, float offset, float peak, float cycle, float weight)
        {
            Parameter = parameterName;
            Offset = offset;
            Peak = peak;
            Cycle = cycle;
            Weight = weight;
        }
    }
}
