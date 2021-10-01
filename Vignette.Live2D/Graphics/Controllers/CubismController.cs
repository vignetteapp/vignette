// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace Vignette.Live2D.Graphics.Controllers
{
    /// <summary>
    /// A component that can be inserted as a child of <see cref="CubismDrawable"/>
    /// and can make adjustments to the <see cref="CubismModel"/> which is obtained through dependency injection.
    /// </summary>
    public abstract class CubismController : Component
    {
        /// <summary>
        /// The model to make adjustments on which is resolved through dependency injection.
        /// </summary>
        /// <remarks>This is only populated after it has been loaded.</remarks>
        [Resolved]
        protected CubismModel Model { get; private set; }
    }
}
