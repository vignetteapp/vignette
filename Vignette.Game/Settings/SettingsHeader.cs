// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Vignette.Game.Graphics.Shapes;
using Vignette.Game.Graphics.Sprites;
using Vignette.Game.Graphics.Typesets;
using Vignette.Game.Graphics.UserInterface;
using Vignette.Game.Graphics.Themeing;

namespace Vignette.Game.Settings
{
    public class SettingsHeader : Container
    {
        public readonly FluentSearchBox SearchBox;

        public bool ShowLogo
        {
            set
            {
                logo.FadeTo(value ? 1 : 0, 200, Easing.OutQuint);
                content.ResizeHeightTo(value ? 150 : 90, 300, Easing.OutQuint);
            }
        }

        private readonly ThemableSpriteIcon logo;
        private readonly Container content;

        public SettingsHeader()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            InternalChildren = new Drawable[]
            {
                new ThemableBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                content = new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Padding = new MarginPadding
                    {
                        Top = 20,
                        Bottom = 30,
                        Horizontal = 10,
                    },
                    Children = new Drawable[]
                    {
                        logo = new ThemableSpriteIcon
                        {
                            Size = new Vector2(50),
                            Icon = VignetteFont.Logo,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Colour = ThemeSlot.Black,
                        },
                        SearchBox = new FluentSearchBox
                        {
                            Width = 250,
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                        }
                    }
                },
            };
        }
    }
}
