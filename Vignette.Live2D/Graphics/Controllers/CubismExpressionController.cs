// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Allocation;
using System.Collections.Generic;
using Vignette.Live2D.IO.Serialization;
using Vignette.Live2D.Motion;
using Vignette.Live2D.Utils;

namespace Vignette.Live2D.Graphics.Controllers
{
    /// <summary>
    /// A controller that manages expressions of a Live2D model.
    /// </summary>
    public class CubismExpressionController : CubismController
    {
        protected readonly Dictionary<string, CubismExpression> Entries = new Dictionary<string, CubismExpression>();

        public CubismExpression Current { get; private set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            var definitions = Model.Settings.FileReferences.Expressions;

            if (definitions == null)
                return;

            foreach (var def in definitions)
            {
                using var stream = Model.Resources.GetStream(def.File);
                Entries.Add(def.Name, new CubismExpression(Model, CubismUtils.ReadJsonSetting<CubismExpressionSetting>(stream)));
            }
        }

        /// <summary>
        /// Plays out the named expression.
        /// </summary>
        /// <param name="name">The name of the expression as defined in the model setting.</param>
        /// <returns>Whether the expression has successfully been set.</returns>
        public bool Play(string name)
        {
            if (!Entries.TryGetValue(name, out var expression))
                return false;

            Current = expression;

            return true;
        }

        protected override void Update()
        {
            base.Update();
            Current?.Update(Clock.CurrentTime / 1000);
        }
    }
}
