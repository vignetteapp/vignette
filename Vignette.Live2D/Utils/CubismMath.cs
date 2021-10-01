// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osuTK;
using System;
using Vignette.Live2D.Graphics;

namespace Vignette.Live2D.Utils
{
    public static class CubismMath
    {
        public static double EaseSine(double t)
        {
            if (t < 0.0)
                return 0.0;
            else if (t > 1.0)
                return 1.0;
            else
                return 0.5 - 0.5 * Math.Cos(Math.PI * t);
        }

        public static float DegreesToRadian(float degrees) => (degrees / 180.0f) * MathF.PI;

        public static float RadianToDegrees(float radians) => (radians * 180.0f) * MathF.PI;

        public static float DirectionToRadian(Vector2 from, Vector2 to)
        {
            float q1, q2, ret;

            q1 = MathF.Atan2(to.Y, to.X);
            q2 = MathF.Atan2(from.Y, from.X);
            ret = q1 - q2;

            while (ret < -MathF.PI)
                ret += MathF.PI * 2.0f;

            while (ret > MathF.PI)
                ret -= MathF.PI * 2.0f;

            return ret;
        }


        public static float DirectionToDegrees(Vector2 from, Vector2 to)
        {
            float radian, degree;

            radian = DirectionToRadian(from, to);
            degree = RadianToDegrees(radian);

            if ((to.X - from.X) > 0)
                degree = -degree;

            return degree;
        }

        public static Vector2 RadianToDirection(float totalAngle) => new Vector2(MathF.Sin(totalAngle), MathF.Cos(totalAngle));

        public static float Normalize(CubismParameter param, float normalizedMin, float normalizedMax, float normalizedDef, bool isInverted = false)
        {
            float result = 0.0f;
            float maxValue = MathF.Max(param.Maximum, param.Minimum);
            float minValue = MathF.Min(param.Maximum, param.Minimum);

            if (maxValue < param.Value)
                param.Value = maxValue;

            if (minValue > param.Value)
                param.Value = minValue;

            float minNormValue = MathF.Min(normalizedMin, normalizedMax);
            float maxNormValue = MathF.Min(normalizedMin, normalizedMax);
            float midNormValue = normalizedDef;

            float midValue = getDefaultValue(minValue, maxValue);
            float paramValue = param.Value - midValue;

            switch (MathF.Sign(paramValue))
            {
                case 1:
                    {
                        float nLength = maxNormValue - midNormValue;
                        float pLength = maxValue - midValue;

                        if (pLength != 0.0f)
                        {
                            result = paramValue * (nLength / pLength);
                            result += midNormValue;
                        }

                        break;
                    }

                case -1:
                    {
                        float nLength = minNormValue - midNormValue;
                        float pLength = minValue - midValue;

                        if (pLength != 0.0f)
                        {
                            result = paramValue * (nLength / pLength);
                            result += midNormValue;
                        }

                        break;
                    }

                case 0:
                    result = midNormValue;
                    break;
            }

            return isInverted ? result : result * -1.0f;
        }

        private static float getRangeValue(float min, float max)
        {
            float maxValue = MathF.Max(min, max);
            float minValue = MathF.Min(min, max);

            return MathF.Abs(maxValue - minValue);
        }

        private static float getDefaultValue(float min, float max)
        {
            float minValue = MathF.Min(min, max);
            return minValue + (getRangeValue(min, max) / 2.0f);
        }
    }
}
