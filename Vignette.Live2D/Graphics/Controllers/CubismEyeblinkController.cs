// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Allocation;
using osu.Framework.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Vignette.Live2D.Graphics.Controllers
{
    /// <summary>
    /// A controller that allows the <see cref="CubismModel"/> to perform eye blinking.
    /// </summary>
    public class CubismEyeblinkController : CubismController
    {
        /// <summary>
        /// Duration between eyeblinks.
        /// </summary>
        public double BlinkInterval { get; set; } = 2.0;

        /// <summary>
        /// Duration on how long the eyes should be closed.
        /// </summary>
        public double ClosedDuration { get; set; } = 0.15;

        /// <summary>
        /// Duration on the closing motion of the eyes.
        /// </summary>
        public double ClosingDuration { get; set; } = 0.1;

        /// <summary>
        /// Duration on the opening motion of the eyes.
        /// </summary>
        public double OpeningDuration { get; set; } = 0.05;

        private EyeState state;
        private double nextBlinkTime;
        private double currentStateStartTime;
        private IEnumerable<CubismParameter> parameters;

        /// <summary>
        /// Create a new eyeblink controller with the option to predefine parameters.
        /// </summary>
        /// <param name="parameters">The parameters to apply the eyeblink motion to.</param>
        public CubismEyeblinkController(params CubismParameter[] parameters)
        {
            this.parameters = parameters;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!parameters.Any())
            {
                var eyeblinkGroup = Model.Settings.Groups.FirstOrDefault(g => g.Name == "EyeBlink");
                parameters = Model.Parameters.Where(p => eyeblinkGroup.Ids.Contains(p.Name));
            }
        }

        protected override void Update()
        {
            base.Update();

            double time = Clock.CurrentTime / 1000;

            double value = 0.0f;
            double t;

            switch (state)
            {
                case EyeState.Closing:
                    t = (time - currentStateStartTime) / ClosingDuration;

                    if (1.0f <= t)
                    {
                        t = 1.0f;
                        state = EyeState.Closed;
                        currentStateStartTime = time;
                    }

                    value = 1.0f - t;
                    break;

                case EyeState.Closed:
                    t = (time - currentStateStartTime) / ClosedDuration;

                    if (1.0f <= t)
                    {
                        state = EyeState.Opening;
                        currentStateStartTime = time;
                    }

                    value = 0;
                    break;

                case EyeState.Opening:
                    t = (time - currentStateStartTime) / OpeningDuration;

                    if (1.0f <= t)
                    {
                        t = 1.0f;
                        state = EyeState.Open;
                        nextBlinkTime = time + determineNextBlinkTime();
                    }

                    value = t;
                    break;

                case EyeState.Open:
                    if (nextBlinkTime < time)
                    {
                        state = EyeState.Closing;
                        currentStateStartTime = time;
                    }

                    value = 1.0f;
                    break;

                case EyeState.Initial:
                    state = EyeState.Open;
                    nextBlinkTime = time + determineNextBlinkTime();
                    value = 1.0f;
                    break;
            }

            foreach (var param in parameters)
                param.Value = (float)value;
        }

        private double determineNextBlinkTime()
        {
            double r = RNG.NextDouble();
            return r * (2.0f * BlinkInterval - 1.0f);
        }

        private enum EyeState
        {
            Initial,
            Open,
            Opening,
            Closed,
            Closing,
        }
    }
}
