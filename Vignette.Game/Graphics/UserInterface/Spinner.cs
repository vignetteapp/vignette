// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Themeing;

namespace Vignette.Game.Graphics.UserInterface
{
    public class Spinner : CompositeDrawable
    {
        private ThemableSpriteIcon background;

        private ThemableSpriteIcon foreground;

        public Spinner()
        {
            InternalChildren = new Drawable[]
            {
                background = new ThemableSpriteIcon
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.AccentLight,
                    Icon = FontAwesome.Solid.CircleNotch,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                foreground = new ThemableSpriteIcon
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = ThemeSlot.AccentPrimary,
                    Icon = FontAwesome.Solid.CircleNotch,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            background.Spin(1000, RotationDirection.Clockwise).Loop();
            foreground.Spin(1000, RotationDirection.Clockwise, 90).Loop();
        }
    }
}
