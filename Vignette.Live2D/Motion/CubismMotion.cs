// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using System;
using System.Collections.Generic;
using System.Linq;
using Vignette.Live2D.Graphics;
using Vignette.Live2D.IO.Serialization;
using Vignette.Live2D.Motion.Segments;
using Vignette.Live2D.Utils;

namespace Vignette.Live2D.Motion
{
    public class CubismMotion : ICubismMotion
    {
        public double GlobalFadeInSeconds { get; set; }

        public double GlobalFadeOutSeconds { get; set; }

        public double Duration { get; private set; } = double.PositiveInfinity;

        public double Weight { get; set; } = 1.0;

        public bool LoopFading { get; set; }

        public bool CanLoop { get; private set; }

        private List<Curve> curves;

        private List<CubismMotionEvent> events = new List<CubismMotionEvent>();

        private readonly Dictionary<CubismId, Curve> effectCurves = new Dictionary<CubismId, Curve>();

        private readonly List<(CubismId, CubismParameter)> effectUnusedParameters = new List<(CubismId, CubismParameter)>();

        public CubismMotion(CubismModel model, CubismMotionSetting setting, CubismModelSetting.FileReference.Motion modelMotionSetting = null)
        {
            Duration = setting.Meta.Duration;
            CanLoop = setting.Meta.Loop;

            GlobalFadeInSeconds = double.IsNaN(modelMotionSetting?.FadeInTime ?? double.NaN)
                ? double.IsNaN(setting.Meta.FadeInTime)
                    ? 0.0
                    : setting.Meta.FadeInTime
                : modelMotionSetting.FadeInTime;

            GlobalFadeOutSeconds = double.IsNaN(modelMotionSetting?.FadeOutTime ?? double.NaN)
                ? double.IsNaN(setting.Meta.FadeOutTime)
                    ? 0.0
                    : setting.Meta.FadeOutTime
                : modelMotionSetting.FadeOutTime;

            var curves = new List<Curve>();

            foreach (var item in setting.Curves)
            {
                var curve = parseCurve(item, model);

                if (curve == null)
                    continue;

                if (curve.TargetType == CubismMotionTarget.Model)
                    effectCurves[curve.Effect] = curve;
                else
                    curves.Add(curve);
            }

            this.curves = curves;
            if (setting.Meta.UserDataCount == (setting.UserData?.Count ?? 0))
            {
                for (int i = 0; i < setting.Meta.UserDataCount; i++)
                {
                    events.Add(new CubismMotionEvent
                    {
                        Time = setting.UserData[i].Time,
                        Value = setting.UserData[i].Value,
                    });
                }
            }
        }

        public void Update(double time, bool loop = false)
        {
            double timeInMotion = time % Duration;

            double globalFadeInWeight = calculateFadeInWeight(time, Duration, GlobalFadeInSeconds, loop, LoopFading);
            double globalFadeOutWeight = calculateFadeOutWeight(time, Duration, GlobalFadeOutSeconds, loop, LoopFading);

            var effectValues = new Dictionary<ICubismId, double>();
            foreach ((var effect, var curve) in effectCurves)
                effectValues[effect] = curve.ValueAt(time);

            int i;
            for (i = 0; i < curves.Count; i++)
            {
                var curve = curves[i];

                if (curve.TargetType == CubismMotionTarget.Parameter)
                {
                    double value, weight;
                    value = curve.ValueAt(timeInMotion);

                    if (curve.Effect != null)
                    {
                        if (effectValues.TryGetValue(curve.Effect, out double effectValue))
                            value *= effectValue;
                    }

                    if (double.IsNaN(curve.FadeInTime) && double.IsNaN(curve.FadeOutTime))
                    {
                        weight = globalFadeInWeight * globalFadeOutWeight;
                    }
                    else
                    {
                        weight = Weight;

                        if (double.IsNaN(curve.FadeInTime))
                            weight *= globalFadeInWeight;
                        else
                            weight *= calculateFadeOutWeight(time, Duration, curve.FadeInTime, loop, LoopFading);

                        if (double.IsNaN(curve.FadeOutTime))
                            weight *= globalFadeOutWeight;
                        else
                            weight *= calculateFadeOutWeight(time, Duration, curve.FadeOutTime, loop, LoopFading);
                    }

                    double currentValue = curve.Parameter.Value;
                    curve.Parameter.Value = (float)(currentValue + (value - currentValue) * weight);
                }
                else if (curve.TargetType == CubismMotionTarget.PartOpacity)
                {
                    curve.Part.CurrentOpacity = (float)curve.ValueAt(timeInMotion);
                }
            }

            foreach ((var effectId, var target) in effectUnusedParameters)
            {
                if (effectValues.TryGetValue(effectId, out var value))
                {
                    double weight = globalFadeInWeight * globalFadeOutWeight;
                    double currentValue = target.Value;
                    target.Value = (float)(currentValue + (value - currentValue) * weight);
                }

                i++;
            }
        }

        public ICubismId GetEffect(string id)
        {
            foreach (var effect in effectCurves.Keys)
            {
                if (effect.Equals(id))
                    return effect;
            }

            return null;
        }

        public bool SetEffectParameters(CubismId effectId, IEnumerable<CubismParameter> parameters)
        {
            if (!effectCurves.ContainsKey(effectId))
                return false;

            var parameterArray = parameters.ToArray();
            bool[] usedIds = new bool[parameterArray.Length];
            foreach (var curve in curves)
            {
                curve.Effect = null;
                for (int i = 0; i < parameterArray.Length; i++)
                {
                    if (curve.Parameter == parameterArray[i])
                    {
                        curve.Effect = effectId;
                        usedIds[i] = true;
                        break;
                    }
                }
            }

            effectUnusedParameters.RemoveAll(p => p.Item1 == effectId);
            for (int i = 0; i < parameters.Count(); i++)
            {
                if (!usedIds[i])
                    effectUnusedParameters.Add((effectId, parameterArray[i]));
            }

            return true;
        }

