// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Allocation;
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

            if (tracker == null)
                return;

            // TODO: Do parameter manipulation here
        }
    }
}
