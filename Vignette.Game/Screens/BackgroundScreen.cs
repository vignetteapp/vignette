// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Vignette.Game.Screens
{
    public abstract class BackgroundScreen : Screen, IEquatable<BackgroundScreen>
    {
        protected BackgroundScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        public bool Equals(BackgroundScreen other)
            => other?.GetType() == GetType();

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);
        }

        public override bool OnExiting(IScreen next)
        {
            return base.OnExiting(next);
        }
    }
}
