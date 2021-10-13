// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class Spinner : CompositeDrawable
    {
        private readonly ThemableSpriteIcon foreground;

        public Spinner()
        {
            InternalChild = foreground = new ThemableSpriteIcon
            {
                RelativeSizeAxes = Axes.Both,
                Colour = ThemeSlot.AccentPrimary,
                Icon = FontAwesome.Solid.CircleNotch,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            foreground.Spin(1000, RotationDirection.Clockwise, 90).Loop();
        }
    }
}
