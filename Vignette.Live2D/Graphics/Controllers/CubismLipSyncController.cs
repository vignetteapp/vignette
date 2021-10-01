// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Allocation;
using osu.Framework.Audio;
using System.Collections.Generic;
using System.Linq;

namespace Vignette.Live2D.Graphics.Controllers
{
    /// <summary>
    /// A controller that provides lip sync motion to the <see cref="CubismModel"/>.
    /// </summary>
    public class CubismLipSyncController : CubismController
    {
        /// <summary>
        /// The audio source that will provide amplitude data for the controller.
        /// </summary>
        public IHasAmplitudes Source { get; set; }

        private IEnumerable<CubismParameter> parameters;

        /// <summary>
        /// Initialize the controller with a predefined set of parameters.
        /// </summary>
        /// <param name="parameters">The parameters this controller will act on.</param>
        public CubismLipSyncController(params CubismParameter[] parameters)
        {
            this.parameters = parameters;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (parameters?.Any() ?? false)
                return;

            var lipSyncGroup = Model.Settings.Groups.FirstOrDefault(g => g.Name == "LipSync");

            if (lipSyncGroup == null)
                return;

            parameters = Model.Parameters.Where(p => lipSyncGroup.Ids.Contains(p.Name));
        }

        protected override void Update()
        {
            base.Update();

            if (parameters == null)
                return;

            foreach (var parameter in parameters)
                parameter.Value = parameter.Maximum * (Source?.CurrentAmplitudes.Average ?? 0.0f);
        }
    }
}
