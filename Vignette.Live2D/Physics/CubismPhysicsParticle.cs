// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.
// This software implements Live2D. Copyright (c) Live2D Inc. All Rights Reserved.
// License for Live2D can be found here: http://live2d.com/eula/live2d-open-software-license-agreement_en.html

using osuTK;

namespace Vignette.Live2D.Physics
{
    public struct CubismPhysicsParticle
    {
        public Vector2 InitialPosition { get; set; }

        public float Mobility { get; set; }

        public float Delay { get; set; }

        public float Acceleration { get; set; }

        public float Radius { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 LastPosition { get; set; }

        public Vector2 LastGravity { get; set; }

        public Vector2 Force { get; set; }

        public Vector2 Velocity { get; set; }
    }
}
