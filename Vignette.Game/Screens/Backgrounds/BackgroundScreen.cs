// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System;
using osu.Framework.Screens;

namespace Vignette.Game.Screens.Backgrounds
{
    public class BackgroundScreen : Screen, IEquatable<BackgroundScreen>
    {
        public bool Equals(BackgroundScreen other)
            => other?.GetType() == GetType();
    }
}