        public IEnumerable<string> GetFiredEvents(double time, double previousTime, bool loop)
        {
            var result = new List<string>();

            if (!loop)
            {
                foreach (var @event in events)
                {
                    if ((previousTime < @event.Time) && (@event.Time <= time))
                        result.Add(@event.Value);
                }
            }
            else
            {
                double timeInMotion = time % Duration;
                double previousTimeInMotion = previousTime % Duration;
                foreach (var @event in events)
                {
                    if (((previousTimeInMotion < @event.Time) && (@event.Time <= timeInMotion)) ||
                        ((timeInMotion < previousTimeInMotion) && ((@event.Time <= timeInMotion) || (previousTimeInMotion <= @event.Time))))
                    {
                        result.Add(@event.Value);
                    }
                }
            }

            return result;
        }

        private Curve parseCurve(CubismMotionSetting.Curve item, CubismModel model)
        {
            if (!Enum.TryParse(item.Target, out CubismMotionTarget target))
                return null;

            var curve = new Curve { TargetType = target };

            switch (target)
            {
                case CubismMotionTarget.Model:
                    break;

                case CubismMotionTarget.Parameter:
                    var parameter = model.Parameters.FirstOrDefault(p => p.Name == item.Id);

                    if (parameter == null)
                        return null;

                    curve.Parameter = parameter;
                    break;

                case CubismMotionTarget.PartOpacity:
                    var part = model.Parts.FirstOrDefault(p => p.Name == item.Id);

                    if (part == null)
                        return null;

                    curve.Part = part;
                    break;
            }

            switch (item.Target)
            {
                case "Model":
                    curve.TargetType = CubismMotionTarget.Model;
                    curve.Effect = new CubismId { Name = item.Id };
                    break;

                case "Parameter":
                    if (!model.Parameters.Any(p => p.Name == item.Id))
                        return null;

                    curve.TargetType = CubismMotionTarget.Parameter;
                    curve.Parameter = model.Parameters.FirstOrDefault(p => p.Name == item.Id);
                    break;

                case "PartOpacity":
                    if (!model.Parts.Any(p => p.Name == item.Id))
                        return null;

                    curve.TargetType = CubismMotionTarget.PartOpacity;
                    curve.Part = model.Parts.FirstOrDefault(p => p.Name == item.Id);
                    break;

                default:
                    return null;
            }

            curve.FadeInTime = item.FadeInTime;
            curve.FadeOutTime = item.FadeOutTime;

            var segments = item.Segments;
            int segmentCount = item.Segments.Count;

            var last = new ControlPoint(segments[0], segments[1]);
            var segmentList = new List<Segment>();

            for (int i = 2; i < segmentCount;)
            {
                switch ((SegmentType)segments[i])
                {
                    case SegmentType.Linear:
                        {
                            var segment = new LinearSegment();
                            segment.Points[0] = last;
                            segment.Points[1] = new ControlPoint(segments[i + 1], segments[i + 2]);
                            segmentList.Add(segment);
                            last = segment.Points[1];

                            i += 3;
                            break;
                        }

                    case SegmentType.Bezier:
                        {
                            var segment = new BezierSegment();
                            segment.Points[0] = last;
                            segment.Points[1] = new ControlPoint(segments[i + 1], segments[i + 2]);
                            segment.Points[2] = new ControlPoint(segments[i + 3], segments[i + 4]);
                            segment.Points[3] = new ControlPoint(segments[i + 5], segments[i + 6]);
                            segmentList.Add(segment);
                            last = segment.Points[3];

                            i += 7;
                            break;
                        }

                    case SegmentType.Stepped:
                        {
                            var segment = new SteppedSegment();
                            segment.Points[0] = last;
                            segment.Points[1] = new ControlPoint(segments[i + 1], segments[i + 2]);
                            segmentList.Add(segment);
                            last = segment.Points[1];

                            i += 3;
                            break;
                        }

                    case SegmentType.InverseStepped:
                        {
                            var segment = new InverseSteppedSegment();
                            segment.Points[0] = last;
                            segment.Points[1] = new ControlPoint(segments[i + 1], segments[i + 2]);
                            segmentList.Add(segment);
                            last = segment.Points[1];

                            i += 3;
                            break;
                        }

                    default:
                        throw new ArgumentOutOfRangeException($"Segment (index: {i}) is out of range of supported values.");
                }
            }

            curve.Segments = segmentList;
            return curve;
        }

        private static double calculateFadeInWeight(double time, double duration, double fadeTime, bool loop, bool loopFading)
        {
            if (fadeTime <= 0)
                return 1;

            return (!loop || !loopFading)
                ? CubismMath.EaseSine(time / fadeTime)
                : CubismMath.EaseSine(time % duration / fadeTime);
        }

        private static double calculateFadeOutWeight(double time, double duration, double fadeTime, bool loop, bool loopFading)
        {
            if (fadeTime <= 0)
                return 1;

            if (!loop)
                return CubismMath.EaseSine((duration - time) / fadeTime);
            else
                return (!loopFading) ? 1 : CubismMath.EaseSine((duration - (time % duration)) / fadeTime);
        }
    }
}
