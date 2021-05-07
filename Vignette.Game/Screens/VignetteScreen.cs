// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Screens;

namespace Vignette.Game.Screens
{
    public abstract class VignetteScreen : Screen
    {
        [Resolved(canBeNull: true)]
        private BackgroundScreenStack backgroundStack { get; set; }

        private BackgroundScreen ownedBackground;

        private BackgroundScreen background;

        protected virtual BackgroundScreen CreateBackground() => null;

        public override void OnEntering(IScreen last)
        {
            backgroundStack?.Push(ownedBackground = CreateBackground());
            background = backgroundStack?.CurrentScreen as BackgroundScreen;

            if (ownedBackground != background)
            {
                ownedBackground?.Dispose();
                ownedBackground = null;
            }

            base.OnEntering(last);
        }

        public override bool OnExiting(IScreen next)
        {
            if (base.OnExiting(next))
                return true;

            if (ownedBackground != null && backgroundStack?.CurrentScreen == ownedBackground)
                backgroundStack?.Exit();

            return false;
        }
    }
}
