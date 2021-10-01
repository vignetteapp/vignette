// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osuTK;

namespace Vignette.Live2D.Physics
{
    public class CubismPhysicsRig
    {
        public CubismPhysicsSubRig[] SubRigs { get; set; }

        public Vector2 Gravity { get; set; } = new Vector2(0, -1.0f);

        public Vector2 Wind { get; set; } = Vector2.Zero;

        public void Initialize()
        {
            for (int i = 0; i < SubRigs.Length; ++i)
                SubRigs[i].Initialize();
        }

        public void Update(double delta)
        {
            for (int i = 0; i < SubRigs.Length; ++i)
                SubRigs[i].Update(delta);
        }
    }
}
