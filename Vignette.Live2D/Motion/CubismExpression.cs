// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Collections.Generic;
using System.Linq;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.IO.Serialization;

namespace Vignette.Live2D.Motion
{
    public class CubismExpression : ICubismMotion
    {
        private double globalFadeInSeconds;

        public double GlobalFadeInSeconds
        {
            get => globalFadeInSeconds;
            set => globalFadeInSeconds = Math.Min(Math.Max(value, 0), 1);
        }

        private double globalFadeOutSeconds;

        public double GlobalFadeOutSeconds
        {
            get => globalFadeOutSeconds;
            set => globalFadeOutSeconds = Math.Min(Math.Max(value, 0), 1);
        }

        public double Weight { get; set; }

        private List<CubismExpressionParameter> parameters = new List<CubismExpressionParameter>();

        public CubismExpression(CubismModel model, CubismExpressionSetting setting)
        {
            GlobalFadeInSeconds = setting.FadeInTime;
            GlobalFadeOutSeconds = setting.FadeInTime;

            for (int i = 0; i < setting.Parameters.Count; i++)
            {
                var item = setting.Parameters[i];
                parameters.Add(new CubismExpressionParameter
                {
                    Parameter = model.Parameters.FirstOrDefault(p => p.Name == item.Id),
                    Blending = Enum.Parse<CubismExpressionBlending>(item.Blend),
                    Value = item.Value,
                });
            }
        }

        public void Update(double time, bool loop = false)
        {
            foreach (var expressionParam in parameters)
            {
                var param = expressionParam.Parameter;

                if (param == null)
                    continue;

                switch (expressionParam.Blending)
                {
                    case CubismExpressionBlending.Add:
                        param.Value += (float)(param.Value + expressionParam.Value * Weight);
                        break;

                    case CubismExpressionBlending.Multiply:
                        param.Value *= (float)((expressionParam.Value - 1) * Weight + 1.0);
                        break;

                    case CubismExpressionBlending.Overwrite:
                        param.Value = (float)(expressionParam.Value * (1 - Weight) + expressionParam.Value * Weight);
                        break;
                }
            }
        }
    }
}
