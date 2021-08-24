// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Themeing;

namespace Vignette.Game.Overlays.MainMenu
{
    public class ExitButton : FluentButtonBase
    {
        private readonly ThemableBox background;

        public ExitButton()
        {
            BackgroundResting = ThemeSlot.Transparent;
            BackgroundHovered = ThemeSlot.Error;
            BackgroundPressed = ThemeSlot.ErrorBackground;
            BackgroundDisabled = ThemeSlot.Transparent;

            Children = new Drawable[]
            {
                    background = new ThemableBox
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    new ThemableSpriteIcon
                    {
                        Icon = FluentSystemIcons.Dismiss28,
                        Size = new Vector2(9),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = ThemeSlot.Black,
                    },
            };
        }

        protected override void UpdateBackground(ThemeSlot slot)
            => background.Colour = slot;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            Action = () => host.Exit();
        }
    }
}
